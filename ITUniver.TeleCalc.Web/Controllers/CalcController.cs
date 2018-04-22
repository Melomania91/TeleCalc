using ITUniver.TeleCalc.ConCalc;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Controllers
{
    public class CalcController : Controller
    {
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
            var calc = new Calc();
            var operations = calc.GetOperationsName();
            ViewBag.Operations = operations;
            ViewBag.Message = string.Format("Доступные операции:\n{0}", string.Join(", ", operations.Select(o => o)));
            return View("Ops");
        }

        [HttpGet]
        public ActionResult Exec()
        {            
            var model = new CalcModel();
            var calc = new Calc();
            var operations = calc.GetOperationsName();
            model.OperationList = new SelectList(calc.GetOperationsName());

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
        public ActionResult Exec(CalcModel model)
        {
            var calc = new Calc();
            var operations = calc.GetOperationsName();
            model.OperationList = new SelectList(calc.GetOperationsName());

            if (!string.IsNullOrEmpty(model.OperName) && operations.Contains(model.OperName))
                model.Result = calc.Exec(model.OperName, model.X, model.Y);
            else
                model.Result = Double.NaN;

            if (Request.IsAjaxRequest())
            {
                return PartialView("Partial", model);
            }
            else
            {
                return View(model);
            }
        }

        
    }
}