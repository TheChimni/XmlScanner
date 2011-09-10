using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace XmlScannerApp.Models
{
	public class PostalAddressReader
	{
		public PostalAddressResult Read(string path)
		{
			using (var fileStream = new FileStream(path, FileMode.Open))
			{
				var result = new PostalAddressResult() { IsDocumentValid = true };
				return result;
			}
		}
	}
}