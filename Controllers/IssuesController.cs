using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OilRigWebApi.Models;

namespace OilRigWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly PostgresContext _context;

        public IssuesController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/Issues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> GetIssues()
        {
          if (_context.Issues == null)
          {
              return NotFound();
          }
            return await _context.Issues.ToListAsync();
        }

        // GET: api/Issues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Issue>> GetIssue(string id)
        {
          if (_context.Issues == null)
          {
              return NotFound();
          }
            var issue = await _context.Issues.FindAsync(id);

            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }

        // PUT: api/Issues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssue(string id, Issue issue)
        {
            if (id != issue.IssueId)
            {
                return BadRequest();
            }

            _context.Entry(issue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IssueExists(id))
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

        // POST: api/Issues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Issue>> PostIssue(Issue issue)
        {
          if (_context.Issues == null)
          {
              return Problem("Entity set 'PostgresContext.Issues'  is null.");
          }
            _context.Issues.Add(issue);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IssueExists(issue.IssueId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIssue", new { id = issue.IssueId }, issue);
        }

        // DELETE: api/Issues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(string id)
        {
            if (_context.Issues == null)
            {
                return NotFound();
            }
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IssueExists(string id)
        {
            return (_context.Issues?.Any(e => e.IssueId == id)).GetValueOrDefault();
        }
    }
}
