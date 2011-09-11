using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using XmlScannerApp.Models;
using System.Configuration;

namespace XmlScannerApp.Models
{
	public class PostalAddressReader
	{
		private const string SchemaFileName = "PostalAddress.xsd";
		private const string DefaultCountryList = "England,France,Germany,Japan,United States";
		private string DataFolder { get; set; }

		public PostalAddressReader(string dataFolder)
		{
			DataFolder = dataFolder;
		}

		public IEnumerable<string> GetFileNames()
		{
			return Directory.GetFiles(DataFolder).Select(x => x.Substring(x.LastIndexOf('\\') + 1));
		}

		public PostalAddressResult Read(string path)
		{
			var result = new PostalAddressResult();
			using (var xmlReader = new XmlTextReader(string.Format(@"{0}\{1}", DataFolder, path)))
			{
				var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
				settings.Schemas.Add("urn:PostalAddress-schema", string.Format(@"{0}\{1}", DataFolder, SchemaFileName));
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
					DetectWarnings(postalAddress, result);
				}
			}
			return result;
		}

		private void DetectWarnings(PostalAddress postalAddress, PostalAddressResult result)
		{
			var countryList = ConfigurationManager.AppSettings.Get("CountryList") ?? DefaultCountryList;
			var permittedCountries = countryList.Split(new[] { ',' });
			foreach (var address in postalAddress.PostalAddresses)
			{
				string tag = null;
				Warning warning = null;
				if (string.IsNullOrEmpty(address.City))
				{
					tag = "<city/>";
					warning = new Warning() { Message = string.Format("The {0} tag is empty", tag), Tag = tag };
					
				}
				else if(!permittedCountries.Contains(address.Country))
				{
					tag = "<country/>";
					warning = new Warning() 
					{ 
						Message = string.Format("{0} isn't within permitted set of countries",
						address.Country.Trim()),
						Tag = tag
					};
				}
				if (warning != null)
				{
					result.AddWarning(warning);
				}
			}
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