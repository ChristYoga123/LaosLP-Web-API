using LAOS_LP_API.Data;
using LAOS_LP_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAOS_LP_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public IEnumerable<Category> Get()
        {
            return _context.categories.ToList();
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var category = _context.categories.Find(id);
            return category == null ? NotFound() : Ok(category);
        }

    }
}
