using ITUniver.TeleCalc.ConCalc;
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
            var operations = calc.GetOperations();

            if (!string.IsNullOrEmpty(operName) && operations.Any(o => o.Name == operName))
                ViewBag.Result = calc.Exec(operName, (double)x, (double)y);
            else
            {
                ViewBag.ShowOperations = true;
                ViewBag.Message = string.Format("Доступные операции:\n{0}", string.Join(", ", operations.Select(o => o.Name)));
            }
                return View(); 
            
        }
        [HttpGet]
        public ActionResult Operations()
        {
            var calc = new Calc();
            var operations = calc.GetOperations();
            ViewBag.Message = string.Format("Доступные операции:\n{0}", string.Join(", ", operations.Select(o => o.Name)));
            return View();
        }


    }
}