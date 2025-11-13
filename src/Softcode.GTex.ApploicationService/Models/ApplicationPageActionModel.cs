namespace Softcode.GTex.ApploicationService.Models
{
    public class ApplicationPageActionModel
    {
        public int Id { get; set; }
        public int? PageId { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string NavigateUrl { get; set; }
        public string ActionName { get; set; }
        public int? SortOrder { get; set; }
        public int? RightId { get; set; }
    }
}