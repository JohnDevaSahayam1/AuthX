using AuthX.Application.DTOs;
using AuthX.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace AuthX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid request");

            var existingUser = await _userManager.FindByNameAsync(dto.UserName);
            if (existingUser != null)
                return BadRequest("User already exists");

            var user = new IdentityUser
            {
                UserName = dto.UserName,
                Email = dto.UserName,
            };

            var createResult = await _userManager.CreateAsync(user, dto.Password);

            if (!createResult.Succeeded)
                return BadRequest(createResult.Errors);

            if (dto.Roles != null && dto.Roles.Any())
            {
                foreach (var role in dto.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                var roleResult = await _userManager.AddToRolesAsync(user, dto.Roles);

                if (!roleResult.Succeeded)
                    return BadRequest(roleResult.Errors);
            }

            return Ok(new
            {
                message = "User registered successfully",
                user = user.UserName,
                roles = dto.Roles
            });
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.UserName);
            if (user == null)
                return Unauthorized("Invalid username or password.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return Unauthorized("Invalid username or password.");

            var roles = await _userManager.GetRolesAsync(user);

            var token = _tokenRepository.CreateJWtToken(user, roles.ToList());

            return Ok(new LoginResponseDTO
            {
                UserName = user.UserName!,
                Email = user.Email!,
                Roles = roles,
                AccessToken = token,
                TokenType = "Bearer"
            });
        }

    }
}
