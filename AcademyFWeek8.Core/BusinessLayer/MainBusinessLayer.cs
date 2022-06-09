using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.Core.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyFWeek8.Core.BusinessLayer
{
    public class MainBusinessLayer : IBusinessLayer
    {
        private readonly IRepositoryCorsi corsiRepo;        
        private readonly IRepositoryStudenti studentiRepo;     
        private readonly IRepositoryDocenti docentiRepo;
        private readonly IRepositoryLezioni lezioniRepo;
        private readonly IRepositoryUtenti utentiRepo;


        public MainBusinessLayer(IRepositoryCorsi corsi, IRepositoryStudenti studenti, IRepositoryDocenti docenti, IRepositoryLezioni lezioni, IRepositoryUtenti utenti)
        {
            corsiRepo = corsi;
            studentiRepo = studenti;
            docentiRepo = docenti;
            lezioniRepo = lezioni;
            utentiRepo = utenti;
        }
        #region Lezioni
        public Esito EliminaLezione(int idLezioneDaEliminare)
        {
            var lezioneEsistente = lezioniRepo.GetById(idLezioneDaEliminare);
            if (lezioneEsistente == null)
            {
                return new Esito { Messaggio = "Nessuna lezione corrispondente all'id inserito", IsOk = false };
            }
            lezioniRepo.Delete(lezioneEsistente);
            return new Esito { Messaggio = "Lezione eliminata correttamente", IsOk = true };
        }
        public List<Lezione> GetAllLezioni()
        {
            return lezioniRepo.GetAll();
        }

        public Esito InserisciNuovaLezione(Lezione nuovaLezione)
        {
            lezioniRepo.Add(nuovaLezione);
            return new Esito { Messaggio = "Lezione inserita correttamente", IsOk = true };
        }
        public Esito ModificaAulaLezione(int idLezioneDaModificare, string nuovaAula)
        {
            //controllo input
            //controllo se id esiste
            var lezione = lezioniRepo.GetById(idLezioneDaModificare);
            if (lezione == null)
            {
                return new Esito { Messaggio = "Id Lezione errato o inesistente", IsOk = false };
            }
            lezione.Aula = nuovaAula;
            lezioniRepo.Update(lezione);
            return new Esito { Messaggio = "Aula Lezione aggiornato correttamente", IsOk = true };
        }

        #endregion

        #region Docenti
        public Esito EliminaDocente(int idDocenteDaEliminare)
        {
            var docenteEsistente = docentiRepo.GetById(idDocenteDaEliminare);
            if (docenteEsistente == null)
            {
                return new Esito { Messaggio = "Nessun docente corrispondente all'id inserito", IsOk = false };
            }
            docentiRepo.Delete(docenteEsistente);
            return new Esito { Messaggio = "Docente eliminato correttamente", IsOk = true };
        }
        public List<Docente> GetAllDocenti()
        {
            return docentiRepo.GetAll();
        }

        public Esito InserisciNuovoDocente(Docente nuovoDocente)
        {
            docentiRepo.Add(nuovoDocente);
            return new Esito { Messaggio = "Docente inserito correttamente", IsOk = true };
        }
        public Esito ModificaTelefonoDocente(int idDocenteDaModificare, string nuovoTelefono)
        {
            //controllo input
            //controllo se id esiste
            var docente = docentiRepo.GetById(idDocenteDaModificare);
            if (docente == null)
            {
                return new Esito { Messaggio = "Id Docente errato o inesistente", IsOk = false };
            }
            docente.Telefono = nuovoTelefono;
            docentiRepo.Update(docente);
            return new Esito { Messaggio = "Telefono Docente aggiornato correttamente", IsOk = true };
        }
        #endregion

        #region Funzionalità Studenti
        public Esito EliminaStudente(int idStudenteDaEliminare)
        {
            var studenteEsistente = studentiRepo.GetById(idStudenteDaEliminare);
            if (studenteEsistente == null)
            {
                return new Esito { Messaggio = "Nessuno studente corrispondente all'id inserito", IsOk = false };
            }
            studentiRepo.Delete(studenteEsistente);
            return new Esito { Messaggio = "Studente eliminato correttamente", IsOk = true };
        }
        public List<Studente> GetAllStudenti()
        {
            return studentiRepo.GetAll();
        }

        public List<Studente> GetStudentiByCorsoCodice(string codiceCorso)
        {
            //controllo input
            //controllo se codice corso esiste. Se non esiste allora restituisco null
            //se il corso esiste, allora recupero dalla repo degli studenti la lista di quelli che hanno quel corsoCodice
            var corso = corsiRepo.GetByCode(codiceCorso);
            if (corso == null)
            {
                return null;
            }
            List<Studente> studentiFiltrati = new List<Studente>();
            foreach (var item in studentiRepo.GetAll())
            {
                if (item.CorsoCodice == codiceCorso)
                {
                    studentiFiltrati.Add(item);
                }
            }
            return studentiFiltrati;

        }

        public Esito InserisciNuovoStudente(Studente nuovoStudente)
        {
            //controllo input
            Corso corsoEsistente = corsiRepo.GetByCode(nuovoStudente.CorsoCodice);
            if (corsoEsistente == null)
            {
                return new Esito { Messaggio = "Codice corso errato", IsOk = false };
            }
            studentiRepo.Add(nuovoStudente);
            //corsoEsistente.Studenti.Add(nuovoStudente);
            return new Esito { Messaggio = "studente inserito correttamente", IsOk = true };
        }
        public Esito ModificaMailStudente(int idStudenteDaModificare, string nuovaEmail)
        {
            //controllo input
            //controllo se id esiste
            var studente = studentiRepo.GetById(idStudenteDaModificare);
            if (studente == null)
            {
                return new Esito { Messaggio = "Id Studente errato o inesistente", IsOk = false };
            }
            studente.Email = nuovaEmail;
            studentiRepo.Update(studente);
            return new Esito { Messaggio = "Email Studente aggiornata correttamente", IsOk = true };
        }
        #endregion Funzionalità Studenti

        #region Funzionalità Corsi
        public Esito AggiungiCorso(Corso nuovoCorso)
        {
            Corso corsoRecuperato = corsiRepo.GetByCode(nuovoCorso.CorsoCodice);
            if (corsoRecuperato == null)
            {
                corsiRepo.Add(nuovoCorso);
                return new Esito() { IsOk = true, Messaggio = "Corso aggiunto correttamente" };
            }
            return new Esito() { IsOk = false, Messaggio = "Impossibile aggiungere il corso perché esiste già un corso con quel codice" };
        }
        public Esito EliminaCorso(string? codice)
        {
            var corsoRecuperato = corsiRepo.GetByCode(codice);
            if (corsoRecuperato == null)
            {
                return new Esito() { IsOk = false, Messaggio = "Nessun corso corrispondente al codice inserito" };
            }
            corsiRepo.Delete(corsoRecuperato);
            return new Esito() { IsOk = true, Messaggio = "Corso eliminato correttamente" };
        }

        public List<Corso> GetAllCorsi()
        {
            return corsiRepo.GetAll();
        }

        

        public Esito ModificaCorso(string? codice, string? nuovoNome, string? nuovaDescrizione)
        {
            var corsoRecuperato=corsiRepo.GetByCode(codice);
            if(corsoRecuperato == null)
            {
                return new Esito() { IsOk = false, Messaggio = "Nessun corso corrispondente al codice inserito" };
            }
            corsoRecuperato.Nome = nuovoNome;
            corsoRecuperato.Descrizione= nuovaDescrizione;
            corsiRepo.Update(corsoRecuperato);
            return new Esito() { IsOk = true, Messaggio = "Corso aggiornato correttamente" };
        }


        #endregion Funzionalità Corsi
        #region Utente
        public Utente GetAccount(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }
            return utentiRepo.GetByUsername(username);
        }
        public Esito EliminaUtente(string userUtenteDaEliminare)
        {
            var utenteEsistente = utentiRepo.GetByUsername(userUtenteDaEliminare);
            if (utenteEsistente == null)
            {
                return new Esito { Messaggio = "Nessun utente corrispondente all'user inserito", IsOk = false };
            }
            utentiRepo.Delete(utenteEsistente);
            return new Esito { Messaggio = "utente eliminato correttamente", IsOk = true };
        }
        public List<Utente> GetAllUtenti()
        {
            return utentiRepo.GetAll();
        }

        public Esito InserisciNuovoUtente(Utente nuovoUtente)
        {
            utentiRepo.Add(nuovoUtente);
            return new Esito { Messaggio = "Utente inserito correttamente", IsOk = true };
        }
        public Esito ModificaPasswordUtente(string userUtenteDaModificare, string nuovaPassword)
        {
            //controllo input
            //controllo se id esiste
            var utente = utentiRepo.GetByUsername(userUtenteDaModificare);
            if (utente == null)
            {
                return new Esito { Messaggio = "Username errato o inesistente", IsOk = false };
            }
            utente.Password = nuovaPassword;
            utentiRepo.Update(utente);
            return new Esito { Messaggio = "Password aggiornato correttamente", IsOk = true };
        }
        #endregion
    }
}
