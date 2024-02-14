using System.Text;

namespace KsefClient.Common
{
    public static class KsefLogData
    {
        public static string BuildRequestLog(string requestContent, string path, HttpMethod httpMethod)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Request sent");
            sb.AppendLine($"Method: {httpMethod}");
            sb.AppendLine($"Path: {path}");
            sb.AppendLine($"Request content: {requestContent}");
            sb.AppendLine($"Headers: "); //todo: add headers to log

            return sb.ToString();
        }

        public static string BuildResponseLog(string responseContent, string path, HttpMethod httpMethod, int statusCode) 
        {
            var sb = new StringBuilder();

            sb.AppendLine("Response received");
            sb.AppendLine($"Method: {httpMethod}");
            sb.AppendLine($"Status code: {statusCode}");
            sb.AppendLine($"Path: {path}");
            sb.AppendLine($"Request content: {responseContent}");
            sb.AppendLine($"Headers: "); //todo: add headers to log

            return sb.ToString();
        }
    }
}
