using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApplicationService.Crm.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Crm
{
    public interface ICompanyService
    {
        Task<LoadResult> GetCompanyListAsync(DataSourceLoadOptionsBase options);
        //Task<LoadResult> GetCompanyListByBPIdAsync(int businessProfileId, DataSourceLoadOptionsBase options);
        Task<CompanyModel> GetCompanyByIdAsync(int id);
        Task<List<SelectModel>> GetCompanySelectItemsAsync();
        Task<List<SelectModel>> GetCompanySelectItemsByBPIdAsync(List<int> businessProfileIds);
        Task<List<SelectModel>> GetCompanySelectItemsByBPIdAsync(int businessProfileId);

        Task<int> SaveCompanyDetailsAsync(int id, CompanyModel model);
        Task<int> DeleteCompanyAsync(int id);
        Task<bool> DeleteCompaniesAsync(List<int> ids);
        List<TabModel> GetCompanyDetailsTabs(int id);
        //Task<bool> SendEmailAsync();

    }
}
