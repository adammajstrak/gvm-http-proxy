using System.Xml.Serialization;

namespace GvmHttpProxy.Models
{
    [XmlRoot("Token")]
    public class Token
    {
        [XmlElement("Bearer")]
        public string? Bearer;
    }
}
