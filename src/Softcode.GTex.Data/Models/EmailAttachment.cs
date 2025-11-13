using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class EmailAttachment
    {
        public int Id { get; set; }
        public int EmailJobQueueId { get; set; }
        public string FileName { get; set; }        
        public string FilePath { get; set; }
        public int? DocumentMetadataId { get; set; }
        public int? VersionNumber { get; set; }
        

        public DocumentMetadata DocumentMetadata { get; set; }
        public EmailJobQueue EmailJobQueue { get; set; }
    }
}
