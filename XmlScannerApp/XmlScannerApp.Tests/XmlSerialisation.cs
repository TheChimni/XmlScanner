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
	public class XmlSerialisation
	{
		[Test]
		public void TemporaryTestToGenerateSampleXmlDocument()
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
			using (var fileStream = new FileStream(@"..\..\..\XmlScannerApp\Data\PostalAddressSample1.xml", FileMode.Create))
			{
				serializer.Serialize(fileStream, postalAddress);
			}
		}
	}
}
