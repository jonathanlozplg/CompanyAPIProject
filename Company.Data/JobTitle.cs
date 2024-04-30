namespace Company.Data.Models
{
    public class JobTitle
    {
        public int JobTitleID { get; set; }
        public string TitleName { get; set; }

        public List<EmployeeJobTitle> EmployeeJobTitles { get; set; } = new List<EmployeeJobTitle>();
    }
}
