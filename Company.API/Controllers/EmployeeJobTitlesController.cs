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
    public class EmployeeJobTitlesController : ControllerBase
    {
        private readonly CompanyContext _context;

        public EmployeeJobTitlesController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeJobTitleDto>>> GetEmployeeJobTitles()
        {
            var employeeJobTitles = await _context.EmployeeJobTitles
                .Select(ejt => new EmployeeJobTitleDto
                {
                    EmployeeID = ejt.EmployeeID,
                    JobTitleID = ejt.JobTitleID,
                    JobTitleName = ejt.JobTitle.TitleName
                })
                .ToListAsync();

            return Ok(employeeJobTitles);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeJobTitleDto>> PostEmployeeJobTitle(EmployeeJobTitleDto employeeJobTitleDto)
        {
            var employeeJobTitle = new Company.Data.Models.EmployeeJobTitle
            {
                EmployeeID = employeeJobTitleDto.EmployeeID,
                JobTitleID = employeeJobTitleDto.JobTitleID
            };

            _context.EmployeeJobTitles.Add(employeeJobTitle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployeeJobTitle", new { employeeId = employeeJobTitle.EmployeeID, jobTitleId = employeeJobTitle.JobTitleID }, employeeJobTitleDto);
        }

        [HttpGet("{employeeId:int}/{jobTitleId:int}")]
        public async Task<ActionResult<EmployeeJobTitleDto>> GetEmployeeJobTitle(int employeeId, int jobTitleId)
        {
            var employeeJobTitle = await _context.EmployeeJobTitles
                .Where(ejt => ejt.EmployeeID == employeeId && ejt.JobTitleID == jobTitleId)
                .Select(ejt => new EmployeeJobTitleDto
                {
                    EmployeeID = ejt.EmployeeID,
                    JobTitleID = ejt.JobTitleID,
                })
                .FirstOrDefaultAsync();

            if (employeeJobTitle == null)
            {
                return NotFound();
            }

            return employeeJobTitle;
        }
    }
}
