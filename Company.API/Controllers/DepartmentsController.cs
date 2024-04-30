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
    public class DepartmentsController : ControllerBase
    {
        private readonly CompanyContext _context;

        public DepartmentsController(CompanyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetDepartments()
        {
            var departments = await _context.Departments
                .Select(d => new DepartmentDto
                {
                    DepartmentID = d.DepartmentID,
                    DepartmentName = d.DepartmentName,
                    CompanyID = d.CompanyID
                })
                .ToListAsync();

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DepartmentDto>> GetDepartment(int id)
        {
            var department = await _context.Departments
                .Where(d => d.DepartmentID == id)
                .Select(d => new DepartmentDto
                {
                    DepartmentID = d.DepartmentID,
                    DepartmentName = d.DepartmentName,
                    CompanyID = d.CompanyID 
                })
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return NotFound();
            }

            return department;
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentDto>> PostDepartment(DepartmentDto departmentDto)
        {
            var department = new Company.Data.Models.Department
            {
                DepartmentName = departmentDto.DepartmentName,
                CompanyID = departmentDto.CompanyID
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.DepartmentID }, departmentDto);
        }

    }
}
