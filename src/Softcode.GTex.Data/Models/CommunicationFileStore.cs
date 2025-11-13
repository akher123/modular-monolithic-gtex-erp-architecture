using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class CommunicationFileStore
    {
        public int Id { get; set; }
        public int CommunicationId { get; set; }
        public int DocumentFileStoreId { get; set; }

        public Communication Communication { get; set; }
        public DocumentFileStore DocumentFileStore { get; set; }
    }
}
