using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace KsefClient.Helpers
{
    public interface IXmlHelper
    {
        void PrepareInitSessionXmlRequest([NotNull] string pathToXmlFile, [NotNull] string challenge, [NotNull] string identifier);
        byte[] ConvertToBytes([NotNull] string pathToFile);

        string GetXmlAsString([NotNull] string pathToFile);

        string PrepareXadesFile([NotNull] string pathToXmlFile, [NotNull] X509Certificate2 certificate);
    }

    public class XmlHelper : IXmlHelper
    {
        const string XadesPrefix = "xades";
        const string Signature_Id = "Signature";
        const string Signature_Properties_Id = "SignedProperties";
        const string Namespace_Xades = "http://uri.etsi.org/01903/v1.3.2#";

        public void PrepareInitSessionXmlRequest([NotNull] string pathToXmlFile, [NotNull] string challenge, [NotNull] string identifier)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToXmlFile);

            var elementsDictionary = new Dictionary<string, string>
            {
                { "Challenge", challenge},
                { "Identifier", identifier}
            };

            var itemNodes = FindNode(xmlDocument, elementsDictionary);

            for (var index = 0; index < itemNodes.Length; index++)
            {
                if (itemNodes[index].Name == elementsDictionary.ElementAt(index).Key && itemNodes[index].FirstChild.HasChildNodes)
                    itemNodes[index].FirstChild.InnerXml = elementsDictionary.ElementAt(index).Value;
                else
                    itemNodes[index].InnerXml = elementsDictionary.ElementAt(index).Value;
            }

            xmlDocument.Save(pathToXmlFile);
        }

        private static XmlNode[] FindNode([NotNull] XmlDocument xDoc, Dictionary<string, string> elements)
        {
            var nodeItems = new XmlNode[2];

            for (var index = 0; index < elements.Count; index++)
            {
                var item = elements.ElementAt(index);
                nodeItems[index] = xDoc.GetElementsByTagName(item.Key).Item(0);
            }

            return nodeItems;
        }

        public byte[] ConvertToBytes([NotNull] string pathToFile)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToFile);
            return Encoding.UTF8.GetBytes(xmlDocument.OuterXml);
        }

        public string GetXmlAsString([NotNull] string pathToFile)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToFile);
            return xmlDocument.OuterXml;
        }

        public string PrepareXadesFile([NotNull] string pathToXmlFile, [NotNull] X509Certificate2 certificate)
        {
            //Load xml file to add xades section
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToXmlFile);

            var xmlSigned = AddSignedSection(xmlDocument, certificate);

            xmlDocument?.DocumentElement?.AppendChild(xmlDocument.ImportNode(xmlSigned.GetXml(), true));
            return xmlDocument?.OuterXml ?? "";
        }

        private SignedXml AddSignedSection(XmlDocument xmlDocument, [NotNull] X509Certificate2 certificate)
        {
            //Create new instance of SignedXml
            var signedXml = new SignedXml(xmlDocument);
            signedXml.Signature.Id = "Signature";
            signedXml.SignedInfo.CanonicalizationMethod = SignedXml.XmlDsigExcC14NTransformUrl;
            signedXml.SignedInfo.SignatureMethod = SignedXml.XmlDsigRSASHA256Url;
            signedXml.SigningKey = certificate.GetRSAPrivateKey();

            var signatureReference = new Reference { Uri = "" };
            signatureReference.DigestMethod = SignedXml.XmlDsigSHA256Url;
            signatureReference.DigestValue = certificate.RawData;

            signatureReference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(signatureReference);

            signedXml.Signature.SignatureValue = Encoding.ASCII.GetBytes(certificate.GetCertHashString());

            var keyInfo = new KeyInfo();
            var keyX509 = new KeyInfoX509Data(certificate);
            keyInfo.AddClause(keyX509);
            signedXml.KeyInfo = keyInfo;

            AddObjectXadesSection(xmlDocument, signedXml, certificate);

            signedXml.ComputeSignature();

            return signedXml;
        }

        private void AddObjectXadesSection([NotNull] XmlDocument document, [NotNull] SignedXml SignedXml, 
            [NotNull] X509Certificate2 signingCertificate)
        {
            var objectNode = document.CreateElement("Object", SignedXml.XmlDsigNamespaceUrl);

            var qualifyingPropertiesNode = document.CreateElement(XadesPrefix, "QualifyingProperties", Namespace_Xades);
            qualifyingPropertiesNode.SetAttribute("Target", $"#{Signature_Id}");
            objectNode.AppendChild(qualifyingPropertiesNode);

            var signedPropertiesNode = document.CreateElement(XadesPrefix, "SignedProperties", Namespace_Xades);
            signedPropertiesNode.SetAttribute("Id", Signature_Properties_Id);
            qualifyingPropertiesNode.AppendChild(signedPropertiesNode);

            var signedSignaturePropertiesNode = document.CreateElement(XadesPrefix, "SignedSignatureProperties", Namespace_Xades);
            signedPropertiesNode.AppendChild(signedSignaturePropertiesNode);

            var signingTime = document.CreateElement(XadesPrefix, "SigningTime", Namespace_Xades);
            signingTime.InnerText = $"{DateTime.UtcNow.ToString("s")}Z";
            signedSignaturePropertiesNode.AppendChild(signingTime);

            var signingCertificateNode = document.CreateElement(XadesPrefix, "SigningCertificate", Namespace_Xades);
            signedSignaturePropertiesNode.AppendChild(signingCertificateNode);

            var certNode = document.CreateElement(XadesPrefix, "Cert", Namespace_Xades);
            signingCertificateNode.AppendChild(certNode);

            var certDigestNode = document.CreateElement(XadesPrefix, "CertDigest", Namespace_Xades);
            certNode.AppendChild(certDigestNode);

            var digestMethod = document.CreateElement("DigestMethod", SignedXml.XmlDsigNamespaceUrl);
            var digestMethodAlgorithmAtribute = document.CreateAttribute("Algorithm");
            digestMethodAlgorithmAtribute.InnerText = SignedXml.XmlDsigSHA256Url;
            digestMethod.Attributes.Append(digestMethodAlgorithmAtribute);
            certDigestNode.AppendChild(digestMethod);
  
            var digestValue = document.CreateElement("DigestValue", SignedXml.XmlDsigNamespaceUrl);
            digestValue.InnerText = Convert.ToBase64String(signingCertificate.GetCertHash());
            certDigestNode.AppendChild(digestValue);

            var issuerSerialNode = document.CreateElement(XadesPrefix, "IssuerSerial", Namespace_Xades);
            certNode.AppendChild(issuerSerialNode);

            var x509IssuerName = document.CreateElement("X509IssuerName", SignedXml.XmlDsigNamespaceUrl);
            x509IssuerName.InnerText = signingCertificate.Issuer;
            issuerSerialNode.AppendChild(x509IssuerName);

            var x509SerialNumber = document.CreateElement("X509SerialNumber", SignedXml.XmlDsigNamespaceUrl);
            x509SerialNumber.InnerText = signingCertificate.SerialNumber;
            issuerSerialNode.AppendChild(x509SerialNumber);

            var dataObject = new DataObject();
            dataObject.Data = qualifyingPropertiesNode.SelectNodes(".");
            SignedXml.AddObject(dataObject);
        }
    }
}
