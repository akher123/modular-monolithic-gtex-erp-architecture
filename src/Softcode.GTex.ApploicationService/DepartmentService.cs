using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;

namespace Softcode.GTex.ApploicationService
{
    public class DepartmentService : BaseService<BusinessUnit>, IDepartmentService
    {
        private readonly IRepository<Department> departmentRepository;

        public DepartmentService(IRepository<Department> departmentRepository,
            IApplicationServiceBuilder applicationServiceBuilder) : base(applicationServiceBuilder)
        {
            this.departmentRepository = departmentRepository;
        }

        public async Task<List<SelectModel>> GetBusinessProfileDepartmentSelectItemsAsync(int businessProfileId)
        { 
            List<SelectModel> departments = await Task.Run(()=> 
                this.departmentRepository.GetAll().OfType<BusinessProfileDepartment>()
                .Where(t=>t.BusinessProfileId == businessProfileId)
                .Select(x => new SelectModel
                {
                    Id = x.Id,
                    Name = x.Name                    
                }).ToList());

            return departments;
        }
    }
}
