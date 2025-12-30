using AuthX.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthX.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequestDTO dto);
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO dto);
    }
}
