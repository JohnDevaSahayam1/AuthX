using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Application.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJWtToken(IdentityUser user, IList<string> roles);
    }
}
