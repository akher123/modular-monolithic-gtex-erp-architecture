using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Dms.Models
{
    public class DocumentFileStoreModel
    {
        public int Id { get; set; }
        public int StorageTypeId { get; set; }
        public Guid? FileId { get; set; }
        public string FilePath { get; set; }
        public int VersionNumber { get; set; }
    }
}
