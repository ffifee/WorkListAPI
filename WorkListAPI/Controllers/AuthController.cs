using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkListAPI.Auth;
using WorkListAPI.Models;

namespace WorkListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly  UserManager<AppUser> userManager;
        private readonly  RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim("name", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var eachUser in userRole)
                {
                    authClaims.Add(new Claim("role", eachUser));
                }

                var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:SecretKey"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtAuth:ValidIssuer"],
                    audience: _configuration["JwtAuth:ValidAudience"],
                    expires: DateTime.Now.AddHours(4),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
                    
            }
            return Unauthorized();
        }
        [HttpPost]
        [Route("register")]
        
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var userExist = await userManager.FindByNameAsync(model.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { Status = "Error", Message = "Korisnik već postoji." });

            }
            AppUser user = new AppUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new StatusResponse { Status = "Success", Message = "Uspješno ste kreirali račun." });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { Status = "Error", Message = "Nije uspješno kreiran račun. Molimo pokušajte ponovno." });

            }
        }

        [HttpPost]
        [Route("admin-reg")]

        public async Task<IActionResult> AdminReg([FromBody] Register model)
        {
            var userExist = await userManager.FindByNameAsync(model.UserName);

            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { Status = "Error", Message = "Korisnik već postoji." });
            };

            AppUser user = new AppUser()
            {
                Email = model.Email,
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new StatusResponse { Status = "Error", Message = "Nije uspješno kreiran račun. Molimo pokušajte ponovno." });
            }
            if (!await roleManager.RoleExistsAsync(Roles.Administrator))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Administrator));
            }

            if (!await roleManager.RoleExistsAsync(Roles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Saler))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Saler));
            }

            if (!await roleManager.RoleExistsAsync(Roles.ITPersonel))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.ITPersonel));
            }
            if (await roleManager.RoleExistsAsync(Roles.Administrator))
            {
                await userManager.AddToRoleAsync(user, Roles.Administrator);
            }

            return Ok(new StatusResponse { Status = "Success", Message = "Uspješno ste kreirali račun." });
        }
    }
}