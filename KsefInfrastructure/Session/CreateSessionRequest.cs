using KsefInfrastructure.Command;
using System.Text.Json.Serialization;

namespace KsefInfrastructure.Session
{
    public class CreateSessionRequest : IRequest
    {
        public CreateSessionCommand Command { get; set; }

        public CreateSessionRequest(CreateSessionCommand command)
        {
            Command = command;
        }
    }

    public class CreateSessionCommand
    {
        [JsonPropertyName("Nip")]
        public string NIP { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }
    }
}
