using Core.DTOs;
using Core.Service;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service_Interface
{
    public interface IAuthService 
    {
        Task<AuthModel> RegisterAsync(PatientDetailsDto dto);
 
        Task<AuthModel> GetTokenAsync(TokenRequestDto dto);
        Task<string> AddRoleAsync(AddRoleDto dto);
        Task<int> CountUsersAsync();
        Task<AuthModel> LogoutAsync(string userId);
    }
}
