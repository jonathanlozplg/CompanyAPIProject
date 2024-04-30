namespace Company.Data.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int DepartmentID { get; set; }

        public Department Department { get; set; }

        public decimal Salary { get; set; }
        public bool IsUnionMember { get; set; }

        public List<EmployeeJobTitle> EmployeeJobTitles { get; set; } = new List<EmployeeJobTitle>();
    }
}
