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
    public class AlertsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public AlertsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/Alerts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alert>>> GetAlerts()
        {
          if (_context.Alerts == null)
          {
              return NotFound();
          }
            return await _context.Alerts.ToListAsync();
        }

        // GET: api/Alerts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alert>> GetAlert(string id)
        {
          if (_context.Alerts == null)
          {
              return NotFound();
          }
            var alert = await _context.Alerts.FindAsync(id);

            if (alert == null)
            {
                return NotFound();
            }

            return alert;
        }

        // PUT: api/Alerts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlert(string id, Alert alert)
        {
            if (id != alert.AlertId)
            {
                return BadRequest();
            }

            _context.Entry(alert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertExists(id))
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

        // POST: api/Alerts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alert>> PostAlert(Alert alert)
        {
          if (_context.Alerts == null)
          {
              return Problem("Entity set 'PostgresContext.Alerts'  is null.");
          }
            _context.Alerts.Add(alert);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AlertExists(alert.AlertId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAlert", new { id = alert.AlertId }, alert);
        }

        // DELETE: api/Alerts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert(string id)
        {
            if (_context.Alerts == null)
            {
                return NotFound();
            }
            var alert = await _context.Alerts.FindAsync(id);
            if (alert == null)
            {
                return NotFound();
            }

            _context.Alerts.Remove(alert);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertExists(string id)
        {
            return (_context.Alerts?.Any(e => e.AlertId == id)).GetValueOrDefault();
        }
    }
}
