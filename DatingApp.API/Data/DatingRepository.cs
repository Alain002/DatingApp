using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Modules;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        public readonly DataContext _context;
        public DatingRepository(DataContext context) {
            _context = context;
         }
            
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            // photos wont be automaticly included. because it's a navigation property
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            // if the value is means, it means that nothing has changed in the database
            return await _context.SaveChangesAsync() >0;
        }
    }
}