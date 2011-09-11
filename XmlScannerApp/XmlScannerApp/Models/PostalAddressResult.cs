using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlScannerApp.Models
{
	public class PostalAddressResult
	{
		IList<Error> errorList = new List<Error>(); 

		public bool IsDocumentValid { get; set; }
		public IEnumerable<Error> Errors 
		{ 
			get 
			{
				return errorList; 
			} 
		}

		public void AddError(Error error)
		{
			errorList.Add(error);
		}
	}
}