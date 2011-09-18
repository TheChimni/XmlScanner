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
	public class WebAddressReader
	{
		private const string SchemaFileName = "WebAddress.xsd";
		private const string XmlFolder = "WebAddresses";
		private string DataFolder { get; set; }

		public WebAddressReader(string dataFolder)
		{
			DataFolder = dataFolder;
		}

	}
}