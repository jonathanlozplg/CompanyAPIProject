using Microsoft.EntityFrameworkCore;
using Company.Data.Models;

namespace Company.Data
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Company.Data.Models.Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<EmployeeJobTitle> EmployeeJobTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeJobTitle>()
                .HasKey(ejt => new { ejt.EmployeeID, ejt.JobTitleID });
        }
    }
}
