namespace Company.Data.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public int CompanyID { get; set; }

        public Company Company { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
