using CustomOWINAuth.Models;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace CustomOWINAuth.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
                return View(user);

            string returnUrl = Request.QueryString["ReturnUrl"] != null ? Request.QueryString["ReturnUrl"] : "/Home";

            if(user.Username == "jesumarquez@gmail.com" && user.Password == "123")
            {
                UserState userState = new UserState
                {
                    Name = "Jesú",
                    Email = user.Username,
                    IsAdmin = true
                };

                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userState.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim("userState", userState.ToString())
                };

                ClaimsIdentity identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimTypes.Name, ClaimTypes.Role);
                IOwinContext context = Request.GetOwinContext();
                context.Authentication.SignIn(identity);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password");
                return View(user);
            }

            return Redirect(returnUrl);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            IOwinContext context = Request.GetOwinContext();
            context.Authentication.SignOut();
            return Redirect("Login");
        }
    }
}