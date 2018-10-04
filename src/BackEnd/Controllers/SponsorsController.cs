using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using ConferenceDTO;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class SponsorsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public SponsorsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("conference/{id:Guid}")]
        public async Task<IActionResult> GetConferenceSponsors([FromRoute] Guid id)
        {
            var sponsors = await _db.Sponsors.AsNoTracking().Where(x => x.ParentID == id).ToListAsync();
            return Ok(sponsors);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetSponsor([FromRoute] Guid id)
        {
            var gc = await _db.FindAsync<Sponsor>(id);

            if (gc == null)
            {
                return NotFound();
            }
            
            return Ok(gc);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSponsor([FromBody] Sponsor input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _db.Sponsors.Add(input);
            await _db.SaveChangesAsync();
            
            return CreatedAtAction(nameof(CreateSponsor), new { id = input.ID }, input);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateSponsor([FromRoute]Guid id, [FromBody]Sponsor input)
        {
            var gc = await _db.FindAsync<Sponsor>(id);

            if (gc == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            gc.Name = input.Name;

            await _db.SaveChangesAsync();
            
            return Ok(gc);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteSponsor([FromRoute] Guid id)
        {
            var gc = await _db.FindAsync<Sponsor>(id);

            if (gc == null)
            {
                return NotFound();
            }

            _db.Remove(gc);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
