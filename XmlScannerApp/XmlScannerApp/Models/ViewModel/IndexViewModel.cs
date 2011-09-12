using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;

namespace XmlScannerApp.Models.ViewModel
{
	/// <summary>
	/// View model for Index page
	/// </summary>
	public class IndexViewModel
	{
		public string SelectedSampleFile { get; set; }
		/// <summary>
		/// Used to render sample files in a dropdownbox
		/// </summary>
		public IEnumerable<SelectListItem> SampleFiles { get; set; }

		/// <summary>
		/// Used to render the Results partial
		/// </summary>
		public ResultViewModel Result { get; set; }
	}
}