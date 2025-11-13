using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Softcode.GTex.Api.Hrm.Models;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Hrm;
using Softcode.GTex.ApplicationService.Hrm.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Hrm.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/hrm/employee")]
    public class EmployeeController : BaseController<EmployeeController>
    {
        private readonly IEmployeeService employeeService;
        private readonly IBusinessProfileService businessProfileService;
        private readonly ICustomCategoryService customCategoryService;
        private readonly IAddressDatabaseService addressDatabaseService;
        private readonly IBusinessCategoryService businessCategoryService;
        private readonly IBusinessUnitService businessUnitService;
        private readonly IDepartmentService departmentService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="businessProfileService"></param>
        /// <param name="customCategoryService"></param>
        /// <param name="businessCategoryService"></param>
        /// <param name="addressDatabaseService"></param>
        /// <param name="businessUnitService"></param>
        /// <param name="departmentService"></param>
        public EmployeeController(IEmployeeService employeeService
            , IBusinessProfileService businessProfileService
            , ICustomCategoryService customCategoryService
            , IBusinessCategoryService businessCategoryService
            , IAddressDatabaseService addressDatabaseService
            , IBusinessUnitService businessUnitService
            , IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.businessProfileService = businessProfileService;
            this.customCategoryService = customCategoryService;
            this.businessCategoryService = businessCategoryService;
            this.addressDatabaseService = addressDatabaseService;
            this.businessUnitService = businessUnitService;
            this.departmentService = departmentService;

        }

        /// <summary>
        /// GetEmployeeListAsync
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-employee-List")]
        [ActionAuthorize(ApplicationPermission.Employee.ShowEmployeeList)]
        public async Task<IActionResult> GetEmployeeListAsync(DataSourceLoadOptions loadOptions)
        {
            return Ok(new ResponseMessage<LoadResult> { Result = await employeeService.GetEmployeeListAsync(loadOptions) });
        }

        /// <summary>
        /// et-employee-page-tabs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-employee-page-tabs/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Employee.ViewEmployeeDetails)]
        public async Task<IActionResult> GetEmployeePageTabsAsync(int id)
        {

            return Ok(new ResponseMessage<TabPageViewModel>
            {
                Result = await Task.Run(() => new TabPageViewModel
                {
                    TabItems = employeeService.GetEmployeeDetailsTabs(id),
                    EntityType = ApplicationEntityType.Employee
                })
            });

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-employee-select-box-items/{businessProfileId:int}")]
        public async Task<IActionResult> GetEmployeeInitialDataAsync(int businessProfileId)
        {
            ResponseMessage<EmployeeViewModel> response = new ResponseMessage<EmployeeViewModel>
            {
                Result = new EmployeeViewModel
                {
                    ImTypeSelectItems = await customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.IMType, businessProfileId),


                    TitleSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.TitleType, businessProfileId).Result,
                    PositionSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PositionType, businessProfileId).Result,

                    SkillsSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.SkillType, businessProfileId).Result,
                    GenderSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.GenderType, businessProfileId).Result,
                    PreferredContactMethodSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.PreferredContactMethod, businessProfileId).Result,


                    //CostCentreSelectItems = costCentreService.GetCostCentreSelectItemsAsync(this.LoggedInUser.BusinessProfileId).Result,

                    EmploymentTypeSelectItems = customCategoryService.GetCustomCategoryListAsync(ApplicationCustomCategory.EmploymentType, businessProfileId).Result,
                    BusinessUnitSelectItems = businessUnitService.GetBusinessUnitSelectItemsAsync(businessProfileId).Result,
                    DepartmentSelectItems = departmentService.GetBusinessProfileDepartmentSelectItemsAsync(businessProfileId).Result,
                    RegionSelectItems = addressDatabaseService.GetRegionAsync(businessProfileId).Result
                }
            };
            return Ok(response);
        }

        /// <summary>
        /// get-employee-ById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-employee-by-id/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Employee.ViewEmployeeDetails)]
        public async Task<IActionResult> GetEmployeeByIdAsync(int id)
        {
            ResponseMessage<EmployeeViewModel> response = new ResponseMessage<EmployeeViewModel>
            {
                Result = new EmployeeViewModel
                {
                    Employee = await employeeService.GetEmployeeByIdAsync(id),
                    EmptyEmployee = employeeService.GetEmployeeByIdAsync(0).Result,

                    BusinessProfileSelectItems = businessProfileService.GetUserBusinessProfileSelectItemsAsync().Result,
                    TimezoneSelectItems = addressDatabaseService.GetTimeZoneSelectItemsAsync().Result,
                    PreferredPhoneTypeSelectItems = businessCategoryService.GetBusinessCategoryByType(ApplicationBusinessCategoryType.PreferredPhoneType),

                    IsDefaultBusinessProfile = LoggedInUser.IsDefaultBusinessProfile,


                }
            };
            return Ok(response);
        }


        /// <summary>
        /// create-employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-employee")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Employee.CreateEmployee)]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeModel employee)
        {
            ResponseMessage<int> response = new ResponseMessage<int>
            {
                Result = await employeeService.SaveEmployeeAsync(0, employee)
            };
            return Ok(response);
        }
        /// <summary>
        /// update-employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-employee/{id:int}")]
        [ModelValidation]
        [ActionAuthorize(ApplicationPermission.Employee.UpdateEmployee)]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeModel employee)
        {
            ResponseMessage<int> response = new ResponseMessage<int>
            {
                Result = await employeeService.SaveEmployeeAsync(id, employee)
            };
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessProfileId"></param>
        /// <param name="empId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-supervisor-list/{businessProfileId:int}/{empId:int}")]
        [ActionAuthorize(ApplicationPermission.Employee.DeleteEmployee)]
        public async Task<IActionResult> GetSupervisorListAsync(int businessProfileId, int empId)
        {
            return Ok(new ResponseMessage<List<SupervisorModel>>
            {
                Result = await employeeService.GetSupervisorListByBPAsync(businessProfileId, empId)
            });
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        [HttpDelete]
        [Route("delete-employee/{id:int}")]
        [ActionAuthorize(ApplicationPermission.Employee.DeleteEmployee)]
        public async Task<IActionResult> DeleteContactAsync(int id)
        {
            return Ok(await employeeService.DeleteEmployeeAsync(id));
        }
    }
}
