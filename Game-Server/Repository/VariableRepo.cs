using Game_Server.Infrastructure;
using Game_Server.Models;

namespace Game_Server.Repository
{
	public class VariableRepo : RepositoryBase<Variable>, IVariableRepo
	{
		public VariableRepo(GameServerEntities dbContext) : base(dbContext)
		{
		}
	}
	public interface IVariableRepo : IRepository<Variable> { }
}