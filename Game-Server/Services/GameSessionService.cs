using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Policy;
using Game_Server.Infrastructure;
using Game_Server.Models;
using Game_Server.Repository;

namespace Game_Server.Services
{
	public class GameSessionService
	{
		private readonly IGameSessionRepo _gameSessionRepo;
		private readonly VariableService _variableServieService;

		public GameSessionService(GameServerEntities dbContext)
		{
			_gameSessionRepo = new GameSessionRepo(dbContext);
			_variableServieService = new VariableService(dbContext);
		}

		public IEnumerable<GameSession> GetAllSessions()
		{
			return _gameSessionRepo.GetAll();
		}

		public IEnumerable<GameSession> GetSessionsByDate(DateTime from, DateTime to)
		{
			var sessions = _gameSessionRepo.GetMany(s => s.Date >= from && s.Date <= to);
			return sessions;
		}

		public void AddGameSession(GameSession session)
		{
			_gameSessionRepo.Add(session);
			SaveChanges();
		}

		private void SaveChanges()
		{
			_gameSessionRepo.Save();
		}

		public GameSession FixGameSession(GameSession old)
		{
			GameSession gameSession = new GameSession(old.PlayerId, old.Gender, old.Age);
			foreach (var variable in old.Variables)
			{
				Variable dbVar = _variableServieService.GetVariableByName(variable.Name);
				gameSession.Variables.Add(dbVar);
			}
			foreach (var choice in old.Choices)
			{
				Variable dbVar = _variableServieService.GetVariableByName(choice.Name);
				gameSession.Choices.Add(dbVar);
			}
			return gameSession;
		}
	}
}