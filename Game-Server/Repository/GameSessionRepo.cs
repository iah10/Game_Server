using Game_Server.Infrastructure;
using Game_Server.Models;

namespace Game_Server.Repository
{
	public class GameSessionRepo : RepositoryBase<GameSession>, IGameSessionRepo
	{
		public GameSessionRepo(GameServerEntities dbContext) : base(dbContext)
		{
		}
	}
	public interface IGameSessionRepo: IRepository<GameSession> { }
}