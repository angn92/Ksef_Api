using System.Text.Json.Serialization;

namespace KsefClient.KsefContract.Session.InitSigned
{
    public class InitSignedResponse
    {
        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("referenceNumber")]
        public string ReferenceNumber { get; set; }

        [JsonPropertyName("sessionToken")]
        public SessionToken SessionToken { get; set; }
    }

    public class SessionToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("context")]
        public Context Context { get; set; }
    }

    public class Context
    {
        [JsonPropertyName("contextIdentifier")]
        public ContextIdentifier ContextIdentifier { get; set; }

        [JsonPropertyName("contextName")]
        public ContextName ContextName { get; set; }

        [JsonPropertyName("credentialsRoleList")]
        public CredentialsRoleList CredentialsRoleList { get; set; }
    }

    public class ContextIdentifier
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
    }

    public class ContextName
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("tradeName")]
        public string Identifier { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
    }

    public class CredentialsRoleList
    {
        public IList<CredentialsRole> CredentialsRole { get; set; }
    }

    public class CredentialsRole
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("roleType")]
        public string RoleType { get; set; }

        [JsonPropertyName("roleDescription")]
        public string RoleDescription { get; set; }
    }
}
