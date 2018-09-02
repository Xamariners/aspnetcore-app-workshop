using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class GlobalConferenceController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GlobalConferenceController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetGlobalConferences()
        {
            var globalConference = await _db.GlobalConferences.AsNoTracking().ToListAsync();

            var result = globalConference.Select(gc => new ConferenceDTO.GlobalConferenceResponse
            {
                ID = gc.ID,
                Name = gc.Name,
                Description = gc.Description,
                StartDate = gc.StartDate,
                Summary = gc.Summary,
                EndDate = gc.EndDate,
                Location = gc.Location,
                TagLine = gc.TagLine
            });
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetGlobalConference([FromRoute] Guid id)
        {
            var gc = await _db.FindAsync<GlobalConference>(id);

            if (gc == null)
            {
                return NotFound();
            }
            
            var result = new ConferenceDTO.GlobalConferenceResponse
            {
                ID = gc.ID,
                Name = gc.Name,
                Description = gc.Description,
                StartDate = gc.StartDate,
                Summary = gc.Summary,
                EndDate = gc.EndDate,
                Location = gc.Location,
                TagLine = gc.TagLine
            };
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConference([FromBody] ConferenceDTO.GlobalConference input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var gc = new GlobalConference
            {  
                Name = input.Name,
                Description = input.Description,
                StartDate = input.StartDate,
                Summary = input.Summary,
                EndDate = input.EndDate,
                Location = input.Location,
                TagLine = input.TagLine
            };

            _db.GlobalConferences.Add(gc);
            await _db.SaveChangesAsync();

            var result = new ConferenceDTO.GlobalConferenceResponse
            {
                ID = gc.ID,
                Name = gc.Name,
                Description = gc.Description,
                StartDate = gc.StartDate,
                Summary = gc.Summary,
                EndDate = gc.EndDate,
                Location = gc.Location,
                TagLine = gc.TagLine
            };

            return CreatedAtAction(nameof(GetGlobalConference), new { id = gc.ID }, result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateConference([FromRoute]Guid id, [FromBody]ConferenceDTO.GlobalConference input)
        {
            var gc = await _db.FindAsync<GlobalConference>(id);

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

            var result = new ConferenceDTO.GlobalConferenceResponse
            {
                ID = gc.ID,
                Name = gc.Name,
                Description = gc.Description,
                StartDate = gc.StartDate,
                Summary = gc.Summary,
                EndDate = gc.EndDate,
                Location = gc.Location,
                TagLine = gc.TagLine
            };

            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteConference([FromRoute] Guid id)
        {
            var gc = await _db.FindAsync<GlobalConference>(id);

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
