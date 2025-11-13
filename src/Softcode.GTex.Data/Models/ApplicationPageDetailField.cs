using System;
using System.Collections.Generic;

namespace Softcode.GTex.Data.Models
{
    public partial class ApplicationPageDetailField: ITrackable
    {
        public int Id { get; set; }
        public int PageId { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string MappingField { get; set; }
        //public string CellTemplate { get; set; }
        public int? ControlTypeId { get; set; }
        public string DataType { get; set; }
        public string Format { get; set; }
        public bool? Visible { get; set; }
        public bool? ReadOnly { get; set; }
        public bool Required { get; set; }
        public bool IsUnique { get; set; }
        //public bool? ColumnFilterEnabled { get; set; }
        //public bool? RowFilterEnabled { get; set; }
        //public bool? SortEnabled { get; set; }
        //public string DefaultSortOrder { get; set; }
        public string Alignment { get; set; }
        public int SortOrder { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public int? Length { get; set; }
        public string DataSourceUrl { get; set; }
        public string DataSourceName { get; set; }
        public int? RightId { get; set; }
        public int? ApplicationPageGroupId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedByContactId { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public int? LastUpdatedByContactId { get; set; }

        public ApplicationPageGroup ApplicationPageGroup { get; set; }
        public Contact CreatedByContact { get; set; }
        public Contact LastUpdatedByContact { get; set; }
        public ApplicationPage Page { get; set; }
    }
}
