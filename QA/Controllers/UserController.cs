using QA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.Controllers
{
    public class UserController : Controller
    {
        private OnlineQEntities onlineQEntities = new OnlineQEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Personal()
        {
            return View();
        }
    }
}