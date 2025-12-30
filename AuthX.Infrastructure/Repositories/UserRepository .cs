using AuthX.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityUser?> FindByUserNameAsync(string userName)
            => await _userManager.FindByNameAsync(userName);

        public async Task<bool> CheckPasswordAsync(IdentityUser user, string password)
            => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, string password)
            => await _userManager.CreateAsync(user, password);

        public async Task<IList<string>> GetRolesAsync(IdentityUser user)
            => await _userManager.GetRolesAsync(user);

        public async Task AddRolesAsync(IdentityUser user, IList<string> roles)
        {
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }

            await _userManager.AddToRolesAsync(user, roles);
        }
    }

}
