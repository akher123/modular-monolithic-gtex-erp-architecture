using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public interface IBusinessCategoryService
    {
        List<SelectModel> GetBusinessCategoryByType(int typeId);
        Task<List<SelectModel>> GetBusinessCategoryByTypeAsync(int typeId);
    }
}
