using JetBrains.Annotations;
using KsefInfrastructure.Validation;
using System.Security.Cryptography.X509Certificates;

namespace KsefInfrastructure.Helper
{
    public interface ICertificateHelper
    {
        X509Certificate2 GetCertificate([NotNull] string thumbprint, StoreLocation location);
    }

    public class CertificateHelper : ICertificateHelper
    {
        public X509Certificate2 GetCertificate([NotNull] string thumbprint, StoreLocation location)
        {
            Fail.IfNull(thumbprint);

            using (var store = new X509Store(location))
            {
                store.Open(OpenFlags.ReadOnly);
                var cert = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint.ToUpper(), false);

                return cert.OfType<X509Certificate2>().FirstOrDefault();
            }
        }
    }
}
