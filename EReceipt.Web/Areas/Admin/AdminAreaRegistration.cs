﻿using System.Web.Mvc;

namespace EReceipt.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{tag}",
                new { controller = "Home", action = "Index", tag = UrlParameter.Optional },
                new [] {"EReceipt.Web.Areas.Admin.Controllers"}
            );
        }
    }
}