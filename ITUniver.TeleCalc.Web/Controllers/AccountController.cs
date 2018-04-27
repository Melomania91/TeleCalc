using ITUniver.TeleCalc.Web.Models;
using ITUniver.TeleCalc.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ITUniver.TeleCalc.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository UserRepository { get; set; }

        public AccountController()
        {
            var conString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\ituniver\TeleCalc\ITUniver.TeleCalc.Web\App_Data\TeleCalc.mdf;Integrated Security=True";

            UserRepository = new UserRepository(conString);
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {           
            // Проверяем модель на валидность
            if (!ModelState.IsValid)

            // Если модель не прошла проверку
            // Выдаём ошибку и открываем страницу заново
            {
                ModelState.AddModelError("", "Что-то пошло не так");
                return View(model);
            }

            // Если всё хорошо
            // Отправляем запрос в БД
            // Проверяем, что в БД есть запись с таким логином и паролем
            var user = UserRepository
                .Find($"Login = N'{model.Login}' AND Password = N'{model.Password}' ")
                .FirstOrDefault();

            // Если записи нет
            if (user == null)
            {
                // Выдаём ошибку и открываем страницу заново
                ModelState.AddModelError("", "Ошибка авторизации");
                return View(model);
            }

            // Если всё хорошо, сохраняем найденного пользователя как текущего
            FormsAuthentication.SetAuthCookie(model.Login, true);

            // Переходим на страницу помогатора
            return RedirectToAction("Exec", "Calc");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}