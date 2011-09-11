using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using XmlScannerApp.Models;

namespace XmlScannerApp.Models
{
	public class PostalAddressReader
	{
		public PostalAddressResult Read(string path)
		{
			var result = new PostalAddressResult();
			using (var xmlReader = new XmlTextReader(path))
			{
				var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
				settings.Schemas.Add("urn:PostalAddress-schema",
					@"C:\Users\Reeithaa\Documents\Visual Studio 2010\Projects\XMLScanner\XmlScannerApp\XmlScannerApp\Data\PostalAddress.xsd");
				using (var xmlValidatingReader = XmlReader.Create(xmlReader, settings))
				{
					var serializer = new XmlSerializer(typeof(PostalAddress));
					PostalAddress postalAddress = null;
					try
					{
						postalAddress = (PostalAddress)serializer.Deserialize(xmlValidatingReader);
						//var address1Tag =  postalAddress != null && postalAddress.PostalAddresses.Count() > 0 
						//    ? postalAddress.PostalAddresses.each
					}
					catch
					{
						return result;
					}
					result.IsDocumentValid = true;
					DetectErrors(postalAddress, result);
					// warnings
				}
			}
			return result;
		}

		private void DetectErrors(PostalAddress postalAddress, PostalAddressResult result)
		{
			foreach (var address in postalAddress.PostalAddresses)
			{
				if (string.IsNullOrEmpty(address.Address1))
				{
					var tag = "<address1/>";
					result.AddError(new Error() { Message = string.Format("The {0} tag is empty", tag), Tag = tag });
				}
				else if (string.IsNullOrEmpty(address.PostCode))
				{
					var tag = "<postcode/>";
					result.AddError(new Error() { Message = string.Format("The {0} tag is empty", tag), Tag = tag });
				}
			}
		}
	}
}