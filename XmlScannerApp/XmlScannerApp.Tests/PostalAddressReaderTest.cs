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
	[TestFixture]
	public class PostalAddressReaderTest
	{
		[Test]
		public void ReadValidPostalAddress()
		{
			var reader = new PostalAddressReader();
			var result = reader.Read(@"..\..\..\XmlScannerApp\Data\PostalAddressValidSample1.xml");
			Assert.IsNotNull(result);
			Assert.IsTrue(result.IsDocumentValid);
		}

		[Test]
		public void ReadInvalidPostalAddress()
		{
			var reader = new PostalAddressReader();
			var result = reader.Read(@"..\..\..\XmlScannerApp\Data\PostalAddressInvalidSample1.xml");
			Assert.IsNotNull(result);
			Assert.IsFalse(result.IsDocumentValid);
		}

		[Test]
		public void ReadPostalAddressWithEmptyAddress1()
		{
			var reader = new PostalAddressReader();
			var result = reader.Read(@"..\..\..\XmlScannerApp\Data\PostalAddressWithAddress1Empty.xml");
			Assert.AreEqual(1, result.Errors.Count());
			Assert.AreEqual("<address1/>", result.Errors.First().Tag);
		}

		[Test]
		public void ReadPostalAddressWithEmptyPostcode()
		{
			var reader = new PostalAddressReader();
			var result = reader.Read(@"..\..\..\XmlScannerApp\Data\PostalAddressWithPostcodeEmpty.xml");
			Assert.AreEqual(1, result.Errors.Count());
			Assert.AreEqual("<postcode/>", result.Errors.First().Tag);
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
