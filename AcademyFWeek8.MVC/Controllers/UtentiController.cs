using AcademyFWeek8.Core.BusinessLayer;
using AcademyFWeek8.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace AcademyFWeek8.MVC.Controllers
{
    public class UtentiController : Controller
    {
        private readonly IBusinessLayer BL;
        public UtentiController(IBusinessLayer bl)
        {
            BL = bl;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl="/")
        {
            return View(new UtenteLoginViewModel { ReturnUrl = returnUrl});
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(UtenteLoginViewModel utenteLVM)
        {
            if(utenteLVM == null)
            {
                return View();
            }
            var utente = BL.GetAccount(utenteLVM.Username);
            if(utente != null && ModelState.IsValid)
            {
                if(utente.Password == utenteLVM.Password)
                {
                    //l'utente è "autenticato"
                    var claim = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, utente.Username),
                        new Claim(ClaimTypes.Role, utente.Ruolo.ToString())
                    };
                    var properties = new AuthenticationProperties
                    {
                        RedirectUri = utenteLVM.ReturnUrl,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
                    };
                    var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), properties);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError(nameof(utenteLVM.Password), "Password errata");
                    return View(utenteLVM);
                }
            }
            else
            {
                return View(utenteLVM);
            }
            return View();
        }

        public IActionResult Forbidden() => View();
        //{
        //    return View();
        //}
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
