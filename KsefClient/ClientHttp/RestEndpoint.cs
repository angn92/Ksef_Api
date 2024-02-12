namespace KsefClient.ClientHttp
{
    public static class RestEndpoint
    {
        public static string AuthChallenge { get; } = "online/Session/AuthorisationChallenge";
        public static string InitSegned { get; } = "online/Session/InitSigned";
    }
}
