using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApplicationService.Dms.Models
{
    public class AttachedFileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string OrginalFileName { get; set; }
        public string MimeType { get; set; }
        public int FileSizeInKb { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }

        public string CreatedBy { get; set; }

        public int Version { get; set; } = 1;

        public bool IsDirty { get; set; } = false;
    }
}
