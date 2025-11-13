using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.ApploicationService
{
    public class ApplicationPageService : BaseService<ApplicationPageService>, IApplicationPageService
    {
        private readonly IRepository<ApplicationPage> applicationPageRepository;
        private readonly IApplicationCacheService applicationCacheService;
        public ApplicationPageService(
            IApplicationCacheService applicationCacheService,
            IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<ApplicationPage> applicationPageRepository) : base(applicationServiceBuilder)
        {
            this.applicationCacheService = applicationCacheService;
            this.applicationPageRepository = applicationPageRepository;
        }


        public async Task<ApplicationListPageModel> GetApplicationListPageByNameAsync(string name)
        {
            //var page = await applicationPageRepository.Where(t => t.Name == name)
            //                    .Include(p => p.ApplicationPageListFields)
            //                    .Include(p => p.ApplicationPageServices)
            //                    .Include(p => p.ApplicationPageNavigations)
            //                    .FirstOrDefaultAsync();


            ApplicationPage page = await Task.Run(() => this.applicationCacheService.GetApplicationListPages()
                                                    .Where(t => t.Name == name)
                                                    .FirstOrDefault()
                                                    );
            if (page == null)
            {
                throw new SoftcodeNotFoundException("Application page not found");
            }

            return BuildListPageModel(page);
        }

        public async Task<ApplicationListPageModel> GetApplicationListPageByRoutingUrlAsync(string routingUrl)
        {
            var page = await applicationPageRepository.Where(t => t.RoutingUrl == routingUrl)
                                .Include(p => p.ApplicationPageListFields)
                                .Include(p => p.ApplicationPageServices)
                                .Include(p => p.ApplicationPageNavigations)
                                .FirstOrDefaultAsync();

            if (page == null)
            {
                throw new SoftcodeNotFoundException("Application page not found");
            }

            return BuildListPageModel(page);
        }

        private ApplicationListPageModel BuildListPageModel(ApplicationPage page)
        {
            var pagemodel = new ApplicationListPageModel();
            pagemodel.Id = page.Id;
            pagemodel.Name = page.Name;
            pagemodel.Title = page.Title;
            pagemodel.RoutingUrl = page.RoutingUrl;
            pagemodel.PageType = page.PageType;

            pagemodel.Fields = page.ApplicationPageListFields.OrderBy(f => f.RowNo)
                               .Select(f => new
                               {
                                   dataField = f.Name,
                                   cellTemplate = f.CellTemplate,
                                   caption = f.Caption,
                                   visible = f.Visible,
                                   //placeholder = "Search...",
                                   dataType = f.DataType,
                                   format = f.Format,
                                   allowFiltering = f.RowFilterEnabled,
                                   alignment = f.Alignment,
                                   sortOrder = f.DefaultSortOrder
                               });

            pagemodel.PageServiceUrls = page.ApplicationPageServices
                                        .Select(f => new
                                        {
                                            f.ServiceName,
                                            f.ServiceType,
                                            f.ServiceUrl
                                        });

            pagemodel.PageNavigationUrls = page.ApplicationPageNavigations
                                           .Select(s => new
                                           {
                                               s.LinkName,
                                               s.NavigateUrl
                                           });

            return pagemodel;
        }

        public async Task<ApplicationDetailPageModel> GetApplicationDetailPageByNameAsync(string name)
        {
            var page = await applicationPageRepository.Where(t => t.Name == name)
                               .Include(p => p.ApplicationPageDetailFields)
                               .Include(p => p.ApplicationPageServices)
                               .Include(p => p.ApplicationPageNavigations)
                               .Include(p => p.ApplicationPageGroups)
                               .FirstOrDefaultAsync();

            if (page == null)
            {
                throw new SoftcodeNotFoundException("Application page not found");
            }

            return BuildDetailPageModel(page);
        }

        public async Task<ApplicationDetailPageModel> GetApplicationDetailPageByRoutingUrlAsync(string routingUrl)
        {
            var page = await applicationPageRepository.Where(t => t.RoutingUrl == routingUrl)
                               .Include(p => p.ApplicationPageGroups).ThenInclude(p => p.ApplicationPageDetailFields)
                               .Include(p => p.ApplicationPageServices)
                               .Include(p => p.ApplicationPageNavigations)
                               .Include(p => p.ApplicationPageActions)
                               .FirstOrDefaultAsync();

            if (page == null)
            {
                throw new SoftcodeNotFoundException("Application page not found");
            }

            return BuildDetailPageModel(page);
        }

        private ApplicationDetailPageModel BuildDetailPageModel(ApplicationPage page)
        {
            var pagemodel = new ApplicationDetailPageModel();
            pagemodel.Id = page.Id;
            pagemodel.Name = page.Name;
            pagemodel.Title = page.Title;
            pagemodel.RoutingUrl = page.RoutingUrl;
            pagemodel.PageType = page.PageType;

            pagemodel.FieldGroups = page.ApplicationPageGroups.OrderBy(f => f.SortOrder)
                               .Select(g => new
                               {
                                   name = g.Name,
                                   caption = g.Caption,
                                   fields = g.ApplicationPageDetailFields.OrderBy(f => f.SortOrder).Select(f => new
                                   {
                                       dataField = f.Name,
                                       controlTypeId = f.ControlTypeId,
                                       caption = f.Caption,
                                       mappingField = f.MappingField,
                                       dataType = f.DataType,
                                       format = f.Format,
                                       visible = f.Visible,
                                       readOnly = f.ReadOnly,
                                       required = f.Required,
                                       isUnique = f.IsUnique,
                                       sortOrder = f.SortOrder,
                                       minValue = f.MinValue,
                                       maxValue = f.MaxValue,
                                       dataSourceUrl = f.DataSourceUrl,
                                       dataSourceName = f.DataSourceName,
                                       alignment = f.Alignment,
                                       rightId = f.RightId
                                   })
                               });

            pagemodel.PageServiceUrls = page.ApplicationPageServices
                                        .Select(f => new
                                        {
                                            f.ServiceName,
                                            f.ServiceType,
                                            f.ServiceUrl
                                        });

            pagemodel.PageNavigationUrls = page.ApplicationPageNavigations
                                           .Select(s => new
                                           {
                                               s.LinkName,
                                               s.NavigateUrl
                                           });

            pagemodel.PageActions = page.ApplicationPageActions.OrderBy(a => a.SortOrder)
                                         .Select(s => new
                                         {
                                             s.Name,
                                             s.Caption,
                                             s.RightId,
                                             s.ActionName,
                                             s.NavigateUrl,
                                             s.SortOrder,
                                         });

            return pagemodel;
        }

        public async Task<ApplicationPageModel> GetApplicationListPageByIdAsync(int id)
        {
            ApplicationPage page =  await  this.applicationPageRepository.Where(x=>x.Id==id)
                               .Include(p => p.ApplicationPageListFields)
                               .Include(p => p.ApplicationPageServices)
                               .Include(p => p.ApplicationPageNavigations)
                               .Include(p => p.ApplicationPageActions)
                               .FirstOrDefaultAsync();


            ApplicationPageModel model = new ApplicationPageModel();
            this.Mapper.Map<ApplicationPage, ApplicationPageModel>(page, model);

            return model;

        }

        public Task<ApplicationPageModel> GetApplicationDetailPageByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
