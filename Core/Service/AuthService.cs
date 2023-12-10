using Core.Service_Interface;
using Domain.Models;
using Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Domain.Models;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Helpers;
using API.Models;
using System.Linq;
using Core.DTOs;

namespace Core.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _Context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public AuthService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _Context = context;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }
        public async Task<AuthModel> RegisterAsync(PatientDetailsDto dto)
        {
            //find another user with same email
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return new AuthModel
                {
                    Message = "Email is already registerd, try another one."
                };

            var User = new ApplicationUser
            {
                Email = dto.Email,
                PhoneNumber = dto.phone,
                UserName = dto.Email,
            };
            var result = await _userManager.CreateAsync(User, dto.Email);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(User, "User");
                var jwtSecurityToken = await CreateJwtToken(User);
                var PatientDetails = new PatientDetailsDto
                {
                    DateOfBirth = dto.DateOfBirth,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Gender = dto.Gender,
                     ImageUrl = dto.ImageUrl,
                     phone = dto.PhoneNumber,
                      

                
                };
            //    _Context.PatientDetails.Add(PatientDetails);
                await _Context.SaveChangesAsync();
                return new AuthModel
                {
                    Message = User.Id,
                    Email = User.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = new List<string> { "User" },
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    UserName = User.UserName
                };
            }
            else
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}, ";
                }
                return new AuthModel
                {
                    Message = errors
                };
            }
        }
        public async Task<AuthModel> LogoutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new AuthModel
                {
                    Message = "User not found."
                };
            }

            // Perform any additional logout operations if needed...

            return new AuthModel
            {
                Message = "Logout successful."
            };
        }
        public async Task<int> CountUsersAsync()
        {
            var role = "User"; // Specify the role you want to count the users for

            var usersInRole = await _userManager.GetUsersInRoleAsync(role);
            var count = usersInRole.Count;

            return count;
        }
        
        public async Task<AuthModel> GetTokenAsync(TokenRequestDto dto)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }


            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);

            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UserName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.Roles = rolesList.ToList();
            return authModel;
        }
        public async Task<string> AddRoleAsync(AddRoleDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(dto.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, dto.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, dto.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }
        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser applicationUser)
        {
            var userClaims = await _userManager.GetClaimsAsync(applicationUser);
            var roles = await _userManager.GetRolesAsync(applicationUser);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim("uid", applicationUser.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
             //   expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }


    }
}

