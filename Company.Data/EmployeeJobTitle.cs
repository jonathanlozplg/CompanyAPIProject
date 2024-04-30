namespace Company.Data.Models
{
    public class EmployeeJobTitle
    {
        public int EmployeeID { get; set; }
        public int JobTitleID { get; set; }

        public Employee Employee { get; set; }
        public JobTitle JobTitle { get; set; }
    }
}
