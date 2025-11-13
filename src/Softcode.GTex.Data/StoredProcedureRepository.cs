using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Softcode.GTex.Data
{
    public class StoredProcedureRepository : BaseRepository<ApplicationDbContext, object>, IStoredProcedureRepository
    {
        //private readonly ApplicationDbContext ApplicationDbContext;
        public StoredProcedureRepository(ApplicationDbContext context, ILoggedInUserService loggedInUserService) : base(context, loggedInUserService)
        {
            //this.context = context;
        }

        public List<Company> GetCompanies()
        {
            
            context.Companies.FromSql($"EXECUTE [Customer].[GetCompany] @BusinessProfileId", new SqlParameter("@BusinessProfileId", "93F8BB7E-6F37-4676-D5C7-A6DFC3050358"));

            List<Company> companies = context.Companies.FromSql($"EXECUTE [Customer].[GetCompany] '93F8BB7E-6F37-4676-D5C7-A6DFC3050358'").ToList();

            return companies;
        }
    }
}
