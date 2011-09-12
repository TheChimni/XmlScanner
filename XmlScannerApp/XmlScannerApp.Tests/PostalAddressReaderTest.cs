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
	public class PostalAddressReaderTest
	{
		private const string DataFolder = @"..\..\..\XmlScannerApp\Data\";

		[Test]
		public void ReadValidPostalAddress()
		{
			var reader = new PostalAddressReader(DataFolder);
			var result = reader.Read("PostalAddressValidSample1.xml");
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsDocumentValid);
		}

		[Test]
		public void ReadInvalidPostalAddress()
		{
			var reader = new PostalAddressReader(DataFolder);
			var result = reader.Read("PostalAddressInvalidSample1.xml");
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsDocumentValid);
			Assert.AreEqual("Scan Aborted as the XML document does not meet the XSD schema", result.SummaryMessage);			
		}

		[Test]
		public void ReadPostalAddressWithEmptyAddress1()
		{
			var reader = new PostalAddressReader(DataFolder);
			var result = reader.Read("PostalAddressWithAddress1Empty.xml");
			Assert.AreEqual(1, result.Errors.Count());
			Assert.AreEqual("<address1/>", result.Errors.First().Tag);
		}

		[Test]
		public void ReadPostalAddressWithEmptyPostcode()
		{
			var reader = new PostalAddressReader(DataFolder);
			var result = reader.Read("PostalAddressWithPostcodeEmpty.xml");
			Assert.AreEqual(1, result.Errors.Count());
			Assert.AreEqual("<postcode/>", result.Errors.First().Tag);
		}

		[Test]
		public void AbortPostalAddressWithMorethat10PercentErrors()
		{
			var reader = new PostalAddressReader(DataFolder);
			var result = reader.Read("PostalAddressWithMoreThan10PercentErrors.xml");
			Assert.IsNotNullOrEmpty(result.SummaryMessage);
			Assert.IsTrue(result.ExceedsErrorThreshold);
			Assert.AreEqual("Scan aborted as more than 10% of records contained errors", result.SummaryMessage);
		}

		[Test]
		public void ReadPostalAddressWithEmptyCityTag()
		{
			var reader = new PostalAddressReader(DataFolder);
			var result = reader.Read("PostalAddressWithCityEmpty.xml");
			Assert.AreEqual(2, result.Warnings.Count());
		}

		[Test]
		public void ReadPostalAddressWhereCountryIsNotSupported()
		{
			var reader = new PostalAddressReader(DataFolder);
			var result = reader.Read("PostalAddressWithUnknownCountry.xml");
			Assert.AreEqual(1, result.Warnings.Count());
			Assert.AreEqual("UK isn't within permitted set of countries", result.Warnings.First().Message);
		}

		//[Test]
		public void GenerateValidSampleXmlDocument()
		{
			var postalAddress = new PostalAddress();
			postalAddress.PostalAddresses = new[]
			{
				new PostalAddressRecord { Address1 ="No. 10", Address2 = "Downing Street", Address3 = "", 
					City = "London", Country = "UK", PostCode = "W1 1AA" },
 				new PostalAddressRecord { Address1 = "Andrews House" , Address2 = "College Road", Address3 = "", 
					City = "Guildford", Country = "UK", PostCode = "GU1 4QB" }
			};

			var serializer = new XmlSerializer(typeof(PostalAddress));
			using (var fileStream = new FileStream(@"..\..\..\XmlScannerApp\Data\PostalAddressValidSample1.xml", FileMode.Create))
			{
				serializer.Serialize(fileStream, postalAddress);
			}
		}

		//[Test]
		public void GenerateInValidSampleXmlDocumentDoesNotMatchXsd()
		{
			var postalAddress = new PostalAddress();
			postalAddress.PostalAddresses = new[]
			{
				new PostalAddressRecord { Address1 ="No. 10", Address2 = "Downing Street", 
					City = "London", Country = "UK"},
 				new PostalAddressRecord { Address1 = "Andrews House" , Address2 = "College Road", Address3 = "", 
					City = "Guildford", Country = "UK", PostCode = "GU1 4QB" }
			};

			var serializer = new XmlSerializer(typeof(PostalAddress));
			using (var fileStream = new FileStream(@"..\..\..\XmlScannerApp\Data\PostalAddressInvalidSample1.xml", FileMode.Create))
			{
				serializer.Serialize(fileStream, postalAddress);
			}
		}
	}
}
