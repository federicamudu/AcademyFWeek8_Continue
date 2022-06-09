using AcademyFWeek8.Core.BusinessLayer;
using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.MVC.Helper;
using AcademyFWeek8.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Authorize(Policy = "Adm")]
        public IActionResult Index()
        {
            var utenti = BL.GetAllUtenti();
            List<UtenteLoginViewModel> utentiVM = new List<UtenteLoginViewModel>();
            foreach (var item in utenti)
            {
                utentiVM.Add(item.ToUtenteViewModel());
            }
            return View(utentiVM);
        }
        [Authorize(Policy = "Adm")]
        public IActionResult Details(string username)
        {
            var utenti = BL.GetAllUtenti().FirstOrDefault(c => c.Username == username);
            var utenteVM = utenti.ToUtenteViewModel();
            return View(utenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Create(UtenteLoginViewModel utenteVM)
        {
            if (ModelState.IsValid)
            {
                Utente utente = utenteVM.ToUtente();
                Esito esito = BL.InserisciNuovoUtente(utente);
                if (esito.IsOk == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //visualizzo il messaggio di errore in una pagina dedicata
                    ViewBag.MessaggioErrore = esito.Messaggio;
                    return View("ErroriBusiness");
                }
            }
            return View(utenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Edit(string username)
        {
            var utenteRecuperato = BL.GetAllUtenti().FirstOrDefault(c => c.Username == username);
            var utente = utenteRecuperato.ToUtenteViewModel();
            return View(utente);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Edit(UtenteLoginViewModel utenteVM)
        {
            if (ModelState.IsValid)
            {
                Utente utente = utenteVM.ToUtente();
                Esito esito = BL.ModificaPasswordUtente(utente.Username, utente.Password);
                if (esito.IsOk == true)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //visualizzo il messaggio di errore in una pagina dedicata
                    ViewBag.MessaggioErrore = esito.Messaggio;
                    return View("ErroriBusiness");
                }
            }
            return View(utenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Delete(string username)
        {
            var utenteRecuperato = BL.GetAllUtenti().FirstOrDefault(c => c.Username == username);
            var utente = utenteRecuperato.ToUtenteViewModel();
            return View(utente);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Delete(UtenteLoginViewModel utenteVM)
        {
            Utente utente = utenteVM.ToUtente(); //traduzione superflua
            Esito esito = BL.EliminaUtente(utente.Username);
            if (esito.IsOk == true)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //visualizzo il messaggio di errore in una pagina dedicata
                ViewBag.MessaggioErrore = esito.Messaggio;
                return View("ErroriBusiness");
            }

            return View(utenteVM);
        }
    }
}
