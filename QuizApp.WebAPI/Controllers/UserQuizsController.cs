using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.WebAPI.Data;
using QuizApp.WebAPI.Models;

namespace QuizApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserQuizsController : ControllerBase
    {
        private readonly QuizAppDbContext _context;

        public UserQuizsController(QuizAppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserQuizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserQuiz>>> GetUserQuizzes()
        {
            return await _context.UserQuizzes.ToListAsync();
        }

        // GET: api/UserQuizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserQuiz>> GetUserQuiz(Guid id)
        {
            var userQuiz = await _context.UserQuizzes.FindAsync(id);

            if (userQuiz == null)
            {
                return NotFound();
            }

            return userQuiz;
        }

        // PUT: api/UserQuizs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserQuiz(Guid id, UserQuiz userQuiz)
        {
            if (id != userQuiz.Id)
            {
                return BadRequest();
            }

            _context.Entry(userQuiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserQuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserQuizs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserQuiz>> PostUserQuiz(UserQuiz userQuiz)
        {
            _context.UserQuizzes.Add(userQuiz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserQuiz", new { id = userQuiz.Id }, userQuiz);
        }

        // DELETE: api/UserQuizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserQuiz(Guid id)
        {
            var userQuiz = await _context.UserQuizzes.FindAsync(id);
            if (userQuiz == null)
            {
                return NotFound();
            }

            _context.UserQuizzes.Remove(userQuiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserQuizExists(Guid id)
        {
            return _context.UserQuizzes.Any(e => e.Id == id);
        }
    }
}
