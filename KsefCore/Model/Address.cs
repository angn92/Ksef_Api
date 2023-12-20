using EmployeeDiaryModel.Model;
using System.Diagnostics.CodeAnalysis;

namespace KsefCore.Model
{
    public class Address
    {
        public Guid Id { get; set; }
        public string FullAddress { get; set; }
        public Guid ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public Address()
        {
        }

        public Address([NotNull] string addres)
        {
            Id = Guid.NewGuid();
            FullAddress = addres;
        }
    }
}
