using ITUniver.TeleCalc.ConCalc;
using ITUniver.TeleCalc.Web.Models;
using ITUniver.TeleCalc.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Controllers
{
    [Authorize]
    public class CalcController : Controller
    {
        private Calc Calc { get; set; }

        private HistoryRepository HistoryRepository { get; set; }

        private OperationRepository OperationRepository { get; set; }

        private UserRepository UserRepository { get; set; }

        public CalcController()
        {
            var conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ituniver\TeleCalc\ITUniver.TeleCalc.Web\App_Data\TeleCalc.mdf;Integrated Security=True";
            Calc = new Calc();

            HistoryRepository = new HistoryRepository(conString);
            OperationRepository = new OperationRepository(conString);
            UserRepository = new UserRepository(conString);
        }

       
        [HttpGet]
        public ActionResult Index(string operName, double? x, double? y)
        {
            ViewBag.OperName = operName;
            ViewBag.X = x;
            ViewBag.Y = y;
            ViewBag.ShowOperations = false;
            var calc = new Calc();
            var operations = calc.GetOperationsName();

            if (!string.IsNullOrEmpty(operName) && operations.Contains(operName))
                ViewBag.Result = calc.Exec(operName, (double)x, (double)y);
            else
            {
                ViewBag.ShowOperations = true;
                ViewBag.Message = string.Format("Доступные операции:\n{0}", string.Join(", ", operations.Select(o => o)));
            }
            return View();

        }

        [HttpGet]
        public ActionResult Operations()
        {
            ViewBag.Operations = OperationRepository.Find("").Select(o => o.Name).ToArray();
            return View("Ops");
        }

        [HttpGet]
        public ActionResult Exec()
        {
            var model = new CalcModel();
            var calc = new Calc();

            model.OperationList = new SelectList(calc.GetOperationsName());

            ViewData["Tops"] = OperationRepository.GetTop(1).Select(o => o.Name);
            if (Request.IsAjaxRequest())
            {
                return PartialView("Partial", model);
            }
            else
            {
                return View(model);
            }

        }

        [HttpPost]
        public PartialViewResult Exec(CalcModel model)
        {
            var calc = new Calc();
            var operations = calc.GetOperationsName();
            model.OperationList = new SelectList(calc.GetOperationsName());

            if (!string.IsNullOrEmpty(model.OperName) && operations.Contains(model.OperName))
            {
                model.Result = calc.Exec(model.OperName, model.InputData);

                if (OperationRepository.LoadByName(model.OperName) == null)
                {
                    var opModel = new OperationModel()
                    {
                        Name = model.OperName,
                        Owner = 1
                    };

                    OperationRepository.Save(opModel);
                }

                var history = new OperationHistory()
                {
                    Operation = OperationRepository.LoadByName(model.OperName).Id,
                    Initiator = UserRepository.GetUserByLogin(User.Identity.Name).ToString() ?? "1",
                    Result = model.Result,
                    Args = string.Join(";", model.InputData),
                    CalcDate = DateTime.Now,
                    Time = 15
                };

                HistoryRepository.Save(history); 
            }
            else
                model.Result = Double.NaN;

            return PartialView("ExecResult", model);

        }

        [HttpGet]
        public ActionResult OperationHistory()
        {
            ViewData.Model = HistoryRepository.Find("");
            return View();
        }

    }
}