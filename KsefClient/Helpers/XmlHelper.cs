using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace KsefClient.Helpers
{
    public interface IXmlHelper
    {
        void PrepareInitSessionXmlRequest([NotNull] string pathToXmlFile, [NotNull] string challenge, [NotNull] string identifier);
    }

    public class XmlHelper : IXmlHelper
    {
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

            for ( var index = 0; index < itemNodes.Length; index++)
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

            for ( var index = 0; index < elements.Count; index++)
            {
                var item = elements.ElementAt(index);
                nodeItems[index] = xDoc.GetElementsByTagName(item.Key).Item(0);
            }

            return nodeItems;
        }
    }
}
