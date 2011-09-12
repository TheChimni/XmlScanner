using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlScannerApp.Models.ViewModel
{
	/// <summary>
	/// View Model for PostalAddressResult
	/// </summary>
	public class ResultViewModel
	{
		/// <summary>
		/// Returns the selected filename on the view
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Used to display the summary message on the view
		/// </summary>
		public string SummaryMessage { get; set; }

		/// <summary>
		/// Used to display a list of error messages
		/// </summary>
		public IEnumerable<string> Errors { get; set; }

		/// <summary>
		/// Used to display a list of warning messages
		/// </summary>
		public IEnumerable<string> Warnings { get; set; }
	}
}