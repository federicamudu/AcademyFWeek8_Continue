using AcademyFWeek8.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyFWeek8.Core.InterfaceRepository
{
    public interface IRepositoryUtenti:IRepository<Utente>
    {
        Utente GetByUsername(string username);
    }
}
