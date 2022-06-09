using AcademyFWeek8.Core.BusinessLayer;
using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.MVC.Helper;
using AcademyFWeek8.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyFWeek8.MVC.Controllers
{
    [Authorize]
    public class LezioniController : Controller
    {
        private readonly IBusinessLayer BL;
        public LezioniController(IBusinessLayer bl)
        {
            BL = bl;
        }
        public IActionResult Index()
        {
            var lezioni = BL.GetAllLezioni();
            List<LezioneViewModel> lezioniVM = new List<LezioneViewModel>();
            foreach (var item in lezioni)
            {
                lezioniVM.Add(item.ToLezioneViewModel());
            }
            return View(lezioniVM);
        }
        public IActionResult Details(int id)
        {
            var lezione = BL.GetAllLezioni().FirstOrDefault(c => c.LezioneId == id);
            var lezioneVM = lezione.ToLezioneViewModel();
            return View(lezioneVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Create(LezioneViewModel lezioneVM)
        {
            if (ModelState.IsValid)
            {
                Lezione lezione = lezioneVM.ToLezione();
                Esito esito = BL.InserisciNuovaLezione(lezione);
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
            return View(lezioneVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var lezioneRecuperata = BL.GetAllLezioni().FirstOrDefault(c => c.LezioneId == id);
            var lezione = lezioneRecuperata.ToLezioneViewModel();
            return View(lezione);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Edit(LezioneViewModel lezioneVM)
        {
            if (ModelState.IsValid)
            {
                Lezione lezione = lezioneVM.ToLezione();
                Esito esito = BL.ModificaAulaLezione(lezione.LezioneId, lezione.Aula);
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
            return View(lezioneVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var lezioneRecuperata = BL.GetAllLezioni().FirstOrDefault(c => c.LezioneId == id);
            var lezione = lezioneRecuperata.ToLezioneViewModel();
            return View(lezione);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Delete(LezioneViewModel lezioneVM)
        {
            Lezione lezione = lezioneVM.ToLezione(); //traduzione superflua
            Esito esito = BL.EliminaLezione(lezione.LezioneId);
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

            return View(lezioneVM);
        }
    }
}
