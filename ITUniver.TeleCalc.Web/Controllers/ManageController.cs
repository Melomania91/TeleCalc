using ITUniver.TeleCalc.Web.Models;
using ITUniver.TeleCalc.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Controllers
{
    public class ManageController : Controller
    {
        private UserRepository UserRepository { get; set; }

        public ManageController()
        {
            var conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ituniver\TeleCalc\ITUniver.TeleCalc.Web\App_Data\TeleCalc.mdf;Integrated Security=True";

            UserRepository = new UserRepository(conString);
        }

        // GET: Manage
        public ActionResult UserList()
        {
            var model = UserRepository.Find("");
            return View(model);
        }

        [HttpGet]
        public ActionResult EditUser(int Id)
        {
            var user = UserRepository.Find($" Id = {Id}").FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User model)
        {
            if (UserRepository.Save(model))
            {
                return RedirectToAction("UserList");
            }
            ModelState.AddModelError("", "Не удалось сохранить");

            return View(model);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User model)
        {
            UserRepository.Save(model);
            return RedirectToAction("UserList");
        }
    }
}