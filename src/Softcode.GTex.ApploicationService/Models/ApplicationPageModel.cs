using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService.Models
{
    public class ApplicationPageModel
    {
        public ApplicationPageModel()
        {
            ApplicationPageGroups = new List<ApplicationPageGroupModel>();
            ApplicationPageListFields = new List<ApplicationPageListFieldModel>();
            ApplicationPageNavigations = new List<ApplicationPageNavigationModel>();
            ApplicationPageServices = new List<ApplicationPageServiceModel>();
            ApplicationPageActions = new List<ApplicationPageActionModel>();
        }
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string RoutingUrl { get; set; }
        public string Title { get; set; }
        public string PageType { get; set; }

        public List<ApplicationPageGroupModel> ApplicationPageGroups { get; set; }
        public List<ApplicationPageListFieldModel> ApplicationPageListFields { get; set; }
        public List<ApplicationPageNavigationModel> ApplicationPageNavigations { get; set; }
        public List<ApplicationPageServiceModel> ApplicationPageServices { get; set; }
        public List<ApplicationPageActionModel> ApplicationPageActions { get; set; }
    }

     
}
