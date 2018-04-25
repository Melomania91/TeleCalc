using ITUniver.TeleCalc.ConCalc;
using ITUniver.TeleCalc.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public PartialViewResult Exec(CalcModel model)
        {
            var calc = new Calc();
            var operations = calc.GetOperationsName();
            model.OperationList = new SelectList(calc.GetOperationsName());

            if (!string.IsNullOrEmpty(model.OperName) && operations.Contains(model.OperName))
                model.Result = calc.Exec(model.OperName, model.InputData);
            else
                model.Result = Double.NaN;

            return PartialView("ExecResult", model);

        }

        [HttpGet]
        public ActionResult OperationHistory()
        {
            var history = ReadOrderData(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ituniver\TeleCalc\ITUniver.TeleCalc.Web\App_Data\TeleCalc.mdf;Integrated Security=True");

            return View(history);
        }

        private List<OperationHistory> ReadOrderData(string connectionString)
        {
            var hist = new List<OperationHistory>();
            string queryString = "SELECT his.Id, us.Name, op.Name, his.Args, " +
                "his.Result, his.CalcDate, his.Time " +
                "FROM dbo.History as his, [dbo].[User] as us, dbo.Operation as op " +
                "WHERE his.Initiator = us.Id AND his.Operation = op.Id;";

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    hist.Add(new OperationHistory()
                    {
                        Id = (int)reader[0],
                        Initiator = (string)reader[1],
                        Operation = (string)reader[2],
                        Args = (string)reader[3],
                        Result = (double)reader[4],
                        CalcDate = (DateTime)reader[5],
                        Time = (int)reader[6]
                    });
                    
                }

                // Call Close when done reading.
                reader.Close();
                return hist;
            }
        }
    }
}