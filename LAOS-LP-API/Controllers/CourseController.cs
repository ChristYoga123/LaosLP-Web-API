using LAOS_LP_API.Data;
using LAOS_LP_API.Models;
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
            var course = _context.courses.Include(ct => ct.Category);
            return course.ToList();
        }

    }
}
