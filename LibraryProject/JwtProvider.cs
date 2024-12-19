using LibraryProject.Entities.UserApi;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryProject
{
    public class JwtProvider
    {
        public string GenerateToken(UserApi user)
        {
            //Полезная нагрузка токена
            Claim[] claims = [new("userId", user.Id.ToString())];

            //Алгоритм генерации секретного ключа
            var signCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthOptions.KEY)),
                SecurityAlgorithms.HmacSha256);

            //Создание токена
            var token = new JwtSecurityToken(
                claims : claims,
                signingCredentials : signCredentials,
                expires : DateTime.UtcNow.AddHours(AuthOptions.Hours)
                );

            //Перевод токена в строку
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
