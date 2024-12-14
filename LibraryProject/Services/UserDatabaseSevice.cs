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
        public async Task<User> Get(int? id, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var user = await databaseContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            return user;
        }
        public async Task<List<User>> GetAll(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            var users = await databaseContext.Users.ToListAsync();
            return users;
        }
        public async Task Add(UserWithoutExternal user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            if (user == null)
            {
                throw new ArgumentNullException();
            }
            var us = await databaseContext.Users.Where(u => u.PhoneNumber == user.PhoneNumber).FirstOrDefaultAsync();
            if (us != null)
            {
                throw new Exception("Пользователь с таким номером телефона уже существует");
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
        }
        public async Task DeleteById(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }
            var user = await databaseContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("Пользователь не найден");
            }
            databaseContext.Users.Remove(user);
            await databaseContext.SaveChangesAsync();
        } 
        public async Task Update(int? id, User user)
        {
            if (user == null || id == null) throw new ArgumentNullException();
            var u = await databaseContext.Users.FindAsync(id);
            if (u == null)
            {
                throw new Exception("Пользователь не найден");
            }
            u.Surname = user.Surname;
            u.Adress = user.Adress;
            u.PhoneNumber = user.PhoneNumber;
            databaseContext.Users.Update(u);
            await databaseContext.SaveChangesAsync();
        }
    }
}
