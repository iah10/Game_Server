using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Game_Server.Models;
using Microsoft.Ajax.Utilities;

namespace Game_Server.Infrastructure
{
	public class GameServerEntities : DbContext
	{

		/// <summary>
		/// The Constructor
		/// </summary>
		public GameServerEntities()
			: base("GameServerEntities")
		{

		}


		public DbSet<GameSession> Sessions { get; set; }
		public DbSet<Variable> Variables { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{

			modelBuilder.Entity<GameSession>()
						.HasMany<Variable>(g => g.Variables)
						.WithMany(v => v.SessionsVaraiables)
						.Map(cs =>
						{
							cs.MapLeftKey("GameSessionRefId");
							cs.MapRightKey("VariableRefId");
							cs.ToTable("GameSessionVariables");
						});

			modelBuilder.Entity<GameSession>()
						.HasMany<Variable>(g => g.Choices)
						.WithMany(v => v.SessionsChoices)
						.Map(cs =>
						{
							cs.MapLeftKey("GameSessionRefId");
							cs.MapRightKey("ChoiceRefId");
							cs.ToTable("GameSessionChoices");
						});

		}

		/// <summary>
		/// Save the changes to the Database
		/// </summary>
		public virtual void Commit()
		{
			SaveChanges();
		}
	}
}