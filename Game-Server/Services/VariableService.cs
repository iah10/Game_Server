using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Game_Server.Infrastructure;
using Game_Server.Models;
using Game_Server.Repository;

namespace Game_Server.Services
{
	public class VariableService
	{
		private readonly IVariableRepo _variableRepo;

		public VariableService(GameServerEntities dbContext)
		{
			_variableRepo = new VariableRepo(dbContext);
		}

		public IEnumerable<Variable> GetAllVariables()
		{
			return _variableRepo.GetAll();
		}

		public Variable GetVariableByName(String name)
		{
			return _variableRepo.Get(v => v.Name.Equals(name));
		}

		private void SaveChanges()
		{
			_variableRepo.Save();
		}
	}
}