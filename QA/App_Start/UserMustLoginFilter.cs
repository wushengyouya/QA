using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.App_Start
{
    public class UserMustLoginFilter : ActionFilterAttribute
    {
        

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/User/Login");
            }
        }
    }
}