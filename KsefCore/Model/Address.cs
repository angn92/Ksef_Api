using System.Diagnostics.CodeAnalysis;

namespace KsefCore.Model
{
    public class Address
    {
        public Guid Id { get; set; }
        public string FullAddress { get; set; }

        public Address([NotNull] string addres)
        {
            Id = Guid.NewGuid();
            FullAddress = addres;
        }
    }
}
