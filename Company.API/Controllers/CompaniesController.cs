using Microsoft.AspNetCore.Mvc;
using Company.Data;
using Company.Data.Models;
using Company.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyContext _context;

        public CompaniesController(CompanyContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            return await _context.Companies
                .Select(c => new CompanyDto
                {
                    CompanyID = c.CompanyID,
                    CompanyName = c.CompanyName,
                    OrganizationNumber = c.OrganizationNumber
                })
                .ToListAsync();
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(int id)
        {
            var company = await _context.Companies
                .Where(c => c.CompanyID == id)
                .Select(c => new CompanyDto
                {
                    CompanyID = c.CompanyID,
                    CompanyName = c.CompanyName,
                    OrganizationNumber = c.OrganizationNumber
                })
                .FirstOrDefaultAsync();

            if (company == null)
            {
                return NotFound();
            }

            return company;
        }

        // POST: api/Companies
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyDto companyDto)
        {
            var company = new Company.Data.Models.Company
            {
                CompanyName = companyDto.CompanyName,
                OrganizationNumber = companyDto.OrganizationNumber
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCompany), new { id = company.CompanyID }, companyDto);
        }

        // ... You can also add PUT and DELETE as needed

        // Remember to handle model validation, mapping, error handling, etc.
    }
}
