using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XmlScannerApp.Models
{
	public static class ExtensionMethods
	{
		public static bool IsNullOrEmpty<T>(this T [] inputCollection)
		{
			return inputCollection == null || inputCollection.Count() == 0;
		}
	}
}