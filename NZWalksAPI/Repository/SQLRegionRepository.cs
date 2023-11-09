using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext context;
        public SQLRegionRepository(NZWalksDbContext context)
        {
            this.context = context;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await context.Regions.AddAsync(region);
            await context.SaveChangesAsync();

            return region;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var regionDelete = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(regionDelete == null)
            {
                return null;
            }

            context.Regions.Remove(regionDelete);
            await context.SaveChangesAsync();

            return regionDelete;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await context.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> UpdateAsync(Guid id, Region Region)
        {
            var existRegion = await context.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(existRegion == null) 
            {
                return null;
            }

            existRegion.Code = Region.Code;
            existRegion.Name = Region.Name;
            existRegion.RegionImageUrl = Region.RegionImageUrl;

            await context.SaveChangesAsync();

            return existRegion;
        }
    }
}
