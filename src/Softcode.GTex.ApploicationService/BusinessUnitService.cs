using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.ApploicationService
{
    public class BusinessUnitService : BaseService<BusinessUnit>, IBusinessUnitService
    {
        private readonly IRepository<BusinessUnit> businessUnitRepository;

        public BusinessUnitService(IRepository<BusinessUnit> businessUnitRepository,
            IApplicationServiceBuilder applicationServiceBuilder) : base(applicationServiceBuilder)
        {
            this.businessUnitRepository = businessUnitRepository;
        }

        public async Task<List<SelectModel>> GetBusinessUnitSelectItemsAsync(int businessProfileId)
        { 
            List<SelectModel> businessUnits = await Task.Run(()=> this.businessUnitRepository
                .Where(t => t.IsActive && t.BusinessProfileId == businessProfileId )
                .Select(x => new SelectModel
                {
                    Id = x.Id,
                    Name = x.Name                    
                }).ToList());

            return businessUnits;
        }
    }
}
