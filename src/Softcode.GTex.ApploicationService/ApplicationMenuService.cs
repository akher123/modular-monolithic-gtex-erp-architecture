using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Softcode.GTex.ExceptionHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Softcode.GTex.ApploicationService
{
    public class ApplicationMenuService : BaseService<ApplicationMenuService>, IApplicationMenuService
    {
        private readonly IRepository<ApplicationMenu> applicationMenuRepository;
        private readonly IRepository<ApplicationRoleRight> applicationRoleRightRepository;
        public ApplicationMenuService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<ApplicationMenu> applicationMenuRepository
            , IRepository<ApplicationRoleRight> applicationRoleRightRepository
            ) : base(applicationServiceBuilder)
        {
            this.applicationMenuRepository = applicationMenuRepository;
            this.applicationRoleRightRepository = applicationRoleRightRepository;
        }


        public async Task<List<ApplicationMenuModel>> GetApplicationMenuAsync()
        {
            List<ApplicationMenuModel> results = await Task.Run(() => GetApplicationMenu());
            return results;
        }

        public async Task<ApplicationHeaderModel> GetApplicationHeaderAsync()
        {
            return await Task.Run(() =>
            {
                ApplicationHeaderModel applicationHeader = new ApplicationHeaderModel();
                applicationHeader.Gender = this.LoggedInUser.Gender;
                applicationHeader.ImageSource = this.LoggedInUser.ImageSource;
                applicationHeader.DisplayName = $"{this.LoggedInUser.FirstName} {this.LoggedInUser.LastName}";

                return applicationHeader;
            });
        }

        private List<ApplicationMenuModel> GetApplicationMenu()
        {
            this.ApplicationCacheService.ClearApplicationMenu();
            List<ApplicationMenu> menu = this.ApplicationCacheService.GetApplicationMenu();

            if (LoggedInUser.IsSuperAdmin)
            {
                List<ApplicationMenu> superAdminMenu = menu.Where(m => m.ParentId == null)
                        .Select(s => new ApplicationMenu
                        {
                            Caption = s.Caption,
                            Entity = s.Entity,
                            EntityId = s.EntityId,
                            EntityRightId = s.EntityRightId,
                            Id = s.Id,
                            RowNo = s.RowNo,
                            ParentId = s.ParentId,
                            IsVisible = s.IsVisible,
                            Name = s.Name,
                            NavigateUrl = s.NavigateUrl,
                            ImageSource = s.ImageSource,
                            InverseParent = GetChildren(menu.Where(m => m.ParentId == s.Id), s.Id)
                        }).ToList();
                return Mapper.Map<List<ApplicationMenuModel>>(superAdminMenu);
            }

            var roleRights = this.ApplicationCacheService.GetApplicationRoleRight();

            var userRoles = this.LoggedInUser.DefaultBusinessProfileRoleIds;

            var userMenu = (from m in menu
                            join r in roleRights.Where(r => userRoles.Contains(r.RoleId)).Distinct() on m.EntityRightId equals r.RightId into rr
                            from r in rr.DefaultIfEmpty()
                            orderby m.RowNo
                            where m.ParentId == null || r != null
                            select m).Distinct();

            List<ApplicationMenu> appMenu = userMenu.Where(m => m.ParentId == null)
                        .Select(s => new ApplicationMenu
                        {
                            Caption = s.Caption,
                            Entity = s.Entity,
                            EntityId = s.EntityId,
                            EntityRightId = s.EntityRightId,
                            Id = s.Id,
                            RowNo = s.RowNo,
                            ParentId = s.ParentId,
                            IsVisible = s.IsVisible,
                            Name = s.Name,
                            NavigateUrl = s.NavigateUrl,
                            ImageSource = s.ImageSource,
                            InverseParent = GetChildren(userMenu.Where(m => m.ParentId == s.Id), s.Id)
                        }).ToList().Where(x => x.InverseParent.Count > 0).ToList();


            var results = Mapper.Map<List<ApplicationMenuModel>>(appMenu);
            return results;
        }

        private List<ApplicationMenu> GetChildren(IEnumerable<ApplicationMenu> userMenu, int? parentId)
        {
            var child = userMenu
                    .Where(c => c.ParentId == parentId)
                    .Select(s => new ApplicationMenu
                    {
                        Caption = s.Caption,
                        Entity = s.Entity,
                        EntityId = s.EntityId,
                        EntityRightId = s.EntityRightId,
                        Id = s.Id,
                        Name = s.Name,
                        RowNo = s.RowNo,
                        ParentId = s.ParentId,
                        IsVisible = s.IsVisible,
                        NavigateUrl = s.NavigateUrl,
                        ImageSource = s.ImageSource,
                        InverseParent = GetChildren(userMenu.Where(m => m.ParentId == s.Id), s.Id)
                    })
                    .ToList();

            return child;
        }



        public async Task<int> CreateApplicationMenuAsync(ApplicationMenuModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new SoftcodeArgumentMissingException("Menu Name is missing");
            }
            if (string.IsNullOrWhiteSpace(model.Caption))
            {
                throw new SoftcodeArgumentMissingException("Caption is missing");
            }

            if (applicationMenuRepository.Exists(x => x.Name == model.Name))
            {
                throw new SoftcodeInvalidDataException("Menu Name already exist");
            }
            if (applicationMenuRepository.Exists(x => x.Caption == model.Caption))
            {
                throw new SoftcodeInvalidDataException("Caption already exist");
            }
            ApplicationMenu applicationMenu = Mapper.Map<ApplicationMenu>(model);
            applicationMenu.CreatedByContactId = LoggedInUser.ContectId;
            applicationMenu.CreatedDateTime = DateTime.Now;

            if (model.ParentId != null||model.ParentId>0)
            {
                int moduleFeatureId= applicationMenuRepository
                    .AsQueryable()
                    .ToList()
                    .Where(x => x.Id.ToString()
                    .Contains(model.ParentId.GetValueOrDefault()
                    .TakeNDigits(2).ToString())).Max(x=>x.Id);
                moduleFeatureId +=1;
                applicationMenu.Id = moduleFeatureId;
            }
            else
            {
                int moduleFeatureId = applicationMenuRepository
                  .AsQueryable().Max(x => x.Id);
                moduleFeatureId += 1; ;
                applicationMenu.Id = moduleFeatureId;
                applicationMenu.ParentId = null;
            }
            await applicationMenuRepository.CreateAsync(applicationMenu);
            return applicationMenu.Id;
        }

        public async Task<int> UpdateApplicationMenuAsync(int id , ApplicationMenuModel model)
        {
            ApplicationMenu applicationMenu = applicationMenuRepository.FindOne(x=>x.Id==id);
            applicationMenu.ParentId = model.ParentId;
            applicationMenu.Name = model.Name;
            applicationMenu.Caption = model.Caption;
            applicationMenu.RowNo = model.RowNo;
            applicationMenu.NavigateUrl = model.NavigateUrl;
            applicationMenu.ImageSource = model.ImageSource;
            applicationMenu.IsVisible = model.IsVisible;
            await applicationMenuRepository.UpdateAsync(applicationMenu);
            return applicationMenu.Id;
        }

        public async Task<List<TreeModel>> ApplicationMenuTreeListAsync()
        {
            IQueryable<ApplicationMenu> menu = applicationMenuRepository.AsQueryable();
            List<TreeModel> applicationMenuTee = new List<TreeModel>();
            if (LoggedInUser.IsSuperAdmin)
            {
                applicationMenuTee = await menu.AsQueryable()
                        .Select(s => new TreeModel
                        {
                            SId = s.Id.ToString(),
                            SParentId = s.ParentId.ToString(),
                            Name = s.Caption,
                            IsExpanded = true,
                           
                        }).ToListAsync();
            }
            return applicationMenuTee;
        }

   
    }
}
