using LAOS_LP_API.Data;
using LAOS_LP_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAOS_LP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LessonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Lesson> Get()
        {
            var lessons = _context.lessons.Include(c => c.Course).Include("Course.Category");
            return lessons.ToList();
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            var lesson = _context.lessons.Include(c => c.Course).Include("Course.Category").FirstOrDefault(l => l.id == id);
            return lesson == null ? NotFound() : Ok(lesson);
        }
    }
}
