using AcademyFWeek8.Core.BusinessLayer;
using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.MVC.Helper;
using AcademyFWeek8.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyFWeek8.MVC.Controllers
{
    [Authorize]
    public class CorsiController : Controller
    {
        private readonly IBusinessLayer BL;
        public CorsiController(IBusinessLayer bl)
        {
            BL = bl;
        }
        public IActionResult Index()
        {
            //devo recuperare la lista dei corsi dal repo e passarla alla lista
            var corsi = BL.GetAllCorsi();
            List<CorsoViewModel> corsiVM = new List<CorsoViewModel>();
            foreach (var item in corsi)
            {
                corsiVM.Add(item.ToCorsoViewModel());
            }
            return View(corsiVM);
        }
        [HttpGet("Corsi/Details/{codice}")]//default è id
        public IActionResult Details(string codice)
        {
            var corso = BL.GetAllCorsi().FirstOrDefault(c => c.CorsoCodice == codice);
            var corsoVM = corso.ToCorsoViewModel();
            return View(corsoVM);
        }

        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Create(CorsoViewModel corsoVM)
        {
            if (ModelState.IsValid)
            {
                //dobbiamo aggiungere il corsoVM al repo
                Corso corso = corsoVM.ToCorso();
                Esito esito = BL.AggiungiCorso(corso);
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
            return View(corsoVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var corsoRecuperato = BL.GetAllCorsi().FirstOrDefault(c => c.CorsoCodice == id);
            var corso = corsoRecuperato.ToCorsoViewModel();
            return View(corso);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Edit(CorsoViewModel corsoVM)
        {
            if (ModelState.IsValid)
            {
                Corso corso = corsoVM.ToCorso();
                Esito esito = BL.ModificaCorso(corso.CorsoCodice, corso.Nome, corso.Descrizione);
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
            return View(corsoVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var corsoRecuperato = BL.GetAllCorsi().FirstOrDefault(c => c.CorsoCodice == id);
            var corso = corsoRecuperato.ToCorsoViewModel();
            return View(corso);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Delete(CorsoViewModel corsoVM)
        {
            Corso corso = corsoVM.ToCorso(); //traduzione superflua
            Esito esito = BL.EliminaCorso(corso.CorsoCodice);
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

            return View(corsoVM);
        }
    }
}
