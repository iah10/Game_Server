using System;
using System.Net.Mime;
using System.Web.Mvc;
using Game_Server.Excel;
using Game_Server.Infrastructure;
using Game_Server.Services;

namespace Game_Server.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private static readonly GameServerEntities Db = new GameServerEntities();
		private readonly GameSessionService _gameSessionService = new GameSessionService(Db);

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult ProjectReport(DateTime? fromDate, DateTime? toDate)
		{
			if (!fromDate.HasValue) fromDate = DateTime.Now.Date;
			if (!toDate.HasValue) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
			if (toDate < fromDate) toDate = fromDate.GetValueOrDefault(DateTime.Now.Date).Date.AddDays(1);
			ViewBag.fromDate = fromDate;
			ViewBag.toDate = toDate;

			ExcelManipulator excel = new ExcelManipulator(Db);
			var document = excel.GetCsv(fromDate, toDate.GetValueOrDefault().Date.AddDays(1));
			var cd = new ContentDisposition
			{
				// for example foo.bak
				FileName = ExcelManipulator.FileName,

				// always prompt the user for downloading, set to true if you want 
				// the browser to try to show the file inline
				Inline = false
			};
			Response.AppendHeader("Content-Disposition", cd.ToString());
			return File(document, ExcelManipulator.GetFileMimeType());
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}