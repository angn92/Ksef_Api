using JetBrains.Annotations;

namespace KsefClient.Helpers
{
    public interface IUriHelper
    {
        string GenerateUri([NotNull] string baseAddress, [NotNull] string action);
    }
    
    public class UriHelper : IUriHelper
    {
        public string GenerateUri([NotNull] string baseAddress, [NotNull] string action)
        {
            if(!baseAddress.EndsWith('/'))
            {
                baseAddress += "/";
            }

            return String.Concat(baseAddress, action);
        }
    }
}
