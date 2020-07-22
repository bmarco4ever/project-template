using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PRJ.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PRJ.Domain.Interfaces
{
    public interface IDataContext
    {
        /* Repositories Definition */
        DbSet<UserEntity> Users { get; set; }
        /* Repositories Definition */

        DatabaseFacade Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
