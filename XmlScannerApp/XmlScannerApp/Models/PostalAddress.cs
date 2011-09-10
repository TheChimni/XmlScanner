using System.Xml.Serialization;

namespace XmlScannerApp.Models
{
	[XmlType(AnonymousType = true, Namespace = "urn:PostalAddress-schema")]
	[XmlRoot(Namespace = "urn:PostalAddress-schema", IsNullable = false)]
	public class PostalAddress
	{
		[XmlElement("PostalAddressRow")]
		public PostalAddressRecord[] PostalAddresses { get; set; }
	}
	
	[XmlType(Namespace = "urn:PostalAddress-schema")]
	public class PostalAddressRecord
	{
		[XmlElement("address1")]
		public string Address1 { get; set; }
		[XmlElement("address2")]
		public string Address2 { get; set; }
		[XmlElement("address3")]
		public string Address3 { get; set; }
		[XmlElement("city")]
		public string City { get; set; }
		[XmlElement("postcode")]
		public string PostCode { get; set; }
		[XmlElement("country")]
		public string Country { get; set; }
	}
}