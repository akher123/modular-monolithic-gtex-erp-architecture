using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.ApploicationService
{
    public class CostCentreService : BaseService<BusinessUnit>, ICostCentreService
    {
        private readonly IRepository<CostCentre> costCentreRepository;

        public CostCentreService(IRepository<CostCentre> costCentreRepository,
            IApplicationServiceBuilder applicationServiceBuilder) : base(applicationServiceBuilder)
        {
            this.costCentreRepository = costCentreRepository;
        }

        public async Task<List<SelectModel>> GetCostCentreSelectItemsAsync(int businessProfileId)
        { 
            List<SelectModel> costCentreList = await Task.Run(()=> this.costCentreRepository
                .Where(t => t.IsActive && t.BusinessProfileId == businessProfileId )
                .Select(x => new SelectModel
                {
                    Id = x.Id,
                    Name = x.Name                    
                }).ToList());

            return costCentreList;
        }
    }
}
