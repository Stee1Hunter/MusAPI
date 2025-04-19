using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusAPI.Models;

namespace MusAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfflineTracksController : ControllerBase
    {
        private readonly MusicAppContext _context;

        public OfflineTracksController(MusicAppContext context)
        {
            _context = context;
        }

        // GET: api/OfflineTracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OfflineTrack>>> GetOfflineTracks()
        {
            return await _context.OfflineTracks.ToListAsync();
        }

        // GET: api/OfflineTracks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OfflineTrack>> GetOfflineTrack(int id)
        {
            var offlineTrack = await _context.OfflineTracks.FindAsync(id);

            if (offlineTrack == null)
            {
                return NotFound();
            }

            return offlineTrack;
        }

        // PUT: api/OfflineTracks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOfflineTrack(int id, OfflineTrack offlineTrack)
        {
            if (id != offlineTrack.OfflineId)
            {
                return BadRequest();
            }

            _context.Entry(offlineTrack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfflineTrackExists(id))
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

        // POST: api/OfflineTracks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OfflineTrack>> PostOfflineTrack(OfflineTrack offlineTrack)
        {
            _context.OfflineTracks.Add(offlineTrack);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOfflineTrack", new { id = offlineTrack.OfflineId }, offlineTrack);
        }

        // DELETE: api/OfflineTracks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOfflineTrack(int id)
        {
            var offlineTrack = await _context.OfflineTracks.FindAsync(id);
            if (offlineTrack == null)
            {
                return NotFound();
            }

            _context.OfflineTracks.Remove(offlineTrack);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfflineTrackExists(int id)
        {
            return _context.OfflineTracks.Any(e => e.OfflineId == id);
        }
    }
}
