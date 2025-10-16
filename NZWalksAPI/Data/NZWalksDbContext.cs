using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models;

namespace NZWalksAPI.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        
        public DbSet<Walk> Walks {get; set;}
        public DbSet<Region> Regions {get; set;}
        public DbSet<Difficulty> Difficulties {get; set;}
    }
}