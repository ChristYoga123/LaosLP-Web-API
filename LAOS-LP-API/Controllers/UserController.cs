using LAOS_LP_API.Data;
using LAOS_LP_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace LAOS_LP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public UserController(IConfiguration config, ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpGet("id")]
        public IActionResult Show(int id)
        {
            var user = _context.users.Find(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] Login login)
        {
            var usr = _context.users.Where(u => u.email.Equals(login.email) && u.password.Equals(login.password)).FirstOrDefault();
            if (usr != null)
            {
                var claims = new[]
                {
                   new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                   new Claim("id", usr.id.ToString()),
                   new Claim("name", usr.name.ToString()),
                   new Claim("email", usr.email.ToString()),
                   new Claim("password", usr.password.ToString()),
                   new Claim("is_admin", usr.is_admin.ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddHours(10),
                        signingCredentials: signIn);
                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return BadRequest("Username atau Password salah");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            var usr = _context.users.Where(u => u.name == user.name || u.email == user.email).FirstOrDefault();
            if(usr != null)
            {
                return BadRequest("Data pengguna sudah terdaftar");
            }
            _context.users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Show), new { id = user.id }, user);
        }
    }

}
