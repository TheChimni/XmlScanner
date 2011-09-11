using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace XmlScannerApp.Models
{
	public class PostalAddressReader
	{
		public PostalAddressResult Read(string path)
		{
			using (var xmlReader = new XmlTextReader(path))
			{
				var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
				settings.Schemas.Add("urn:PostalAddress-schema",
					@"C:\Users\Reeithaa\Documents\Visual Studio 2010\Projects\XMLScanner\XmlScannerApp\XmlScannerApp\Data\PostalAddress.xsd");
				using (var xmlValidatingReader = XmlReader.Create(xmlReader, settings))
				{
					var serializer = new XmlSerializer(typeof(PostalAddress));
					try
					{
						var postalAddress = (PostalAddress)serializer.Deserialize(xmlValidatingReader);
					}
					catch
					{
						return new PostalAddressResult() { IsDocumentValid = false };
					}
				}
			}
			return new PostalAddressResult() { IsDocumentValid = true };
		}		
	}
}