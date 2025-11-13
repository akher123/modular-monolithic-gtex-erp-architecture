using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface ICommunicationService
    {
        Task<LoadResult> GetCommunicationListAsync(DataSourceLoadOptionsBase options);
        Task<CommunicationModel> GetCommunicationModelByIdAsync(int id);
        Task<bool> DeleteCommunicationAsync(int id);
        Task<int> SaveCommunicationAsync(int id, CommunicationModel communicationModel);
    }
}
