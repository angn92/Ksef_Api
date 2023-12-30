using JetBrains.Annotations;

namespace KsefClient.KsefSession
{
    public class ContextIdentifier
    {
        public string Type { get; set; }
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
