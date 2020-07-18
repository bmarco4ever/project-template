using PRJ.Domain.Entities;
using System.Threading.Tasks;

namespace PRJ.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserEntity> FindByEmail(string email);
    }
}
