using Microsoft.EntityFrameworkCore;
using PRJ.Domain.Entities;
using PRJ.Domain.Interfaces;
using PRJ.Domain.Interfaces.Services;
using System.Collections.Generic;
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

        public async Task<UserEntity> AuthenticateAsync(string email, string password)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email)).ConfigureAwait(false);

                if (user == null) return null;
                if (user.Password == password)
                {
                    return user;
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return null;
        }

        public async Task<UserEntity> GetAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<UserEntity> CreateAsync(UserEntity user)
        {

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity> RemoveAsync(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
