using Microsoft.AspNetCore.Mvc;
using Company.Data;
using Company.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTitlesController : ControllerBase
    {
        private readonly CompanyContext _context;

        public JobTitlesController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTitleDto>>> GetJobTitles()
        {
            var jobTitles = await _context.JobTitles
                .Select(jt => new JobTitleDto
                {
                    JobTitleID = jt.JobTitleID,
                    TitleName = jt.TitleName
                })
                .ToListAsync();

            return Ok(jobTitles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JobTitleDto>> GetJobTitle(int id)
        {
            var jobTitle = await _context.JobTitles
                .Where(jt => jt.JobTitleID == id)
                .Select(jt => new JobTitleDto
                {
                    JobTitleID = jt.JobTitleID,
                    TitleName = jt.TitleName
                })
                .FirstOrDefaultAsync();

            if (jobTitle == null)
            {
                return NotFound();
            }

            return jobTitle;
        }

        [HttpPost]
        public async Task<ActionResult<JobTitleDto>> PostJobTitle(JobTitleDto jobTitleDto)
        {
            var jobTitle = new Company.Data.Models.JobTitle
            {
                TitleName = jobTitleDto.TitleName
            };

            _context.JobTitles.Add(jobTitle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobTitle), new { id = jobTitle.JobTitleID }, jobTitleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobTitle(int id, JobTitleDto jobTitleDto)
        {
            if (id != jobTitleDto.JobTitleID)
            {
                return BadRequest();
            }

            var jobTitle = await _context.JobTitles.FindAsync(id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            jobTitle.TitleName = jobTitleDto.TitleName;

            _context.Entry(jobTitle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.JobTitles.Any(jt => jt.JobTitleID == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTitle(int id)
        {
            var jobTitle = await _context.JobTitles.FindAsync(id);
            if (jobTitle == null)
            {
                return NotFound();
            }

            _context.JobTitles.Remove(jobTitle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
