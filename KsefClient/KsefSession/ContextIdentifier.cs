using JetBrains.Annotations;
using Newtonsoft.Json;

namespace KsefClient.KsefSession
{
    public class ContextIdentifier
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        public ContextIdentifier()
        {
        }

        public ContextIdentifier([NotNull] string type, [NotNull] string identifier)
        {
            Type = type;
            Identifier = identifier;
        }
    }
}
