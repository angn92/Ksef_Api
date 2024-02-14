using EmployeeDiaryModel.Enums;
using System.Text.Json.Serialization;

namespace KsefInfrastructure.Contract.Token.Validate
{
    public class TokenStatusResponse
    {
        [JsonPropertyName("status")]
        public TokenStatus Status { get; set; }

        [JsonPropertyName("modifiedTime")]
        public DateTime ModifiedTime { get; set; }
    }
}
