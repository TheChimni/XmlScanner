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
					}
					catch
					{
						result.SummaryMessage = "Scan Aborted as the XML document does not meet the XSD schema";
						return result;
					}
					result.IsDocumentValid = true;
					DetectErrors(postalAddress, result);
					if (result.ExceedsErrorThreshold)
					{
						return result;
					}
					// warnings
				}
			}
			return result;
		}

		private void DetectErrors(PostalAddress postalAddress, PostalAddressResult result)
		{
			var count = postalAddress.PostalAddresses.Count();
			foreach (var address in postalAddress.PostalAddresses)
			{
				string tag = null;
				if (string.IsNullOrEmpty(address.Address1))
				{
					tag = "<address1/>";
				}
				else if (string.IsNullOrEmpty(address.PostCode))
				{
					tag = "<postcode/>";
				}
				
				if (tag != null)
				{
					if (result.Errors.Count() > count / 10)
					{
						result.ExceedsErrorThreshold = true;
						result.SummaryMessage = "Scan aborted as more than 10% of records are errored";
						break;
					}
					var error = new Error() { Message = string.Format("The {0} tag is empty", tag), Tag = tag };
					result.AddError(error);
				}
			}
		}
	}
}