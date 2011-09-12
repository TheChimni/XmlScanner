using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlScannerApp.Models
{
	/// <summary>
	/// Class to capture scan results of an XML document
	/// </summary>
	public class PostalAddressResult
	{
		IList<Error> errorList = new List<Error>();
		IList<Warning> warningList = new List<Warning>();

		/// <summary>
		/// Gives the summary of scan 
		/// </summary>
		public string SummaryMessage { get; set; }

		/// <summary>
		/// Property to check if the XML document has a valid schema 
		/// </summary>
		public bool IsDocumentValid { get; set; }

		/// <summary>
		///Property to check if a given XML document contains records in error that
		///exceed a predefined threshold.
		/// </summary>
		public bool ExceedsErrorThreshold { get; set; }

		/// <summary>
		/// Retruns an IEnumerable of Errors
		/// </summary>
		public IEnumerable<Error> Errors 
		{ 
			get { return errorList; } 
		}

		/// <summary>
		/// Returns an IEnumerable of Warnings
		/// </summary>
		public IEnumerable<Warning> Warnings
		{
			get { return warningList; }
		}

		/// <summary>
		/// Adds a given error to the list of errors
		/// </summary>
		/// <param name="error"></param>
		public void AddError(Error error)
		{
			errorList.Add(error);
		}

		/// <summary>
		/// Adds a given warning to the list of warnings
		/// </summary>
		/// <param name="warning"></param>
		public void AddWarning(Warning warning)
		{
			warningList.Add(warning);
		}
	}
}