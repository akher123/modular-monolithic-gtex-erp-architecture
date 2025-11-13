using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data
{
    public interface IStoredProcedureRepository : IRepository<object>
    {
        List<Company> GetCompanies();
    }
}
