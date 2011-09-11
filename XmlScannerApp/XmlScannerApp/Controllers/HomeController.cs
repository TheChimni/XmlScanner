using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XmlScannerApp.Models;
using XmlScannerApp.Models.ViewModel;

namespace XmlScannerApp.Controllers
{
	public class HomeController : Controller
	{
		//
		// GET: /Home/

		public ActionResult Index()
		{
			var reader = new PostalAddressReader(Server.MapPath("Data"));
			var fileNames = reader.GetFileNames();
			var selectList = new SelectList(fileNames);
			var viewModel = new IndexViewModel() { SampleFiles = selectList };
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Index(IndexViewModel model)
		{
			var reader = new PostalAddressReader(Server.MapPath("Data"));
			var fileNames = reader.GetFileNames();
			var selectList = new SelectList(fileNames);
			var viewModel = new IndexViewModel() { SampleFiles = selectList };
			if (ModelState.IsValid)
			{
				var result = reader.Read(model.SelectedSampleFile);
				viewModel.SummaryMessage = result.SummaryMessage;
			}
			return View(viewModel);
		}
	}
}
