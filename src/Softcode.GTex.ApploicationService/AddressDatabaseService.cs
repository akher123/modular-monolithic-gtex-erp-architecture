using Softcode.GTex.Data;
using Softcode.GTex.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softcode.GTex.ApploicationService.Models;

namespace Softcode.GTex.ApploicationService
{
    public class AddressDatabaseService : BaseService<AddressDatabaseService>, IAddressDatabaseService
    {
        private readonly IRepository<TimeZone> timeZoneRepository;
        private readonly IRepository<Country> countryRepository;
        private readonly IRepository<State> stateRepository;
        private readonly IRepository<PostalCode> postcodeRepository;
        private readonly IRepository<Region> regionRepository;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="timeZone"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        public AddressDatabaseService(IApplicationServiceBuilder applicationServiceBuilder
            , IRepository<TimeZone> timeZoneRepository
            , IRepository<Country> countryRepository
            , IRepository<State> stateRepository
            , IRepository<PostalCode> postcodeRepository
            , IRepository<Region> regionRepository) : base(applicationServiceBuilder)
        {
            this.timeZoneRepository = timeZoneRepository;
            this.countryRepository = countryRepository;
            this.stateRepository = stateRepository;
            this.postcodeRepository = postcodeRepository;
            this.regionRepository = regionRepository;
        }

        public async Task<List<SelectModel>> GetCountrySelectItemsAsync()
        {
            return await Task.Run(() => countryRepository.GetAll()
               .OrderBy(t => t.CountryName)
               .Select(x => new SelectModel
               {
                   Id = x.Id,
                   Name = x.CountryName,
                   IsDefault = x.IsDefault
               }).ToList());
        }

        public async Task<List<SelectModel>> GetStateSelectItemsAsync(int countryId)
        {
            return await Task.Run(() => stateRepository.Where(x => x.CountryId == countryId)
               .OrderBy(t => t.StateName)
               .Select(x => new SelectModel
               {
                   Id = x.Id,
                   Name = x.StateName,
                   //Tag =x.CountryId
               }).ToList());
        }

        public async Task<List<SelectModel>> GetPostcodeSelectItemsAsync(int stateId)
        {
            return await Task.Run(() => postcodeRepository.Where(x => x.StateId == stateId)
               .OrderBy(t => t.PostCode)
               .Select(x => new SelectModel
               {
                   Id = x.Id,
                   Name = x.PostCode,
                   //Tag = x.StateId
               }).ToList());
        }

        public async Task<List<SuburbModel>> GetSuburbAsync()
        {
            return await Task.Run(() => postcodeRepository.GetAll().Include(x => x.State).Include(x => x.Country)
               .OrderBy(t => t.City)
               .Select(x => new SuburbModel
               {
                   Id=x.Id,
                   CountryId = x.CountryId,
                   Country = x.Country.CountryName,
                   StateId = x.StateId,
                   State = x.State.StateName,
                   PostCode = x.PostCode,
                   Suburb = x.City
               }).Distinct().ToList());
        }

        public async Task<List<SelectModel>> GetTimeZoneSelectItemsAsync()
        {
            return await Task.Run(() => timeZoneRepository.GetAll()
                .OrderBy(t => t.OffsetValueInMinutes)
                .ThenBy(t => t.DisplayName)
                .Select(x => new SelectModel
                {
                    Id = x.Id.ToString(),
                    Name = x.DisplayName,
                    Tag = x.OffsetValueInMinutes
                }).ToList());
        }

        public async Task<List<SelectModel>> GetRegionAsync(int businessProfileId)
        {            
            bool isDefaultBusinessProfileUser = this.LoggedInUser.IsDefaultBusinessProfile;
            return await Task.Run(() => regionRepository.Where(x => x.IsActive && (x.BusinessProfileId == null || x.BusinessProfileId == businessProfileId))
              .OrderBy(t => t.SortOrder)
              .Select(x => new SelectModel
              {
                  Id = x.Id,
                  Name = x.RegionName,
                  Tag = x.RegionCode,
                  IsDefault = x.IsDefault
              }).ToList());
        }
    }
}

