using AccesoDatos.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Modelo.Dtos;
using Modelo.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class UserServices : IUserServices, IAuthServices
    {
        private readonly EjemploContext _context;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public UserServices(EjemploContext context, IConfiguration config, IMapper mapper)
        {
            _context = context;
            _config = config;
            _mapper = mapper;
        }

        public async Task<User> AddUser(UserCreatedDto user)
        {
            /*
            var u = new User();

            u.Name = user.Name;
            u.LastName = user.LastName;
            u.UserName = user.UserName;
            */

            var u = _mapper.Map<User>(user);


            u.UserId = Guid.NewGuid();
            u.MustChangePassword = true;

            byte[] hash, salt;
            CreatePasswordHash(user.Password, out hash, out salt);

            u.PasswordHash = hash;
            u.PasswordSalt = salt;

            await _context.Users.AddAsync(u);
            await _context.SaveChangesAsync();
            return u;
        }

        public async Task<bool> DeleteUser(Guid UserId)
        {
            var user = await GetUserById(UserId);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUserById(Guid UserId)
        {
            return await _context.Users.Where(x => x.UserId == UserId).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto login)
        {
            LoginResponseDto result = null;
            var user = await _context.Users.Where(x => x.UserName == login.UserName).FirstOrDefaultAsync();

            if (user != null)
            {
                if (!await VerifyPassword(login.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }

                //result = new LoginResponseDto();
                //result.UserId = user.UserId;
                //result.Name = user.Name;
                //result.LastName = user.LastName;
                //result.UserName = user.UserName;
                //result.MustChangePassword = user.MustChangePassword;
                //result.LastAuthenticateDateTime = user.LastAuthenticateDateTime;

                result = _mapper.Map<LoginResponseDto>(user);
                result.Token = await GetToken(user);
            }

            return result;
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private async Task<bool> VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            //if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            //if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return await Task.FromResult<bool>(false);
                }
            }

            return await Task.FromResult<bool>(true);
        }

        private async Task<string> GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings").GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //foreach (var rol in user.UserRoles)
            //    tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, rol.Rol.Name));

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return await Task.FromResult<string>(tokenString);
        }


    }
}

