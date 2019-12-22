using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QA.App_Start
{
    public class DoctorActionFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["doctor"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/User/DoctorLogin");
            }
        }
    }
}