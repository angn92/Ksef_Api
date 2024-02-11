using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KsefClient.Common
{
    public class InitSessionTokenRequest
    {
        public Context Context { get; set; }
    }

    public class Context
    {
        public string Challenge { get; set; }
        public string Identifier { get; set; }
        public DocumentType DocumentType { get; set; }

    }

    public class DocumentType
    {
        public string Service { get; set; }
        public FormCode FormCode { get; set; }
    }

    public class FormCode
    {
        public string SystemCode { get; set; }
        public string SchemaVersion { get; set; }
        public string TargetNamespace { get; set; }
        public string Value { get; set; }
    }
}
