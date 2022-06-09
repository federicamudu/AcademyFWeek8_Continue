using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.Core.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyFWeek8.RepositoryMOCK
{
    public class RepositoryLezioniMock : IRepositoryLezioni
    {
        public static List<Lezione> lezioni = new List<Lezione>()
        {
            new Lezione()
            {
                DataOraInizio = new DateTime(2022,02,01,09,00,00),
                Durata = 180,
                Aula = "B",
                DocenteID = 1,
                CorsoCodice = "C-01"
            }
        };
        public Lezione Add(Lezione item)
        {
            if (item == null)
            {
                return null;
            }
            lezioni.Add(item);
            return item;
        }

        public bool Delete(Lezione item)
        {
            if (item == null)
            {
                return false;
            }
            lezioni.Remove(item);
            return true;
        }

        public List<Lezione> GetAll()
        {
            return lezioni;
        }

        public Lezione GetById(int id)
        {
            foreach (var item in lezioni)
            {
                if (item.LezioneId == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Lezione Update(Lezione item)
        {
            foreach (var c in lezioni)
            {
                if (c.LezioneId == item.LezioneId)
                {
                    c.Aula = item.Aula;
                    return c;
                }
            }
            return null;
        }
    }
}
