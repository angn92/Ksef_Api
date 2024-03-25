namespace KsefClient.ClientHttp
{
    public static class RestEndpoint
    {
        /// <summary>
        /// Interactive interface - session
        /// </summary>
        public static string AuthChallenge { get; } = "online/Session/AuthorisationChallenge";
        public static string InitSegned { get; } = "online/Session/InitSigned";
        public static string InitToken { get; } = "online/Session/InitToken";
        public static string Terminate { get; } = "online/Session/Terminate";

        /// <summary>
        /// Interactive interface - credentials
        /// </summary>
        public static string GenerateToken { get; } = "online/Credentials/GenerateToken";
        public static string RevokeToken { get; } = "online/Credentials/RevokeToken";

        /// <summary>
        /// Interactive interface - invoices
        /// </summary>
        public static string SendInvoice { get; } = "online/Invoice/Send";
        public static string InvoiceStatus { get; } = "online/Invoice/Status/{InvoiceElementReferenceNumber}";
        public static string GetInvoice { get; } = "online/Invoice/Status/{KsefReferenceNumber}";
    }
}
