using AcademyFWeek8.Core.BusinessLayer;
using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.MVC.Helper;
using AcademyFWeek8.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademyFWeek8.MVC.Controllers
{
    [Authorize]
    public class DocentiController : Controller
    {
        private readonly IBusinessLayer BL;
        public DocentiController(IBusinessLayer bl)
        {
            BL = bl;
        }
        public IActionResult Index()
        {
            var docenti = BL.GetAllDocenti();
            List<DocenteViewModel> docentiVM = new List<DocenteViewModel>();
            foreach (var item in docenti)
            {
                docentiVM.Add(item.ToDocenteViewModel());
            }
            return View(docentiVM);
        }
        public IActionResult Details(int id)
        {
            var docente = BL.GetAllDocenti().FirstOrDefault(c => c.ID == id);
            var docenteVM = docente.ToDocenteViewModel();
            return View(docenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Create(DocenteViewModel docenteVM)
        {
            if (ModelState.IsValid)
            {
                Docente docente = docenteVM.ToDocente();
                Esito esito = BL.InserisciNuovoDocente(docente);
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
            return View(docenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var docenteRecuperato = BL.GetAllDocenti().FirstOrDefault(c => c.ID == id);
            var docente = docenteRecuperato.ToDocenteViewModel();
            return View(docente);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Edit(DocenteViewModel docenteVM)
        {
            if (ModelState.IsValid)
            {
                Docente docente = docenteVM.ToDocente();
                Esito esito = BL.ModificaTelefonoDocente(docente.ID, docente.Telefono);
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
            return View(docenteVM);
        }
        [Authorize(Policy = "Adm")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var docenteRecuperato = BL.GetAllDocenti().FirstOrDefault(c => c.ID == id);
            var docente = docenteRecuperato.ToDocenteViewModel();
            return View(docente);
        }
        [Authorize(Policy = "Adm")]
        [HttpPost]
        public IActionResult Delete(DocenteViewModel docenteVM)
        {
            Docente docente = docenteVM.ToDocente(); //traduzione superflua
            Esito esito = BL.EliminaDocente(docente.ID);
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

            return View(docenteVM);
        }
    }
}
