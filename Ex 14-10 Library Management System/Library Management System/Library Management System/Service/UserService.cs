using Library_Management_System.Entities;
using Library_Management_System.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library_Management_System.Service
{
    public interface IUserService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<object?> GetUserDetailsAsync(int id);
        Task<object?> UpdateUserAsync(int id, UpdateUserDto dto); // FIXED: Added to interface
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _repository.GetByUsernameAsync(dto.Username);
            if (existingUser != null) return null;

            var user = new User
            {
                Username = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            var savedUser = await _repository.AddAsync(user);
            var token = GenerateJwtToken(savedUser);

            return new AuthResponseDto { Id = savedUser.Id, Username = savedUser.Username, Role = savedUser.Role, Token = token };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return null;
            }

            var token = GenerateJwtToken(user);
            return new AuthResponseDto { Id = user.Id, Username = user.Username, Role = user.Role, Token = token };
        }

        public async Task<object?> GetUserDetailsAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            return new
            {
                user.Id,
                user.Username,
                user.Role,
                Status = user.IsActive ? "Active" : "Inactive"
            };
        }

        // FIXED: Added the missing update logic!
        public async Task<object?> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return null;

            // Only update the fields that are provided
            if (!string.IsNullOrWhiteSpace(dto.Username))
                user.Username = dto.Username;

            if (!string.IsNullOrWhiteSpace(dto.Role))
                user.Role = dto.Role;

            if (dto.IsActive.HasValue)
                user.IsActive = dto.IsActive.Value;

            // If a new password is provided, generate a new hash
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _repository.UpdateAsync(user);

            return new
            {
                user.Id,
                user.UserCode,
                user.Username,
                user.Role,
                Status = user.IsActive ? "Active" : "Inactive"
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}