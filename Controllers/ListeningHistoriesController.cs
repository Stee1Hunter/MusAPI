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
    public class ListeningHistoriesController : ControllerBase
    {
        private readonly MusicAppContext _context;

        public ListeningHistoriesController(MusicAppContext context)
        {
            _context = context;
        }

        // GET: api/ListeningHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListeningHistory>>> GetListeningHistories()
        {
            return await _context.ListeningHistories.ToListAsync();
        }

        // GET: api/ListeningHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ListeningHistory>> GetListeningHistory(int id)
        {
            var listeningHistory = await _context.ListeningHistories.FindAsync(id);

            if (listeningHistory == null)
            {
                return NotFound();
            }

            return listeningHistory;
        }

        // PUT: api/ListeningHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListeningHistory(int id, ListeningHistory listeningHistory)
        {
            if (id != listeningHistory.HistoryId)
            {
                return BadRequest();
            }

            _context.Entry(listeningHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListeningHistoryExists(id))
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

        // POST: api/ListeningHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListeningHistory>> PostListeningHistory(ListeningHistory listeningHistory)
        {
            _context.ListeningHistories.Add(listeningHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListeningHistory", new { id = listeningHistory.HistoryId }, listeningHistory);
        }

        // DELETE: api/ListeningHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListeningHistory(int id)
        {
            var listeningHistory = await _context.ListeningHistories.FindAsync(id);
            if (listeningHistory == null)
            {
                return NotFound();
            }

            _context.ListeningHistories.Remove(listeningHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListeningHistoryExists(int id)
        {
            return _context.ListeningHistories.Any(e => e.HistoryId == id);
        }
    }
}
