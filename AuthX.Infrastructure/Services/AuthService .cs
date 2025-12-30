using AuthX.Application.DTOs;
using AuthX.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;

        public AuthService( IUserRepository userRepository,ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<string> RegisterAsync(RegisterRequestDTO dto)
        {
            var existingUser = await _userRepository.FindByUserNameAsync(dto.UserName);
            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new IdentityUser
            {
                UserName = dto.UserName,
                Email = dto.UserName
            };

            var result = await _userRepository.CreateUserAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception("User creation failed");

            if (dto.Roles != null && dto.Roles.Any())
                await _userRepository.AddRolesAsync(user, dto.Roles);

            return "User registered successfully";
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto)
        {
            var user = await _userRepository.FindByUserNameAsync(dto.UserName);
            if (user == null)
                throw new UnauthorizedAccessException();

            var isValid = await _userRepository.CheckPasswordAsync(user, dto.Password);
            if (!isValid)
                throw new UnauthorizedAccessException();

            var roles = await _userRepository.GetRolesAsync(user);
            var token = _tokenRepository.CreateJWtToken(user, roles);

            return new LoginResponseDTO
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Roles = roles,
                AccessToken = token,
                TokenType = "Bearer"
            };
        }
    }

}
