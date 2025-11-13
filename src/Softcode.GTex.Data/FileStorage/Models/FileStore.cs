using System;

namespace Softcode.GTex.Data.FileStorage.Models
{
    public partial class FileStore
    {
        public Guid Id { get; set; }
        public byte[] FileContent { get; set; }
        public byte[] TimeStamp { get; set; }
    }
}
