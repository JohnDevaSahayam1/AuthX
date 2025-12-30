using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser?> FindByUserNameAsync(string userName);
        Task<bool> CheckPasswordAsync(IdentityUser user, string password);
        Task<IdentityResult> CreateUserAsync(IdentityUser user, string password);
        Task<IList<string>> GetRolesAsync(IdentityUser user);
        Task AddRolesAsync(IdentityUser user, IList<string> roles);
    }
}
