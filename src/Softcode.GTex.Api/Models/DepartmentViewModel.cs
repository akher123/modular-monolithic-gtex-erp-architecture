using Softcode.GTex.ApploicationService.Models;
using System.Collections.Generic;

namespace Softcode.GTex.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DepartmentViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public DepartmentModel Department { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DepartmentModel> Departments { get; set; }
    }
}
