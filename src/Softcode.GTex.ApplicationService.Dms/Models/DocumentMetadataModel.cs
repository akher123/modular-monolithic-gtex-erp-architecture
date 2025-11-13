using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Softcode.GTex.ApplicationService.Dms.Models
{
    public partial class DocumentMetadataModel
    {
        public DocumentMetadataModel() {
            files = new List<AttachedFileModel>();
            Revisions = new List<AttachedFileModel>();
        }
        public int Id { get; set; }
        [MaxLength(200, ErrorMessage = "Maximum length of title is 200 characters.")]
        public string Title { get; set; }
        public int? EntityTypeId { get; set; }
        public int? EntityId { get; set; }
        [MaxLength(400, ErrorMessage = "Maximum length of description is 400 characters.")]
        public string Description { get; set; }
        [MaxLength(200, ErrorMessage = "Maximum length of file name is 200 characters.")]
        public string FileName { get; set; }
        [MaxLength(200, ErrorMessage = "Maximum length of orginal file name is 200 characters.")]
        public string OrginalFileName { get; set; }
        public int? DocumentTypeId { get; set; }
        public int FileSizeInKb { get; set; }
        public Guid? FileId { get; set; }
        public bool IsInternal { get; set; }
        public bool IsArchived { get; set; }
        public int BusinessProfileId { get; set; }
        [MaxLength(400, ErrorMessage = "Maximum length of description is 400 characters.")]
        public string Keywords { get; set; }
        public string MimeType { get; set; }
        public bool IsPublic { get; set; }
        public byte[] File { get; set; }
        public List<AttachedFileModel> files { get; set; }
        public List<AttachedFileModel> Revisions { get; set; }
    }
}
