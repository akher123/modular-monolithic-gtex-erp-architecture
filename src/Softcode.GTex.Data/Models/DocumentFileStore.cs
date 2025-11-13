using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class DocumentFileStore : ITrackable
    {
        public DocumentFileStore()
        {
            CommunicationFileStores = new HashSet<CommunicationFileStore>();
        }

        public int Id { get; set; }
        public int DocumentMetadataId { get; set; }
        public int StorageTypeId { get; set; }
        public Guid? FileId { get; set; }
        public string FilePath { get; set; }
        public string MimeType { get; set; }
        public string FileName { get; set; }
        public string OrginalFileName { get; set; }
        public int FileSizeInKb { get; set; }
        public string Extension { get; set; }
        public int VersionNumber { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Publickey { get { return Encryption.ConvertStringToHex(FileId.ToString() +"#"+ Id.ToString(), System.Text.Encoding.Unicode); } }

        public Contact CreatedByContact { get; set; }
        public DocumentMetadata DocumentMetadata { get; set; }
        public Contact LastUpdatedByContact { get; set; }

        public ICollection<CommunicationFileStore> CommunicationFileStores { get; set; }
    }
}
