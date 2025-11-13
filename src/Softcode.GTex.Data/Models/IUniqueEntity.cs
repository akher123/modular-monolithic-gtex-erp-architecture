using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.Data.Models
{
    public interface IUniqueEntity
    {
        Guid UniqueEntityId { get; set; }
        /// <summary>
        ///  [NotMapped] Field EntityTypeId { get; set; } = Configuration.ApplicationEntityType...;
        /// </summary>
        int EntityTypeId { get; set; }
        int Id { get; set; }
        RecordInfo RecordInfo { get; set; }
    }
}
