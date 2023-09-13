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
    public class OilRigsController : ControllerBase
    {
        private readonly PostgresContext _context;

        public OilRigsController(PostgresContext context)
        {
            _context = context;
        }

        // GET: api/OilRigs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OilRig>>> GetOilRigs()
        {
          if (_context.OilRigs == null)
          {
              return NotFound();
          }
            return await _context.OilRigs.ToListAsync();
        }

        // GET: api/OilRigs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OilRig>> GetOilRig(string id)
        {
          if (_context.OilRigs == null)
          {
              return NotFound();
          }
            var oilRig = await _context.OilRigs.FindAsync(id);

            if (oilRig == null)
            {
                return NotFound();
            }

            return oilRig;
        }

        // PUT: api/OilRigs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOilRig(string id, OilRig oilRig)
        {
            if (id != oilRig.RigId)
            {
                return BadRequest();
            }

            _context.Entry(oilRig).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OilRigExists(id))
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

        // POST: api/OilRigs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OilRig>> PostOilRig(OilRig oilRig)
        {
          if (_context.OilRigs == null)
          {
              return Problem("Entity set 'PostgresContext.OilRigs'  is null.");
          }
            _context.OilRigs.Add(oilRig);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OilRigExists(oilRig.RigId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOilRig", new { id = oilRig.RigId }, oilRig);
        }

        // DELETE: api/OilRigs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOilRig(string id)
        {
            if (_context.OilRigs == null)
            {
                return NotFound();
            }
            var oilRig = await _context.OilRigs.FindAsync(id);
            if (oilRig == null)
            {
                return NotFound();
            }

            _context.OilRigs.Remove(oilRig);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OilRigExists(string id)
        {
            return (_context.OilRigs?.Any(e => e.RigId == id)).GetValueOrDefault();
        }
    }
}
