using LibraryProject.Applications;
using LibraryProject.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryProject.Interfaces;
using AutoMapper;
using LibraryProject.ControllerModels;

namespace LibraryProject.Services
{
    public class UserDatabaseSevice : IUserService
    {
        private readonly DatabaseContext databaseContext;
        private readonly IMapper _mapper;
        public UserDatabaseSevice(DatabaseContext databaseContext, IMapper mapper)
        {
            _mapper = mapper;
            this.databaseContext = databaseContext;
        }
        public async Task<UserDTOChild> Get(int? id, CancellationToken cancellationToken)
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
            return _mapper.Map<UserDTOChild>(user);
        }
        public async Task<List<UserDTOChild>> GetAll(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            var users = await databaseContext.Users.Select(p => _mapper.Map<UserDTOChild>(p)).ToListAsync();
            return users;
        }
        public async Task Add(UserDTOParent user, CancellationToken cancellationToken)
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
        public async Task Update(int? id, UserDTOChild user)
        {
            if (user == null || id == null) throw new ArgumentNullException();
            var u = await databaseContext.Users.FindAsync(id);
            if (u == null)
            {
                throw new Exception("Пользователь не найден");
            }
            var us = _mapper.Map<User>(user);
            u.Surname = us.Surname;
            u.Adress = us.Adress;
            u.PhoneNumber = us.PhoneNumber;
            databaseContext.Users.Update(u);
            await databaseContext.SaveChangesAsync();
        }
    }
}
