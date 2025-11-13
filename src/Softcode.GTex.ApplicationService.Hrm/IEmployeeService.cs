using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApplicationService.Hrm.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Hrm
{
    public interface IEmployeeService
    {
        #region Get List
        Task<LoadResult> GetEmployeeListAsync(DataSourceLoadOptionsBase options);
        
        #endregion

        #region Get Single Entity
        List<TabModel> GetEmployeeDetailsTabs(int id);
        Task<EmployeeModel> GetEmployeeByIdAsync(int id);
        #endregion

        #region CUD
        Task<int> SaveEmployeeAsync(int id, EmployeeModel model);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> DeleteEmployeesAsync(List<int> ids);
        Task<List<SupervisorModel>> GetSupervisorListByBPAsync(int businessProfileId, int empId);
        
        #endregion
    }
}
