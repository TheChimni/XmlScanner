using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlScannerApp.Models
{
	public class PostalAddressResult
	{
		public bool IsDocumentValid { get; set; }
		public IEnumerable<Error> Errors { get; set; }
	}
}