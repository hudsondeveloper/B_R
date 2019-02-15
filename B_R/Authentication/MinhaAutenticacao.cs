using B_R.Controllers;
using B_R.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace B_R.Authentication
{
    public class MinhaAutenticacao : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
           
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            User usuario = HomeController.user;
            if (usuario == null)
            {
               HttpContext.Current.Response.Redirect("~/");
            }
        }
    }
}