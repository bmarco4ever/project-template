
using Microsoft.EntityFrameworkCore;
using PRJ.Data.Configurations;
using PRJ.Domain.Entities;
using PRJ.Domain.Interfaces;

namespace PRJ.Data
{
    public class DataContext : DbContext, IDataContext
    {
        /* Repositories Definition */
        public DbSet<UserEntity> Users { get; set; }
        /* Repositories Definition */

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Database Configuration */
            modelBuilder.Entity<UserEntity>(new UserConfiguration().Configure);
            /* Database Configuration */
        }
    }
}
