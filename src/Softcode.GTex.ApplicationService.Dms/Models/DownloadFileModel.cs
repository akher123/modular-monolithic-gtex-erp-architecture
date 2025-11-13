namespace Softcode.GTex.ApplicationService.Dms.Models
{
    public class DownloadFileModel
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] File { get; set; }
    }
}
