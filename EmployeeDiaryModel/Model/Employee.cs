namespace EmployeeDiaryModel.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyLogin { get; set; }
        public string Email { get; set; }
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        
        
    }
}
