using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repository
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext context;

        public SQLWalkRepository(NZWalksDbContext context)
        {
            this.context = context;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await context.Walks.AddAsync(walk);
            await context.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var deleteWalk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            //Check if it exists
            if(deleteWalk == null)
            {
                return null;
            }

            context.Walks.Remove(deleteWalk);

            await context.SaveChangesAsync();

            return deleteWalk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var walks = context.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //  Filter
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false) 
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // Sorting
            if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
            }
            else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
            {
                walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
            }

            //  Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await context.Walks.Include("Difficulty").Include("Region").ToListAsync(); //Include to integrate navigation properties.
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            //  Confirm if it exist
            var updateWalk = await context.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (updateWalk == null)
            {
                return null;
            }

            //  Update information from controller
            updateWalk.Name = walk.Name;
            updateWalk.Description = walk.Description;
            updateWalk.LengthInKm = walk.LengthInKm;
            updateWalk.WalkImageUrl = walk.WalkImageUrl;
            updateWalk.DifficultyId = walk.DifficultyId;
            updateWalk.RegionId = walk.RegionId;

            await context.SaveChangesAsync();

            return updateWalk;
        }

        
    }
}
