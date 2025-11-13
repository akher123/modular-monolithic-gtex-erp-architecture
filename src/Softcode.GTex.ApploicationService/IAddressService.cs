using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IAddressService
    {

        #region Get List
        Task<LoadResult> GetAddressListByEntityIdAsync(Guid uniqueEntityId, int entityId, DataSourceLoadOptionsBase options);
        Task<List<AddressModel>> GetAddressModelListByEntityIdAsync(Guid uniqueEntityId, int entityId);
        #endregion

        #region Get Single Entity
        Task<AddressModel> GetAddressByIdAsync(Guid uniqueEntityId, int id);
        #endregion

        #region CUD
        Task<int> SaveAddressAsync(int id, AddressModel model);
        Task<bool> DeleteAddressAsync(int id);
        Task<bool> DeleteAddresssAsync(List<int> ids);
        #endregion
    }
}
