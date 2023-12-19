using JetBrains.Annotations;

namespace EmployeeDiaryModel.Model
{
    public class Contractor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyLogin { get; set; }
        public string Email { get; set; }
        

        public Contractor([NotNull] string firstName, [NotNull] string lastName, [NotNull] string companyLogin, [NotNull] string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            CompanyLogin = companyLogin;
            Email = email;
        }
    }
}
