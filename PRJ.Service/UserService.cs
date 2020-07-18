using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Entities;
using PRJ.Domain.Interfaces;
using PRJ.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace PRJ.Services
{
    public class UserService : IUserService
    {
        private readonly IDataContext _context;

        public UserService(IDataContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> GetById(long id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(id)).ConfigureAwait(false);
        }

        public async Task<UserEntity> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email)).ConfigureAwait(false);
        }
    }
}
