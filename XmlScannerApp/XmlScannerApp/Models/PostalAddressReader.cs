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

		//DefaultCountryList constant is used only for unit testing the code
		private const string DefaultCountryList = "England,France,Germany,Japan,United States";
		private const string XmlFolder = "PostalAddresses";
		private string DataFolder { get; set; }

		/// <summary>
		/// Parameterised constructor that sets the path for the Data folder in the project
		/// </summary>
		/// <param name="dataFolder"></param>
		public PostalAddressReader(string dataFolder)
		{
			DataFolder = dataFolder;
		}

		/// <summary>
		/// Returns a list of all the file names in a given data folder.
		/// </summary>
		/// <returns>IEnumerable of strings</returns>
		public IEnumerable<string> GetFileNames()
		{
			return Directory.GetFiles(string.Format(@"{0}\{1}", DataFolder, XmlFolder)).Select(x => x.Substring(x.LastIndexOf('\\') + 1));
		}

		/// <summary>
		/// Reads a given sample XML document from the path specfied and scans it for Errors and Warnings 
		/// </summary>
		/// <param name="path"></param>
		/// <returns>An instance of PostalAddressResult</returns>
		public PostalAddressResult Read(string path)
		{
			var result = new PostalAddressResult();
			using (var xmlReader = new XmlTextReader(string.Format(@"{0}\{1}\{2}", DataFolder, XmlFolder, path)))
			{
				var settings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
				settings.Schemas.Add("urn:PostalAddress-schema", string.Format(@"{0}\{1}", DataFolder, SchemaFileName));
				using (var xmlValidatingReader = XmlReader.Create(xmlReader, settings))
				{
					var serializer = new XmlSerializer(typeof(PostalAddress));
					PostalAddress postalAddress = null;
					try
					{
						// Deserialise the XML to PostalAddress
						postalAddress = (PostalAddress)serializer.Deserialize(xmlValidatingReader);
					}
					catch
					{
						result.SummaryMessage = "Scan Aborted as the XML document does not meet the XSD schema";
						return result;
					}
					result.IsDocumentValid = true;
					// Detect Errors in the document
					DetectErrors(postalAddress, result);
					if (result.ExceedsErrorThreshold)
					{
						// Abort scan
						return result;
					}
					// Detect Warnings in the document
					DetectWarnings(postalAddress, result);

					//Create a scan summary message for errors and warnings 
					result.SummaryMessage = string.Format("Scan completed with {0} errors and {1} warnings",
						result.Errors.Count(),
						result.Warnings.Count());
				}
			}
			return result;
		}


		/// <summary>
		/// Detects warning messages for a given XML document based on some rules
		/// </summary>
		/// <param name="postalAddress"></param>
		/// <param name="result"></param>
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

		/// <summary>
		/// Detects errors in an XML document during scan based on some validation rules
		/// </summary>
		/// <param name="postalAddress"></param>
		/// <param name="result"></param>
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
					//Abort scan if 10% of records in the document contain errors
					if (result.Errors.Count() > count / 10)
					{
						result.ExceedsErrorThreshold = true;
						result.SummaryMessage = "Scan aborted as more than 10% of records contained errors";
						break;
					}
					var error = new Error() { Message = string.Format("The {0} tag is empty", tag), Tag = tag };
					result.AddError(error);
				}
			}
		}
	}
}