using System;
using Application.Contracts;
using Application.DTOS;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repo
{
    internal class UserRepo : IUser
    {
        private readonly StudiaDBContext studiaDBContext;

        private readonly IConfiguration configuration;
        public UserRepo(StudiaDBContext studiaDBContext, IConfiguration configuration)
        {
            this.studiaDBContext = studiaDBContext;
            this.configuration = configuration;
        }
        public async Task<LoginResponse> LoginUserAsync(LoginUserDTO loginDTO)
        {
            var getUser = await FindUserByEmail(loginDTO.email!);

            if (getUser == null)
            {
                return new LoginResponse(false, "User not found");
            }

            bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.password, getUser.password);
            
            if (checkPassword) {
                string token = GenerateJWTToken(getUser);
                return new LoginResponse(true, "Login Succesfully", token );
            }
            else
                return new LoginResponse(false, "Invalid Credentials");
        }

        private string GenerateJWTToken(UserEntity user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credencials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var usersClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id_user.ToString()),
                new Claim(ClaimTypes.Name, user.name),
                new Claim(ClaimTypes.Email, user.email),
            };
            var token = new JwtSecurityToken
            (
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: usersClaims,
            expires: DateTime.Now .AddDays(5),
            signingCredentials: credencials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserEntity> FindUserByEmail(string email) =>
             await studiaDBContext.Users.FirstOrDefaultAsync(u => u.email == email);
        public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var getUser = await FindUserByEmail(registerUserDTO.email!);
            
            if (getUser != null) {
                return new RegistrationResponse(false, "User already exists");
            }
            else
            {
                studiaDBContext.Users.Add(new UserEntity()
                {
                    name = registerUserDTO.name,
                    email = registerUserDTO.email,
                    password = BCrypt.Net.BCrypt.HashPassword(registerUserDTO.password)
                });
            }

            await studiaDBContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Registation Completed");
        }

        }
    }
