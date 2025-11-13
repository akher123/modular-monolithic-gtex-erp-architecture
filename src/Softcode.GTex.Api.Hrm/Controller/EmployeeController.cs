using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Softcode.GTex.Api.Hrm.Models;
using Softcode.GTex.Api.Models;
using Softcode.GTex.Api.Providers;
using Softcode.GTex.ApplicationService.Hrm.Interfaces;
using Softcode.GTex.ApplicationService.Hrm.Models;
using Softcode.GTex.ApploicationService;
using Softcode.GTex.ExceptionHelper;
using System.Threading.Tasks;

namespace Softcode.GTex.Api.Hrm.Controller
{

    /// <summary>
    /// Employee Api controller
    /// </summary>
    [Route(HrmRoutePrefix.Employees)]
    public class EmployeeController : BaseController<EmployeeController>
    {
        private readonly IEmployeeService employeeService;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ICustomCategoryService customCategoryService;
        /// <summary>
        /// Employee ApiController constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="employeeService"></param>
        /// <param name="loggedInUserService"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="customCategoryService"></param>
        public EmployeeController(
             IEmployeeService employeeService
            
            , ICustomCategoryService customCategoryService) 
        {
            this.employeeService = employeeService;
            
        }

        /// <summary>
        /// Employee Dev expresss data model
        /// </summary>
        /// <param name="loadOptions">Dev expresss data model</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-employees")]
        public async Task<IActionResult> GetEmployeesAsync(DataSourceLoadOptions loadOptions)
        {
            throw new SoftcodeInvalidDataException("invalid data");
            return Ok(new ResponseMessage<LoadResult>
            {
                Result = await employeeService.GetEmployeesAsync(loadOptions)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Employee unique identifire</param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-employee/{id:int}")]
        public async Task<IActionResult> GetEmployeeAsync(int id)
        {
            return Ok(new ResponseMessage<EmployeeViewModel>
            {
                Result = new EmployeeViewModel
                {
                    Employee = await employeeService.GetEmployeeAsync(id),
                    //ReligionSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.CommunicationStatus).Result,
                    //BloodGroupSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.BloodGroup).Result,
                    //NationalitySelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.Nationality).Result,
                    //TerminationTypeSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.TerminationType).Result,
                    //EmployeeStatusSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.EmployeeStatus).Result,
                    //GenderSelectItems = customCategoryService.GetCustomCategoryListByTypeIdAsync(ApplicationCustomCategory.Gender).Result,
                }
            });
        }
        /// <summary>
        /// Get Employee Details Async
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("get-employee-details/{id:int}")]
        public async Task<IActionResult> GetEmployeeDetailsAsync(int id)
        {
            return Ok(new ResponseMessage<TabPageViewModel>
            {
                Result = await Task.Run(() => new TabPageViewModel
                {
                    TabItems = employeeService.GetEmployeeDetailsTabs(id),
                })
            });
        }

        /// <summary>
        /// Cerate employee post action method
        /// </summary>
        /// <param name="employee">EmployeeModel</param>
        /// <returns></returns>
        [HttpPost]
        [Route("create-employee")]
        [ModelValidation]
        public async Task<IActionResult> CreateEmployeeCreateAsync([FromBody] EmployeeModel employee)
        {
            return Ok(new ResponseMessage<int>()
            {
                Result = await employeeService.SaveEmployeeAsync(0, employee)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("update-employee/{id:int}")]
        [ModelValidation]
        public async Task<IActionResult> UpdateEmployeeCreateAsync(int id, [FromBody] EmployeeModel employee)
        {
           
            return Ok(new ResponseMessage<int>()
            {
                Result = await employeeService.SaveEmployeeAsync(id, employee)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete-employee/{id:int}")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            return Ok(new ResponseMessage<bool>()
            {
                Result = await employeeService.DeleteEmployeeAsync(id)
            });
        }
    }
}
