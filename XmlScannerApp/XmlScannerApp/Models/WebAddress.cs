using System.Xml.Serialization;

namespace XmlScannerApp.Models
{
	[XmlType(AnonymousType = true, Namespace = "urn:WebAddress-schema")]
	[XmlRoot(Namespace = "urn:WebAddress-schema", IsNullable = false)]
	public class WebAddress
	{
		[XmlElement("WebAddressRow")]
		public WebAddressRecord[] WebAddresses { get; set; }
	}

	[XmlType(Namespace = "urn:WebAddress-schema")]
	public class WebAddressRecord
	{
		[XmlElement("url")]
		public string Url { get; set; }
		[XmlElement("description")]
		public string Description { get; set; }
		[XmlElement("favicon")]
		public string Favicon { get; set; }
		[XmlElement("tags")]
		public string Tags { get; set; }
	}
}