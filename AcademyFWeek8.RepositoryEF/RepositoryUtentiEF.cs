using AcademyFWeek8.Core.Entities;
using AcademyFWeek8.Core.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyFWeek8.RepositoryEF
{
    public class RepositoryUtentiEF : IRepositoryUtenti
    {
        //private readonly MasterContext ctx;
        //public RepositoryUtentiEF(MasterContext context)
        //{
        //    ctx = context;
        //}
        public Utente Add(Utente item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Utente item)
        {
            throw new NotImplementedException();
        }

        public List<Utente> GetAll()
        {
            using (var ctx = new MasterContext())
            {
                return ctx.Utente.ToList();
            }
        }

        public Utente GetByUsername(string username)
        {
            return GetAll().FirstOrDefault(x => x.Username == username);
        }

        public Utente Update(Utente item)
        {
            throw new NotImplementedException();
        }
    }
}
