using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IUserService
    {
        //Task<LoadResult> GetActiveUsersAsync(Guid? businessProfileId, DataSourceLoadOptionsBase options);
        Task<LoadResult> GetUserListAsync( DataSourceLoadOptionsBase options);
        //List<SelectModel> GetUserTypeSelectItems();
        Task<ApplicationUserModel> GetUserModelByIdAsync(string id);
        Task<string> InsertUserDetailAsync(ApplicationUserModel userModel);
        Task<string> UpdateUserDetailAsync(string id, ApplicationUserModel userModel);
        Task<bool> DeleteUsersAsync(List<string> ids);
        Task<bool> DeleteUserByIdAsync(string id);
        bool ValidateUserEmail(string id, string email);
        bool ValidateUserName(string id, string username);
        Task<bool> ChangeUserPasswordAsync(string currentPassword, string newPassword);
    }
}
