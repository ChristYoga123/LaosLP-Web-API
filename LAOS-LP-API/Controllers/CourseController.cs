using LAOS_LP_API.Data;
using LAOS_LP_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAOS_LP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CourseController(ApplicationDbContext context) => _context = context;
        
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            var courses = _context.courses.Include(ct => ct.Category);
            return courses.ToList();
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var course = _context.courses.Include(ct => ct.Category).FirstOrDefault(c => c.id == id);
            return course == null ? NotFound() : Ok(course);
        }

    }
}
