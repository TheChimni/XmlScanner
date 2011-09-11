using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace XmlScannerApp.Models.ViewModel
{
	public class IndexViewModel
	{
		public string SelectedSampleFile { get; set; }
		public IEnumerable<SelectListItem> SampleFiles { get; set; }
	}
}