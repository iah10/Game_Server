using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game_Server.Infrastructure;
using Game_Server.Models;
using Game_Server.Services;
using WebGrease.Css.Extensions;
using System.IO;
using System.Web;

namespace Game_Server.Excel
{
	public class ExcelManipulator
	{
		private readonly GameServerEntities _context;
		private readonly VariableService _variableService;
		private readonly GameSessionService _sessionService;

		public const String FileName = "Game_Result.csv";

		public ExcelManipulator(GameServerEntities context)
		{
			_variableService = new VariableService(context);
			_sessionService = new GameSessionService(context);
		}


		private List<String> GetHeader()
		{

			IEnumerable<Variable> variables  = _variableService.GetAllVariables();
			List<String> header = new List<String> {"Id", "Gender", "Age", "Date"};
			variables.ForEach(v => header.Add(v.Name));
			header.Add("Choices");
			return header;
		}

		public static String GetFileMimeType()
		{
			var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + FileName);
			return MimeMapping.GetMimeMapping(filePath);
		}

		private List<List<String>> GetContent(DateTime? fromDate, DateTime? toDate)
		{
			IEnumerable<Variable> variables = _variableService.GetAllVariables();
			IEnumerable<GameSession> sessions;
			if (fromDate != null && toDate != null)
				sessions = _sessionService.GetSessionsByDate((DateTime) fromDate, (DateTime) toDate);
			else
				sessions = _sessionService.GetAllSessions();

			List<List<String>> content = new List<List<String>>();
			foreach (var session in sessions)
			{
				List<String> result = new List<String>();
				result.Add(session.PlayerId);
				result.Add(session.Gender);
				result.Add(session.Age+"");
				result.Add(session.Date.ToString());
				result.AddRange(variables.Select(sysVar => session.Variables.Any(sessionVariable => sysVar.Name.Equals(sessionVariable.Name)) ? "1" : "0"));
				result.Add(session.Choices.Aggregate("", (current, choice) => current + choice.VariableNumber+" "));
				content.Add(result);
			}
			return content;
		}

		public byte[] GetCsv(DateTime? fromDate, DateTime? toDate)
		{
			var filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + FileName);
			//var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Game_Result.csv");
			const string delimiter = ",";

			List<String> headar = GetHeader();
			var sb = new StringBuilder();
			foreach (var result in headar)
			{
				sb.Append(result + delimiter);
			}
			sb.Append(Environment.NewLine);
			List<List<String>> sessions = GetContent(fromDate, toDate);
			foreach (var session in sessions)
			{
				foreach (var result in session)
				{
					sb.Append(result + delimiter);	
				}
				sb.Append(Environment.NewLine);
			}
			sb.Append(Environment.NewLine);
			File.WriteAllText(filePath, sb.ToString());
			return File.ReadAllBytes(filePath);
		}
	}
}