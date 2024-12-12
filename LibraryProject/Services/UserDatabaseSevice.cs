using LibraryProject.Applications;
using LibraryProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class UserDatabaseSevice
    {
        public static async Task<(int, User?)> Get(DatabaseContext databaseContext, int id)
        {
            var user = await databaseContext.Users.FindAsync(id);
            if (user == null)
            {
                return (1, new());
            }
            return (0, user);
        }
        public static async Task<List<User>> GetAll(DatabaseContext databaseContext)
        {
            var users = await databaseContext.Users.ToListAsync();
            return users;
        }
        public static async Task<int> Add(DatabaseContext databaseContext, UserWithoutExternal user)
        {
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
        public static async Task<int> DeleteById(DatabaseContext databaseContext, int id)
        {
            var user = await databaseContext.Users.FindAsync(id);
            if (user == null)
            {
                return 1;
            }
            databaseContext.Users.Remove(user);
            await databaseContext.SaveChangesAsync();
            return 0;
        }
        public static async Task<int> Update(DatabaseContext databaseContext, int id, User user)
        {
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
