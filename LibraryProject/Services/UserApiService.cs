using AutoMapper;
using LibraryProject.Applications;
using LibraryProject.ControllerModels;
using LibraryProject.Entities.UserApi;
using LibraryProject.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.Services
{
    public class UserApiService : IUserApiService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IMapper _mapper;
        private readonly JwtProvider _jwtProvider;
        public UserApiService(DatabaseContext databaseContext, IMapper mapper, JwtProvider jwtProvider)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
            _jwtProvider = jwtProvider;
        }
        public async Task<string> Login(UserApiDTO user, CancellationToken token)
        {
            if (user == null)
            {
                throw new ArgumentNullException("В качестве аргумента пришел null");
            }
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            var userApi = await _databaseContext.ApiUsers.Where(p => p.Login == user.Login && p.Password == user.Password).FirstOrDefaultAsync()
                ??throw new Exception("Пользователь не найден в базе данных");

            var jwtToken = _jwtProvider.GenerateToken(userApi);
            return jwtToken;
        }
        public async Task Register(UserApiDTO user, CancellationToken token)
        {
            if (user == null)
            {
                throw new ArgumentNullException("В качестве аргумента пришел null");
            }
            if (token.IsCancellationRequested)
            {
                throw new OperationCanceledException("Операция отменена");
            }
            var temp = await _databaseContext.ApiUsers.Where(p => p.Login == user.Login).FirstOrDefaultAsync();
            if (temp != null)
            {
                throw new Exception("Такой пользователь уже существует");
            }
            UserApi u = new() { Login = user.Login, Password = user.Password };
            await _databaseContext.ApiUsers.AddAsync(u);
            await _databaseContext.SaveChangesAsync();
        }
    }
}
