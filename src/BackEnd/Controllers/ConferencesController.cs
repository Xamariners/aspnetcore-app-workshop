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
    public class ConferencesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ConferencesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetConferences()
        {
            var conferences = await _db.Conferences.AsNoTracking().ToListAsync();

            var result = conferences.Select(c => new ConferenceDTO.ConferenceResponse
            {
                ID = c.ID,
                Name = c.Name,
                Slug = c.Slug,
                StartDate = c.StartDate,
                Description = c.Description,
                EndDate = c.EndDate,
                Address1 = c.Address1,
                Address2 = c.Address2,
                City = c.City,
                ConferenceOrganisers = c.ConferenceOrganisers,
                Country = c.Country,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                PostCode = c.PostCode,
            });
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetConference([FromRoute] string id)
        {
            var c = await _db.FindAsync<Conference>(id);

            if (c == null)
            {
                return NotFound();
            }
            
            var result = new ConferenceDTO.ConferenceResponse
            {
                ID = c.ID,
                Name = c.Name,
                StartDate = c.StartDate,
                Description = c.Description,
                EndDate = c.EndDate,
                Address1 = c.Address1,
                Address2 = c.Address2,
                City = c.City,
                ConferenceOrganisers = c.ConferenceOrganisers,
                Country = c.Country,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                PostCode = c.PostCode,
            };
            return Ok(result);
        }

        [HttpGet("slug/{id}")]
        public async Task<IActionResult> GetConferenceBySlug([FromRoute] string id)
        {
            var c = await _db.Conferences
                .Include(x => x.ConferenceOrganisers)
                .FirstOrDefaultAsync(x => x.Slug.Equals(id, StringComparison.InvariantCultureIgnoreCase));

            if (c == null)
            {
                return NotFound();
            }

            var result = new ConferenceDTO.ConferenceResponse
            {
                ID = c.ID,
                Name = c.Name,
                Slug = c.Slug,
                StartDate = c.StartDate,
                Description = c.Description,
                EndDate = c.EndDate,
                Address1 = c.Address1,
                Address2 = c.Address2,
                City = c.City,
                ConferenceOrganisers = c.ConferenceOrganisers,
                Country = c.Country,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                PostCode = c.PostCode,
            };
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateConference([FromBody] ConferenceDTO.Conference input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var c = new Conference
            {  
                Name = input.Name,
                StartDate = input.StartDate,
                Description = input.Description,
                EndDate = input.EndDate,
                Address1 = input.Address1,
                Address2 = input.Address2,
                City = input.City,
                Country = input.Country,
                Latitude = input.Latitude,
                Longitude = input.Longitude,
                PostCode = input.PostCode,
            };

            _db.Conferences.Add(c);
            await _db.SaveChangesAsync();

            var result = new ConferenceDTO.ConferenceResponse
            {
                ID = c.ID,
                Name = c.Name,
                StartDate = c.StartDate,
                Description = c.Description,
                EndDate = c.EndDate,
                Address1 = c.Address1,
                Address2 = c.Address2,
                City = c.City,
                ConferenceOrganisers = c.ConferenceOrganisers,
                Country = c.Country,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                PostCode = c.PostCode,
            };

            return CreatedAtAction(nameof(GetConference), new { id = c.ID }, result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateConference([FromRoute]Guid id, [FromBody]ConferenceDTO.Conference input)
        {
            var c = await _db.FindAsync<Conference>(id);

            if (c == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            c.Name = input.Name;

            await _db.SaveChangesAsync();

            var result = new ConferenceDTO.ConferenceResponse
            {
                ID = c.ID,
                Name = c.Name,
                StartDate = c.StartDate,
                Description = c.Description,
                EndDate = c.EndDate,
                Address1 = c.Address1,
                Address2 = c.Address2,
                City = c.City,
                ConferenceOrganisers = c.ConferenceOrganisers,
                Country = c.Country,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                PostCode = c.PostCode,
            };

            return Ok(result);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteConference([FromRoute] Guid id)
        {
            var conference = await _db.FindAsync<Conference>(id);

            if (conference == null)
            {
                return NotFound();
            }

            _db.Remove(conference);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
