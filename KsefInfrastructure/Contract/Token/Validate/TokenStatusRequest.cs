using KsefInfrastructure.Command;
using System.Text.Json.Serialization;

namespace KsefInfrastructure.Contract.Token.Validate
{
    public class TokenStatusRequest : IRequest
    {
        [JsonPropertyName("identifer")]
        public string Identifier { get; set; }
    }
}