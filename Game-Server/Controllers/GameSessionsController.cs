using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Game_Server.Infrastructure;
using Game_Server.Models;
using Game_Server.Services;

namespace Game_Server.Controllers
{
    public class GameSessionsController : ApiController
    {
		private static readonly GameServerEntities Db = new GameServerEntities();
		private readonly GameSessionService _gameSessionService = new GameSessionService(Db);

        // GET: api/GameSessions
        public IEnumerable<GameSession> GetSessions()
        {
            return _gameSessionService.GetAllSessions();
        }

  
        // POST: api/GameSessions
        [ResponseType(typeof(GameSession))]
        public IHttpActionResult PostGameSession(GameSession gameSession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
	        GameSession modifiedSession = _gameSessionService.FixGameSession(gameSession);
			_gameSessionService.AddGameSession(modifiedSession);

			return CreatedAtRoute("DefaultApi", new { id = gameSession.Id }, gameSession);
        }
    }
}