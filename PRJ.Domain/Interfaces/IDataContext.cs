using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PRJ.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PRJ.Domain.Interfaces
{
    public interface IDataContext
    {
        DbSet<UserEntity> Users { get; set; }

        DatabaseFacade Database { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
