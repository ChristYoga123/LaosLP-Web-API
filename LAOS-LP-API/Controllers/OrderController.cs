using LAOS_LP_API.Data;
using LAOS_LP_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAOS_LP_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _context.orders.Include(u => u.User).Include(c => c.Course).Include("Course.Category").ToList();
        }
        [HttpGet("id")]
        public IActionResult Show(int id)
        {
            var order = _context.orders.Include(u => u.User).Include(c => c.Course).Include("Course.Category").FirstOrDefault(o => o.id == id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public IActionResult Store(Order order)
        {
            _context.orders.Add(order);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Show), new { id = order.id }, order);
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var order = _context.orders.Find(id);
            if (order == null) return NotFound();
            _context.orders.Remove(order);
            _context.SaveChanges();
            return NoContent();
        }
        
    }
}
