using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Application.DTOs
{
    public class LoginResponseDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
    }
}
