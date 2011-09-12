using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlScannerApp.Models.ViewModel
{
	public class ResultViewModel
	{
		public string SummaryMessage { get; set; }
		public IEnumerable<string> Errors { get; set; }
		public IEnumerable<string> Warnings { get; set; }
	}
}