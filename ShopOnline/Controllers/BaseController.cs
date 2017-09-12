﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShopOnline.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (Session["TenDangNhap"] == null && Session["MatKhau"]==null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new { controller = "Admin", action = "Login" }));
            }
            base.OnActionExecuting(filterContext);
        }
	}
}