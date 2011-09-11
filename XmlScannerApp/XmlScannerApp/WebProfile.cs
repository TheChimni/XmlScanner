using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using XmlScannerApp.Models;
using XmlScannerApp.Models.ViewModel;

namespace XmlScannerApp
{
	public class WebProfile : Profile
	{
		protected override void Configure()
		{
			Mapper.CreateMap<PostalAddressReader, IndexViewModel>();
		}
	}
}