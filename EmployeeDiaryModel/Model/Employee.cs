using JetBrains.Annotations;

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
        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public Employee()
        {
        }

        public Employee([NotNull] string firstName, [NotNull] string lastName, [NotNull] string companyLogin, [NotNull] string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            CompanyLogin = companyLogin;
            Email = email;
        }
    }
}
