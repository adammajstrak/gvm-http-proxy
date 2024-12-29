using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace GvmHttpProxy.Models
{
    [XmlRoot("Credentials")]
    public class Credentials
    {
        [XmlElement("Username")]
        [Required(ErrorMessage = "The Username field is required.")]
        public string? Username { get; set; }

        [XmlElement(ElementName = "Password")]
        [Required(ErrorMessage = "The Password field is required.")]
        public string? Password { get; set; }
    }
}
