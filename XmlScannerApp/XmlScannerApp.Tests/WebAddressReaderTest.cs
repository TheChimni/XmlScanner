using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using XmlScannerApp.Models;
using System.Xml.Serialization;
using System.IO;

namespace XmlScannerApp.Tests
{
	/// <summary>
	/// Test class that tests PostalAddressReader and PostalAddressResult
	/// </summary>
	[TestFixture]
	public class WebAddressReaderTest
	{
		private const string DataFolder = @"..\..\..\XmlScannerApp\Data\WebAddresses";

		//[Test]
		//public void ReadValidPostalAddress()
		//{
		//    var reader = new WebAddressReader(DataFolder);
		//    var result = reader.Read("PostalAddressValidSample1.xml");
		//    Assert.IsNotNull(result);
		//    Assert.IsTrue(result.IsDocumentValid);
		//}

		//[Test]
		//public void ReadInvalidPostalAddress()
		//{
		//    var reader = new WebAddressReader(DataFolder);
		//    var result = reader.Read("PostalAddressInvalidSample1.xml");
		//    Assert.IsNotNull(result);
		//    Assert.IsFalse(result.IsDocumentValid);
		//    Assert.AreEqual("Scan Aborted as the XML document does not meet the XSD schema", result.SummaryMessage);
		//}

		//[Test]
		//public void ReadPostalAddressWithEmptyAddress1()
		//{
		//    var reader = new WebAddressReader(DataFolder);
		//    var result = reader.Read("PostalAddressWithAddress1Empty.xml");
		//    Assert.AreEqual(1, result.Errors.Count());
		//    Assert.AreEqual("<address1/>", result.Errors.First().Tag);
		//}

		//[Test]
		//public void ReadPostalAddressWithEmptyPostcode()
		//{
		//    var reader = new WebAddressReader(DataFolder);
		//    var result = reader.Read("PostalAddressWithPostcodeEmpty.xml");
		//    Assert.AreEqual(1, result.Errors.Count());
		//    Assert.AreEqual("<postcode/>", result.Errors.First().Tag);
		//}

		//[Test]
		//public void AbortPostalAddressWithMorethat10PercentErrors()
		//{
		//    var reader = new WebAddressReader(DataFolder);
		//    var result = reader.Read("PostalAddressWithMoreThan10PercentErrors.xml");
		//    Assert.IsNotNullOrEmpty(result.SummaryMessage);
		//    Assert.IsTrue(result.ExceedsErrorThreshold);
		//    Assert.AreEqual("Scan aborted as more than 10% of records contained errors", result.SummaryMessage);
		//}

		//[Test]
		//public void ReadPostalAddressWithEmptyCityTag()
		//{
		//    var reader = new WebAddressReader(DataFolder);
		//    var result = reader.Read("PostalAddressWithCityEmpty.xml");
		//    Assert.AreEqual(2, result.Warnings.Count());
		//}

		//[Test]
		//public void ReadPostalAddressWhereCountryIsNotSupported()
		//{
		//    var reader = new WebAddressReader(DataFolder);
		//    var result = reader.Read("PostalAddressWithUnknownCountry.xml");
		//    Assert.AreEqual(1, result.Warnings.Count());
		//    Assert.AreEqual("UK isn't within permitted set of countries", result.Warnings.First().Message);
		//}


		// Temporary method to generate test data.
		[Test]
		public void GenerateValidSampleXmlDocument()
		{
			var webAddress = new WebAddress();
			webAddress.WebAddresses = new []
			{
				new WebAddressRecord { Url = "https://github.com/", Tags = "Git Hub tags", 
					Description = "A hub for gits :)", Favicon = "Some icon" },
				new WebAddressRecord { Url = "https://stackoverflow.com/", Tags = "stack overflow tags", 
					Description = "Test your skills", Favicon = "Some icon" }
			};

			var serializer = new XmlSerializer(typeof(WebAddress));
			using (var fileStream = new FileStream(@"..\..\..\XmlScannerApp\Data\WebAddresses\WebAddressValidSample1.xml", FileMode.Create))
			{
				serializer.Serialize(fileStream, webAddress);
			}
		}

		//Temporary method to generate test data.
		//[Test]
		public void GenerateInValidSampleXmlDocumentDoesNotMatchXsd()
		{
			var webAddress = new WebAddress();
			webAddress.WebAddresses = new []
			{
				new WebAddressRecord { Url = "https://github.com/", Tags = "Git Hub tags", 
					Description = "A hub for gits :)", Favicon = "Some icon" },
				new WebAddressRecord { Url = "https://stackoverflow.com/", Tags = "stack overflow tags", 
					Description = "Test your skills", Favicon = "Some icon" }
			};

			var serializer = new XmlSerializer(typeof(WebAddress));
			using (var fileStream = new FileStream(@"..\..\..\XmlScannerApp\Data\WebAddresses\WebAddressInvalidSample1.xml", FileMode.Create))
			{
				serializer.Serialize(fileStream, webAddress);
			}
		}
	}
}
