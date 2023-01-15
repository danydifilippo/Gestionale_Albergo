using Gestionale_Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Gestionale_Albergo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Dipendenti d)
        {
            if (d.Autenticato(d.Username, d.Password))
            {
                FormsAuthentication.SetAuthCookie(d.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }

    }
}