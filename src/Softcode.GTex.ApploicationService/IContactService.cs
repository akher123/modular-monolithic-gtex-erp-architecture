using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IContactService
    {
        Task<LoadResult> GetContactListAsync(DataSourceLoadOptionsBase options);
        Task<ContactModel> GetContactForUserByContactIdAsync(int id);
        Task<ContactModel> GetContactModelByIdAsync(int id);
        List<TabModel> GetContactDetailsTabs(int id);
       
        Task<int> SaveContactDetailsAsync(ContactModel contactModel);
        Task<bool> DeleteContactAsync(int id);
        Task<bool> DeleteContactAsync(List<int> ids);
        List<SelectModel> ContactSelectItems();
        List<SelectModel> GetGenderTypeSelectedItem();
        List<SelectModel> GetPreferredPhoneTypeSelectedItem();
        Task<List<SelectModel>> GetContactSelectItemsByBusinessProfileIdAsync(int businessProfileId);
        Task<List<SelectModel>> GetContactSelectItemsByBPIdAndContactTypeAsync(int businessProfileId, int contactTypeId );
        //List<SelectModel> GetContactSelectItemsAsync(List<int> businessprofileIds);
        Task<List<SelectModel>> GetContactSelectItemsForUserAsync(List<int> businessprofileIds);
        Task<List<SelectModel>> GetEmployeeSelectItemsForUserAsync(List<int> businessprofileIds);
    }
}
