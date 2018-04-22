using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Extensions
{
    public static class ButtonExtension
    {
        /// <summary>
        /// Сгенерировать кнопку для отправки формы с заданным именем
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name">Название кнопки</param>
        /// <returns></returns>
        public static MvcHtmlString Submit(this HtmlHelper html, string name, string onclick)
        {
            var btn = string.Format("<input type=\"submit\" value=\"{0}\" class=\"btn btn-default\"/ onclick = \"{1}\">", name, onclick);

            return new MvcHtmlString(btn);
        }
    }
}