using AcademyFWeek8.Core.BusinessLayer;
using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.MVC.Helper;
using AcademyFWeek8.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcademyFWeek8.MVC.Controllers
{
    [Authorize]
    public class StudentiController : Controller
    {
        private readonly IBusinessLayer BL;
        public StudentiController(IBusinessLayer bl)
        {
            BL = bl;
        }
        public IActionResult Index()
        {
            var studenti = BL.GetAllStudenti();
            List<StudenteViewModel> studentiVM = new List<StudenteViewModel>();
            foreach (var item in studenti)
            {
                studentiVM.Add(item.ToStudenteViewModel());
            }
            return View(studentiVM);
        }
        public IActionResult Details(int id)
        {
            var studente = BL.GetAllStudenti().FirstOrDefault(c => c.ID == id);
            var studenteVM = studente.ToStudenteViewModel();
            return View(studenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Create()
        {
            LoadViewBag();
            return View();
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Create(StudenteViewModel studenteVM)
        {
            if (ModelState.IsValid)
            {
                Studente studente = studenteVM.ToStudente();
                Esito esito = BL.InserisciNuovoStudente(studente);
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
            LoadViewBag();
            return View(studenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var studenteRecuperato = BL.GetAllStudenti().FirstOrDefault(c => c.ID == id);
            var studente = studenteRecuperato.ToStudenteViewModel();
            LoadViewBag();
            return View(studente);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Edit(StudenteViewModel studenteVM)
        {
            if (ModelState.IsValid)
            {
                Studente studente = studenteVM.ToStudente();
                Esito esito = BL.ModificaMailStudente(studente.ID, studente.Email);
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
            LoadViewBag();
            return View(studenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var studenteRecuperato = BL.GetAllStudenti().FirstOrDefault(c => c.ID == id);
            var studente = studenteRecuperato.ToStudenteViewModel();
            return View(studente);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Delete(StudenteViewModel studenteVM)
        {
            Studente studente = studenteVM.ToStudente(); //traduzione superflua
            Esito esito = BL.EliminaStudente(studente.ID);
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

            return View(studenteVM);
        }
        private void LoadViewBag()
        {
            ViewBag.TitoloStudio = new SelectList(new[]
            {
                new{Value="M", Text="Master" },
                new{Value="D", Text="Diploma" },
                new{Value="L", Text="Laurea" }
            }.OrderBy(x=>x.Text), "Value", "Text");
        }
    }
}
