using Microsoft.AspNetCore.Mvc;
using MedFutureAPI.Entities;
using MedFutureAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedFutureAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;
        public UserController(UserDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll(string? query)
        {
            IQueryable<User> users = _context.User;

            if (!string.IsNullOrWhiteSpace(query))
            {
                users = users
                    .AsEnumerable()
                    .Where(u => u.Name.Contains(query) || u.Stack.Any(s => s.Contains(query) || u.NickName.Contains(query)))
                    .AsQueryable(); 
            }

            var result = users.Select(u => new
            {
                u.Id,
                u.Name,
                u.NickName,
                Birth = u.Birth.ToString("yyyy-MM-dd"),
                u.Stack
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user = _context.User.SingleOrDefault(u => u.Id == id);


            if (user == null)
            {
                return NotFound();
            }

            var result = new
            {
                user.Id,
                user.Name,
                user.NickName,
                Birth = user.Birth.ToString("yyyy-MM-dd"),
                user.Stack
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, User input)
        {
            var user = _context.User.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Update(input.Name, input.NickName, input.Birth, input.Stack);

            _context.User.Update(user);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _context.User.SingleOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
