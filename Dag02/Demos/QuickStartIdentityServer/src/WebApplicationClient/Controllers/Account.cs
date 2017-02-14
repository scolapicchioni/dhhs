using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationClient.Controllers
{
    public class Account : Controller
    {
        public async Task  Logout() {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync("oidc");

            //return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        
    }
}
