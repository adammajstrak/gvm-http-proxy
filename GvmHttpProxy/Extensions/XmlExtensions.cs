using System.Xml.Linq;

namespace GvmHttpProxy.Extensions
{
    public static class XmlExtensions
    {
        public static string Serialize(this XElement element)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append('<').Append(element.Name);

            foreach (var attr in element.Attributes())
            {
                sb.Append(' ')
                  .Append(attr.Name)
                  .Append('=')
                  .Append('\'')
                  .Append(attr.Value)
                  .Append('\'');
            }

            if (element.HasElements || !string.IsNullOrEmpty(element.Value))
            {
                sb.Append('>');

                foreach (var child in element.Elements())
                {
                    sb.Append(Serialize(child));
                }

                if (!string.IsNullOrEmpty(element.Value))
                {
                    sb.Append(element.Value);
                }

                sb.Append("</").Append(element.Name).Append('>');
            }
            else
            {
                sb.Append(" />");
            }

            return sb.ToString();
        }
    }
}
