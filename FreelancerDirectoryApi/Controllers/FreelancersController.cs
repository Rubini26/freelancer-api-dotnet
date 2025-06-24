using FreelancerDirectoryApi.Data;
using FreelancerDirectoryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreelancerDirectoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FreelancersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Freelancers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Freelancer>>> GetFreelancers()
        {
            return await _context.Freelancers
                .Include(f => f.SkillSets)
                .Include(f => f.Hobbies)
                .Where(f => !f.IsArchived)
                .ToListAsync();
        }

        // GET: api/Freelancers/search?term=abc
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Freelancer>>> Search(string term)
        {
            return await _context.Freelancers
                .Include(f => f.SkillSets)
                .Include(f => f.Hobbies)
                .Where(f => (f.Username.Contains(term) || f.Email.Contains(term)) && !f.IsArchived)
                .ToListAsync();
        }

        // POST: api/Freelancers
        [HttpPost]
        public async Task<ActionResult<Freelancer>> CreateFreelancer(Freelancer freelancer)
        {
            // Clear navigation properties to prevent validation errors
            if (freelancer.SkillSets != null)
            {
                foreach (var skill in freelancer.SkillSets)
                {
                    skill.FreelancerId = 0;
                    skill.Freelancer = null;
                }
            }

            if (freelancer.Hobbies != null)
            {
                foreach (var hobby in freelancer.Hobbies)
                {
                    hobby.FreelancerId = 0;
                    hobby.Freelancer = null;
                }
            }

            _context.Freelancers.Add(freelancer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFreelancers), new { id = freelancer.Id }, freelancer);
        }



        // PUT: api/Freelancers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFreelancer(int id, Freelancer freelancer)
        {
            if (id != freelancer.Id)
                return BadRequest();

            _context.Entry(freelancer).State = EntityState.Modified;

            // Update child collections manually
            _context.SkillSets.RemoveRange(_context.SkillSets.Where(s => s.FreelancerId == id));
            _context.Hobbies.RemoveRange(_context.Hobbies.Where(h => h.FreelancerId == id));
            _context.SkillSets.AddRange(freelancer.SkillSets);
            _context.Hobbies.AddRange(freelancer.Hobbies);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Freelancers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFreelancer(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null)
                return NotFound();

            _context.Freelancers.Remove(freelancer);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Freelancers/5/archive
        [HttpPut("{id}/archive")]
        public async Task<IActionResult> ArchiveFreelancer(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null)
                return NotFound();

            freelancer.IsArchived = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Freelancers/5/unarchive
        [HttpPut("{id}/unarchive")]
        public async Task<IActionResult> UnarchiveFreelancer(int id)
        {
            var freelancer = await _context.Freelancers.FindAsync(id);
            if (freelancer == null)
                return NotFound();

            freelancer.IsArchived = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
