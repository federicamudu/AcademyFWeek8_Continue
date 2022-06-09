using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.Core.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyFWeek8.RepositoryMOCK
{
    public class RepositoryDocentiMock : IRepositoryDocenti
    {
        public static List<Docente> docenti = new List<Docente>()
        {
            new Docente{
                ID=1,
                Nome="Paola",
                Cognome="Pusceddu",
                Email="paola@mail.it",
                Telefono="3456789012"
                },

            new Docente{
                ID=2,
                Nome="Mario",
                Cognome="Rossi",
                Email ="mario@rossi.it",
                Telefono="1234567890"
                }
        };
        public Docente Add(Docente item)
        {
            if (docenti.Count == 0)
            {
                item.ID = 1;
            }
            else //se la lista è piena trova l'id più alto e, dopo aver incrementato di 1, lo assegna ad item
            {
                int maxId = 1;
                foreach (var s in docenti)
                {
                    if (s.ID > maxId)
                    {
                        maxId = s.ID;
                    }
                }
                item.ID = maxId + 1;
            }
            docenti.Add(item);
            return item;
        }

        public bool Delete(Docente item)
        {
            docenti.Remove(item);
            return true;
        }

        public List<Docente> GetAll()
        {
            return docenti;
        }

        public Docente GetById(int id)
        {
            foreach (var item in docenti)
            {
                if (item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Docente Update(Docente item)
        {
            foreach (var s in docenti)
            {
                if (s.ID == item.ID)
                {
                    s.Telefono = item.Telefono;
                    return s;
                }
            }
            return null;
        }
    }
}
