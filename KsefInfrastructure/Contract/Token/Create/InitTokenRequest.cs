using KsefInfrastructure.Command;
using System.Text.Json.Serialization;

namespace KsefInfrastructure.Contract.Token.Create
{
    public class InitTokenRequest : IRequest
    {
        public InitTokenCommand Command { get; set; }

        public InitTokenRequest(InitTokenCommand command)
        {
            Command = command;
        }
    }

    public class InitTokenCommand
    {
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}