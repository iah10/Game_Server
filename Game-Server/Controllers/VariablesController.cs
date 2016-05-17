using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Game_Server.Excel;
using Game_Server.Infrastructure;
using Game_Server.Models;
using Game_Server.Services;

namespace Game_Server.Controllers
{
    public class VariablesController : ApiController
    {
		private static readonly GameServerEntities Db = new GameServerEntities();
		private readonly VariableService _variableService = new VariableService(Db);

        // GET: api/Variables
		public IEnumerable<Variable> Get()
        {
            return _variableService.GetAllVariables();
        }
    }
}
