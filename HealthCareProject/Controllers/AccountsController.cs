using HealthCareProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HealthCareProject.Repositories;

namespace HealthCareProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbContext;
        IGetUserDEtailsUsingEmail<UserModelClass> _getUserDetailsUsingEmail;

        public AccountsController(IConfiguration configuration, ApplicationDbContext dbContext,IGetUserDEtailsUsingEmail<UserModelClass> getUserDEtailsUsingEmail)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _getUserDetailsUsingEmail = getUserDEtailsUsingEmail;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserModelClass user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            else if(_dbContext.Users.FirstOrDefault(x => x.Email== user.Email) != null)
            {
                return BadRequest();
            }

            this.CreatePasswordHash(user.Password, out byte[] passwordSalt, out byte[] passwordHash);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            var currentUser = _dbContext.Users.FirstOrDefault(x => x.Email == login.Email);

            if (currentUser == null)
            {
                return BadRequest("Invalid Username");
            }

            var isValidPassword = VerifyPassword(login.Password, currentUser.PasswordSalt, currentUser.PasswordHash);

            if (!isValidPassword)
            {
                return BadRequest("Invalid Password");
            }

            var token = GenerateToken(currentUser);

            if (token == null)
            {
                return NotFound("Invalid credentials");
            }

            return Ok(token);
        }
        [HttpGet("GetUserDetailsUsingEmail/{email}")]
        public async Task<IActionResult> GetUserDetailsUsingEmail(string email)
        {
            if(email != null)
            {
                var res = await _getUserDetailsUsingEmail.GetUserDetailsUsingEmail(email);
                if(res != null)
                { return Ok(res); }
            }
            return BadRequest();
        }

        [NonAction]
        public void CreatePasswordHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        [NonAction]
        public bool VerifyPassword(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return hash.SequenceEqual(passwordHash);
            }
        }

        [NonAction]
        public string GenerateToken(UserModelClass user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var myClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],
                                             claims: myClaims,
                                             expires: DateTime.Now.AddDays(1),
                                             signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("GetRole")]
        public IActionResult GetRole()
        {
            var Role = User.FindFirstValue(ClaimTypes.Role);
            return Ok(Role);
        }

        [HttpGet("GetName")]
        public IActionResult GetName()
        {
            var UserName = User.FindFirstValue(ClaimTypes.Name);
            return Ok(UserName);
        }
    }
}
