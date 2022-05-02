using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XMEN.Entities;

namespace XMEN.DataAccess
{
    public class ApiDbContext : IdentityDbContext
    {
        public DbSet<ResultXmen> resultXmen { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Ignore<Entity>();
            base.OnModelCreating(builder);
        }
    }
}