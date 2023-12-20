using JetBrains.Annotations;
using KsefCore.Model;

namespace EmployeeDiaryModel.Model
{
    public class Contractor
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nip { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }


        public Contractor([NotNull] string firstName, [NotNull] string lastName, [NotNull] string nip, [NotNull] string email, [NotNull] string address)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Nip = nip;
            Email = email;
            Address = new Address(address);
        }
    }
}
