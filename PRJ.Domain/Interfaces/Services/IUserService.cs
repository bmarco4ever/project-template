using PRJ.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRJ.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserEntity> AuthenticateAsync(string email, string password);
        Task<UserEntity> GetAsync(int id);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<UserEntity> CreateAsync(UserEntity user);
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task<UserEntity> RemoveAsync(int id);
    }
}
