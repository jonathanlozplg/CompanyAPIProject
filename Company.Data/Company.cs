namespace Company.Data.Models
{
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string OrganizationNumber { get; set; }

        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
