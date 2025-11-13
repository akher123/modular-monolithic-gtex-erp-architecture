using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Softcode.GTex.ApploicationService.Models;
using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softcode.GTex.ExceptionHelper;

namespace Softcode.GTex.ApploicationService
{
    public class CustomCategoryService : BaseService<CustomCategoryService>, ICustomCategoryService
    {
        private readonly IRepository<CustomCategory> customCategoryRepository;
        private readonly IRepository<CustomCategoryType> customCategoryTypeRepository;
        private readonly IRepository<EntityType> entityTypeRepository;



        public CustomCategoryService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<CustomCategoryType> customCategoryTypeRepository
            , IRepository<CustomCategory> customCategoryRepository
            , IRepository<EntityType> entityTypeRepository
            ) : base(applicationServiceBuilder)
        {
            this.customCategoryTypeRepository = customCategoryTypeRepository;
            this.customCategoryRepository = customCategoryRepository;
            this.entityTypeRepository = entityTypeRepository;
        }


        public async Task<List<CustomCategoryModuleModel>> GetCustomCategoryModuleListAsync()
        {
            List<CustomCategoryType> categoryTypes = null;
            if (this.LoggedInUser.IsSuperAdmin)
            {
                categoryTypes = await Task.Run(() => this.ApplicationCacheService.GetCustomCategoryType().ToList());
            }
            else
            {
                categoryTypes = await Task.Run(() => this.ApplicationCacheService.GetCustomCategoryType()
                                                   .Where(x => this.Permission.DefaultBusinessProfileRights.Contains(x.RightId)
                                               ).ToList());
            }

            List<CustomCategoryTypeModel> categoryTypemodels = Mapper.Map<List<CustomCategoryTypeModel>>(categoryTypes);

            return categoryTypemodels.OrderBy(m => m.RowNo).GroupBy(m => m.ModuleName).Select(m => new CustomCategoryModuleModel
            {
                Name = m.Key,
                CustomCategoryTypes = m.Select(c => new CustomCategoryTypeModel
                {
                    Name = c.Name,
                    RoutingKey = c.RoutingKey,
                    HelpText = c.HelpText,
                    CustomCategoryMapTypeId = c.CustomCategoryMapTypeId,
                    Id = c.Id,
                    CustomCategories = c.CustomCategories,
                    ImageSource = c.ImageSource,
                    ModuleName = c.ModuleName,
                    RightId = c.RightId,
                    RowNo = c.RowNo
                }).ToList()
            }).ToList();
        }

        public async Task<LoadResult> GetCustomCategoryListByRoutingKeyAsync(string routingKey, DataSourceLoadOptionsBase options)
        {
            int rightId = this.ApplicationCacheService.GetCustomCategoryType().Where(x => x.RoutingKey == routingKey).FirstOrDefault().RightId;

            if (!this.Permission.IsValid(rightId))
            {
                throw new SoftcodeUnauthorizedException("You are not authorized to perform this action.");
            }

            IQueryable<CustomCategory> ccQuery = customCategoryRepository.Where(x => x.CustomCategoryType != null && x.CustomCategoryType.RoutingKey == routingKey);

            if (!LoggedInUser.IsSuperAdmin)
            {
                int businessProfileId = LoggedInUser.DefaultBusinessProfileId;
                ccQuery = ccQuery.Where(x => x.BusinessProfileId == null || x.BusinessProfileId == businessProfileId);
            }

            var query = ccQuery.Include(c => c.BusinessProfile)
                    .Include(c => c.CustomCategoryMapTypeOption)
                    .Include(c => c.CreatedByContact)
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Code,
                        BusinessProfile = c.BusinessProfile.CompanyName,
                        c.Desciption,
                        MapTypeOptionName = c.CustomCategoryMapTypeOption.Name,
                        c.DisplayOrder,
                        c.IsDefault,
                        c.IsActive,
                        CreatedByContactName = c.CreatedByContact.FirstName + " " + c.CreatedByContact.LastName,
                        c.CreatedDateTime
                    });

            return await Task.Run(() => DataSourceLoader.Load(query, options));
        }

        public async Task<List<SelectModel>> GetMapTypeSelectListAsync(string routingKey)
        {
            var catType = await Task.Run(() => customCategoryTypeRepository.Where(t => t.RoutingKey == routingKey)
                .Include(m => m.CustomCategoryMapType).ThenInclude(m => m.CustomCategoryMapTypeOptions).FirstOrDefault());
            if (catType?.CustomCategoryMapTypeId > 0)
            {
                List<SelectModel> models = catType.CustomCategoryMapType.CustomCategoryMapTypeOptions.Select(x => new SelectModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

                return models;
            }
            return new List<SelectModel>();
        }
        public async Task<List<SelectModel>> GetCustomCategoryListAsync(int categoryTypeId)
        {
            int businessProfileId = this.LoggedInUser.DefaultBusinessProfileId;
            bool isDefaultBusinessProfileUser = this.LoggedInUser.IsDefaultBusinessProfile;
            return await Task.Run(() => this.ApplicationCacheService.GetCustomCategoryType().Where(x=>x.Id==categoryTypeId)
                                            .SelectMany(x=>x.CustomCategories)
                                            .Where(t => t.CustomCategoryTypeId == categoryTypeId && t.IsActive
                                                       && ((t.BusinessProfileId == null || t.BusinessProfileId == businessProfileId) || isDefaultBusinessProfileUser))
                                            .OrderBy(t => t.DisplayOrder)
                .Select(t => new SelectModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    BusinessProfileId = t.BusinessProfileId,
                    IsDefault = t.IsDefault
                }).ToList());
        }

        public async Task<List<SelectModel>> GetCustomCategoryListAsync(int categoryTypeId, int businessProfileId)
        {
            return await Task.Run(() => this.ApplicationCacheService.GetCustomCategoryType().Where(x => x.Id == categoryTypeId)
                                            .SelectMany(x => x.CustomCategories)
                                            .Where(t => t.CustomCategoryTypeId == categoryTypeId && t.IsActive
                                                       && ((t.BusinessProfileId == null || t.BusinessProfileId == businessProfileId)))
                                            .OrderBy(t => t.DisplayOrder)
                .Select(t => new SelectModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    BusinessProfileId = t.BusinessProfileId,
                    IsDefault = t.IsDefault
                }).ToList());
        }

        public async Task<List<SelectModel>> GetCustomCategoryListAsync(int categoryTypeId, int[] businessProfileIds)
        {
            return await Task.Run(() => this.ApplicationCacheService.GetCustomCategoryType().Where(x => x.Id == categoryTypeId)
                                            .SelectMany(x => x.CustomCategories)
                                            .Where(t => t.CustomCategoryTypeId == categoryTypeId && t.IsActive
                                                       && ((t.BusinessProfileId == null || businessProfileIds.Contains(t.BusinessProfileId.Value))))
                                            .OrderBy(t => t.DisplayOrder)
                .Select(t => new SelectModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    BusinessProfileId = t.BusinessProfileId,
                    IsDefault = t.IsDefault
                }).ToList());
        }

        //public async Task<List<CustomCategoryTypeModel>> GetCustomCategoryTypeListAsync(List<int> ids)
        //{
        //    int businessProfileId = this.LoggedInUser.DefaultBusinessProfileId;
        //    bool isDefaultBusinessProfileUser = this.LoggedInUser.IsDefaultBusinessProfile;
        //    return await Task.Run(() => this.customCategoryTypeRepository
        //        .Where(x => ids.Contains(x.Id))
        //        .Include(t => t.CustomCategories)
        //        .Select(x => new CustomCategoryTypeModel
        //        {
        //            Id = x.Id,
        //            Name = x.Name,
        //            CustomCategories = x.CustomCategories
        //                .Where(c => c.IsActive && ((c.BusinessProfileId == null || c.BusinessProfileId == businessProfileId) || isDefaultBusinessProfileUser))
        //                .OrderBy(c => c.DisplayOrder)
        //                .Select(c => new CustomCategoryModel
        //                {
        //                    Id = c.Id,
        //                    Name = c.Name,
        //                    BusinessProfileId = c.BusinessProfileId,
        //                    IsDefault = c.IsDefault
        //                }).ToList()
        //        }).ToList());
        //}

        public async Task<List<SelectModel>> GetEntityTypeListAsync()
        {
            return await Task.Run(() => this.entityTypeRepository.Where(t => !t.IsInternal).OrderBy(t => t.Name)
              .Select(t => new SelectModel
              {
                  Id = t.Id,
                  Name = t.Name
              }).ToList());
        }
        public async Task<CustomCategoryModel> GetCustomCategoryByIdAsync(int id)
        {
            if (id == 0)
            {
                return new CustomCategoryModel();
            }



            CustomCategory customCategory = await Task.Run(() => customCategoryRepository.Where(x => x.Id == id).Include(x => x.CustomCategoryType).FirstOrDefault());

            if (customCategory == null)
            {
                throw new SoftcodeNotFoundException("Custom Category not found");
            }
            else if (!this.Permission.IsValid(customCategory.CustomCategoryType.RightId))
            {
                throw new SoftcodeUnauthorizedException("You are not authorized to perform this action.");
            }



            return Mapper.Map<CustomCategoryModel>(customCategory);
        }
        public async Task<CustomCategoryTypeModel> GetCustomCategoryTypeByRoutingKeyAsync(string routingKey)
        {
            var type = await customCategoryTypeRepository.Where(t => t.RoutingKey == routingKey).FirstOrDefaultAsync();

            if (type == null)
            {
                throw new SoftcodeNotFoundException($"{routingKey} Category not found.");
            }
            else if (!this.Permission.IsValid( type.RightId))
            {
                throw new SoftcodeUnauthorizedException("You are not authorized to perform this action.");
            }

            return Mapper.Map<CustomCategoryTypeModel>(type);
        }


        public async Task<int> SaveCustomCategoryAsync(int id, CustomCategoryModel model)
        {
            if (model == null)
            {
                throw new SoftcodeArgumentMissingException("Invalid category data");
            }

            CustomCategory customCategory = new CustomCategory();

            var customCategoryType = customCategoryTypeRepository.Where(x => x.Id == model.CustomCategoryTypeId).Include(x => x.CustomCategories).FirstOrDefault();

            if (!this.Permission.IsValid( customCategoryType.RightId))
            {
                throw new SoftcodeUnauthorizedException("You are not authorized to perform this action.");
            }

            if (customCategoryType.CustomCategoryMapTypeId > 0 && customCategoryType.IsMapTypeMappingUnique)
            {
                if (customCategoryRepository.Where(x => x.Name == model.Name && x.CustomCategoryMapTypeOptionId == model.CustomCategoryMapTypeOptionId).Any())
                {
                    throw new SoftcodeInvalidDataException("Mapping multiple types and categories with same type is not allowed");
                }
            }

            if (id > 0)
            {
                customCategory = await customCategoryRepository.FindOneAsync(x => x.Id == id);

                if (customCategory == null)
                {
                    throw new SoftcodeNotFoundException("Category not found for edit");
                }
            }
            else
            {

                if (customCategoryType.CustomCategories.Any())
                {
                    model.DisplayOrder = customCategoryType.CustomCategories.Max(x => x.DisplayOrder) + 1;
                }
                else
                {
                    model.DisplayOrder = 1;
                }
            }

            Mapper.Map(model, customCategory);

            if (model.IsDefault)
            {
                customCategoryRepository.Attach(customCategoryRepository
                                                .Where(x => x.Id != id && x.CustomCategoryTypeId == model.CustomCategoryTypeId && x.IsDefault).ToList()
                                                .Select(x => { x.IsDefault = false; return x; }).FirstOrDefault());
            }


            if (model.Id == 0)
            {
                await customCategoryRepository.CreateAsync(customCategory);
            }
            else
            {
                await customCategoryRepository.UpdateAsync(customCategory);
            }
            this.ApplicationCacheService.ClearCustomCategoryType();
            return customCategory.Id;
        }
        public async Task<int> DeleteEntityAsync(int id)
        {
            CustomCategory category = customCategoryRepository.Where(x => x.Id == id).Include(x => x.CustomCategoryType).FirstOrDefault();

            if (category == null)
            {
                throw new SoftcodeNotFoundException("Category not found");
            }
            else if (!this.Permission.IsValid( category.CustomCategoryType.RightId))
            {
                throw new SoftcodeUnauthorizedException("You are not authorized to perform this action.");
            }

            if (category.IsDefault)
            {
                throw new SoftcodeInvalidDataException("Default category cannot be deleted.");
            }

            customCategoryRepository.Attach(customCategoryRepository
                .Where(c => c.CustomCategoryTypeId == category.CustomCategoryTypeId && c.DisplayOrder > category.DisplayOrder).OrderBy(x => x.DisplayOrder).ToList()
              .Select(x => { x.DisplayOrder -= 1; return x; }).ToList());

            this.ApplicationCacheService.ClearCustomCategoryType();
            int result = await customCategoryRepository.DeleteAsync(category);

            this.ApplicationCacheService.ClearCustomCategoryType();

            return result;
        }
        public async Task<bool> DeleteEntitiesAsync(List<int> ids)
        {
            if (ids == null)
            {
                throw new SoftcodeArgumentMissingException("Category not found");
            }

            if (!await customCategoryRepository.ExistsAsync(x => ids.Contains(x.Id)))
            {
                throw new SoftcodeArgumentMissingException("Category not found");
            }
            //TODO: need rearrange display order
            bool result = await customCategoryRepository.DeleteAsync(t => ids.Contains(t.Id))>0;
            this.ApplicationCacheService.ClearCustomCategoryType();

            return result;

        }
        public async Task<bool> MoveUpCategoriesAsync(int id)
        {
            if (id > 0)
            {
                var customCategory = await Task.Run(() => customCategoryRepository.Where(x => x.Id == id).Include(x => x.CustomCategoryType).FirstOrDefault());

                if (customCategory == null)
                {
                    throw new SoftcodeNotFoundException("Category not found for move up");
                }
                else if (!this.Permission.IsValid( customCategory.CustomCategoryType.RightId))
                {
                    throw new SoftcodeUnauthorizedException("You are not authorized to perform this action.");
                }

                var customCategoryUp = customCategoryRepository.Where(x => x.DisplayOrder < customCategory.DisplayOrder && x.CustomCategoryTypeId == customCategory.CustomCategoryTypeId)
                                    .OrderByDescending(x => x.DisplayOrder).FirstOrDefault();

                if (customCategoryUp != null)
                {


                    int currentDO = customCategory.DisplayOrder;

                    customCategory.DisplayOrder = customCategoryUp.DisplayOrder;
                    customCategoryUp.DisplayOrder = currentDO;

                    bool result = customCategoryRepository.SaveChanges() > 0;
                    this.ApplicationCacheService.ClearCustomCategoryType();
                    return result;
                }
            }


            return false;
        }
        public async Task<bool> MoveDownCategoriesAsync(int id)
        {
            if (id > 0)
            {
                CustomCategory customCategory = await Task.Run(() => customCategoryRepository.Where(x => x.Id == id).Include(x => x.CustomCategoryType).FirstOrDefault());

                if (customCategory == null)
                {
                    throw new SoftcodeNotFoundException("Category not found for move down");
                }
                else if (!this.Permission.IsValid( customCategory.CustomCategoryType.RightId))
                {
                    throw new SoftcodeUnauthorizedException("You are not authorized to perform this action.");
                }

                CustomCategory customCategoryDown = customCategoryRepository.Where(x => x.DisplayOrder > customCategory.DisplayOrder && x.CustomCategoryTypeId == customCategory.CustomCategoryTypeId)
                                    .OrderBy(x => x.DisplayOrder).FirstOrDefault();

                if (customCategoryDown != null)
                {
                    int currentDo = customCategory.DisplayOrder;

                    customCategory.DisplayOrder = customCategoryDown.DisplayOrder;
                    customCategoryDown.DisplayOrder = currentDo;

                    bool result = customCategoryRepository.SaveChanges() > 0;

                    this.ApplicationCacheService.ClearCustomCategoryType();
                    return result;
                }
            }


            return false;
        }

    }
}
