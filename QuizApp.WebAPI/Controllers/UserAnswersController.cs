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
    public class UserAnswersController : ControllerBase
    {
        private readonly QuizAppDbContext _context;

        public UserAnswersController(QuizAppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAnswer>>> GetUserAnswers()
        {
            return await _context.UserAnswers.ToListAsync();
        }

        // GET: api/UserAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAnswer>> GetUserAnswer(Guid id)
        {
            var userAnswer = await _context.UserAnswers.FindAsync(id);

            if (userAnswer == null)
            {
                return NotFound();
            }

            return userAnswer;
        }

        // PUT: api/UserAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAnswer(Guid id, UserAnswer userAnswer)
        {
            if (id != userAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAnswerExists(id))
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

        // POST: api/UserAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAnswer>> PostUserAnswer(UserAnswer userAnswer)
        {
            _context.UserAnswers.Add(userAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAnswer", new { id = userAnswer.Id }, userAnswer);
        }

        // DELETE: api/UserAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAnswer(Guid id)
        {
            var userAnswer = await _context.UserAnswers.FindAsync(id);
            if (userAnswer == null)
            {
                return NotFound();
            }

            _context.UserAnswers.Remove(userAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAnswerExists(Guid id)
        {
            return _context.UserAnswers.Any(e => e.Id == id);
        }
    }
}
