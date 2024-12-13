using LibraryProject.Applications;
using LibraryProject.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;

namespace LibraryProject.Services
{
    public class UserDatabaseSevice : IUserService
    {
        private readonly DatabaseContext databaseContext;
        public UserDatabaseSevice(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<(int, User?)> Get(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var user = await databaseContext.Users.FindAsync(id);
            if (user == null)
            {
                return (1, new());
            }
            return (0, user);
        }
        public async Task<List<User>> GetAll()
        {
            var users = await databaseContext.Users.ToListAsync();
            return users;
        }
        public async Task<int> Add(UserWithoutExternal user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            var us = await databaseContext.Users.Where(u => u.PhoneNumber == user.PhoneNumber).FirstOrDefaultAsync();
            if (us != null)
            {
                return 1;
            }
            User u = new()
            {
                Adress = user.Adress,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Surname = user.Surname
            };
            await databaseContext.Users.AddAsync(u);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public async Task<int> DeleteById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var user = await databaseContext.Users.FindAsync(id);
            if (user == null)
            {
                return 1;
            }
            databaseContext.Users.Remove(user);
            await databaseContext.SaveChangesAsync();
            return 0;
        } 
        public async Task<int> Update(int? id, User user)
        {
            if (user == null || id == null) throw new ArgumentNullException();
            var u = await databaseContext.Users.FindAsync(id);
            if (user == null || u == null)
            {
                return 1;
            }
            u.Surname = user.Surname;
            u.Adress = user.Adress;
            u.PhoneNumber = user.PhoneNumber;
            databaseContext.Users.Update(u);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
    }
}
