using LAOS_LP_API.Data;
using LAOS_LP_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAOS_LP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context) => _context = context;

        [HttpGet("id")]
        public IActionResult Show(int id)
        {
            var user = _context.users.Find(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _context.users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Show), new { id = user.id }, user);
        }
    }
}
