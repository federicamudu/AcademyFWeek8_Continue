using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.MVC.Models;

namespace AcademyFWeek8.MVC.Helper
{
    public static class Mapping
    {
        public static CorsoViewModel ToCorsoViewModel(this Corso corso)
        {
            List<StudenteViewModel> listStudentiVM = new List<StudenteViewModel>();
            foreach (var item in corso.Studenti)
            {
                listStudentiVM.Add(item?.ToStudenteViewModel());
            }
            return new CorsoViewModel
            {
                CorsoCodice = corso.CorsoCodice,
                Nome = corso.Nome,
                Descrizione = corso.Descrizione,
                Studenti = listStudentiVM
            };
        }
        public static UtenteLoginViewModel ToUtenteViewModel(this Utente u)
        {
            return new UtenteLoginViewModel
            {
                Id = u.Id,
                Username = u.Username,
                Password = u.Password,
                Ruolo = u.Ruolo,
                ReturnUrl = u.ReturnUrl
            };
        }
        public static Utente ToUtente(this UtenteLoginViewModel u)
        {
            return new Utente
            {
                Id=u.Id,
                Username=u.Username,
                Password=u.Password,
                Ruolo=u.Ruolo,
                ReturnUrl=u.ReturnUrl
            };
        }
        public static DocenteViewModel ToDocenteViewModel(this Docente d)
        {
            List<LezioneViewModel> listLezioni = new List<LezioneViewModel>();
            foreach (var item in d.Lezioni)
            {
                listLezioni.Add(item?.ToLezioneViewModel());
            }
            return new DocenteViewModel
            {
                ID = d.ID,
                Nome = d.Nome,
                Cognome = d.Cognome,
                Email = d.Email,
                Telefono = d.Telefono,
                Lezioni = listLezioni                
            };
        }
        public static StudenteViewModel ToStudenteViewModel(this Studente s)
        {
            return new StudenteViewModel
            {
                ID = s.ID,
                Nome = s.Nome,
                Cognome = s.Cognome,
                Email = s.Email,
                TitoloStudio = s.TitoloStudio,
                DataNascita = s.DataNascita,
                CorsoCodice = s.CorsoCodice
            };
        }
        public static LezioneViewModel ToLezioneViewModel(this Lezione l)
        {
            return new LezioneViewModel
            {
                LezioneId = l.LezioneId,
                DataOraInizio = l.DataOraInizio,
                Durata = l.Durata,
                Aula = l.Aula,
                DocenteID = l.DocenteID,
                CorsoCodice = l.CorsoCodice
            };
        }
        public static Lezione ToLezione(this LezioneViewModel l)
        {
            return new Lezione
            {
                LezioneId = l.LezioneId,
                DataOraInizio = l.DataOraInizio,
                Durata = l.Durata,
                Aula = l.Aula,
                DocenteID = l.DocenteID,
                CorsoCodice = l.CorsoCodice
            };
        }
        public static Corso ToCorso(this CorsoViewModel cvm)
        {
            List<Studente> listStudenti = new List<Studente>();
            foreach (var item in cvm.Studenti)
            {
                listStudenti.Add(item?.ToStudente());
            }
            return new Corso
            {
                CorsoCodice = cvm.CorsoCodice,
                Nome = cvm.Nome,
                Descrizione = cvm.Descrizione,
                Studenti = listStudenti
            };
        }
        public static Studente ToStudente(this StudenteViewModel svm)
        {
            return new Studente
            {
                ID = svm.ID,
                Nome = svm.Nome,
                Cognome = svm.Cognome,
                Email = svm.Email,
                TitoloStudio = svm.TitoloStudio,
                DataNascita = svm.DataNascita,
                CorsoCodice = svm.CorsoCodice
            };
        }
        public static Docente ToDocente(this DocenteViewModel dvm)
        {
            return new Docente
            {
                ID = dvm.ID,
                Nome = dvm.Nome,
                Cognome = dvm.Cognome,
                Email = dvm.Email,
                Telefono = dvm.Telefono
            };
        }
    }
}
