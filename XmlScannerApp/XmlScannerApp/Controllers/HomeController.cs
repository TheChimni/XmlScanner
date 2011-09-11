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

		public ActionResult Index(string id)
		{
			var reader = new PostalAddressReader(Server.MapPath(@"~/Data"));
			var fileNames = reader.GetFileNames();
			var selectList = new SelectList(fileNames);
			var viewModel = new IndexViewModel() { SampleFiles = selectList };
			if (!string.IsNullOrEmpty(id))
			{
				var result = reader.Read(id);
				viewModel.SelectedSampleFile = id;
				viewModel.SummaryMessage = result.SummaryMessage;
			}
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Index(IndexViewModel model)
		{
			if (ModelState.IsValid)
			{
				return RedirectToAction("Index", "Home", new { id = model.SelectedSampleFile });
			}
			else
			{
				var reader = new PostalAddressReader(Server.MapPath(@"~/Data"));
				var fileNames = reader.GetFileNames();
				var selectList = new SelectList(fileNames);
				var viewModel = new IndexViewModel() { SampleFiles = selectList, SelectedSampleFile = model.SelectedSampleFile };
				return View(viewModel);
			}
		}
	}
}
