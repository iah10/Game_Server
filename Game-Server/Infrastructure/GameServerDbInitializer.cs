using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Game_Server.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace Game_Server.Infrastructure
{
	public class GameServerDbInitializer : DropCreateDatabaseAlways<GameServerEntities>
	{
		private GameServerEntities _context;

		protected override void Seed(GameServerEntities context)
		{
			_context = context;
			CreateGameVariables();
			CreateRolesandUsers();
			context.Commit();
		}
		private void CreateRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


			// In Startup i am creating first Admin Role and creating a default Admin User   
			if (!roleManager.RoleExists("Admin"))
			{

				// first we create Admin rool  
				var role = new IdentityRole { Name = "Admin" };
				roleManager.Create(role);

				//Here we create a Admin super user who will maintain the website                 

				var user = new ApplicationUser { UserName = "issahaddar5@gmail.com", Email = "issahaddar5@gmail.com" };

				string userPwd = "Milk_cow5";

				var chkUser = userManager.Create(user, userPwd);

				//Add default User to Role Admin  
				if (chkUser.Succeeded)
				{
					userManager.AddToRole(user.Id, "Admin");

				}
			}
		}
		private void CreateGameVariables()
		{
			List<Variable> variables = new List<Variable>
			{
				new Variable("Breakfast_LabnehOlives_Fridge", 1),
				new Variable("Breakfast_Sandwich_Table", 2),
				new Variable("Breakfast_SandwichLarge_Table", 3),
				new Variable("Breakfast_SandwichSmall_Table", 4),
				new Variable("Breakfast_SandwichMedium_Table", 5),
				new Variable("Breakfast_Croissant", 6),
				new Variable("Breakfast_Mother", 7),
				new Variable("MorningStreet_ManousheInsideCover_Shop", 8),
				new Variable("MorningStreet_ManousheOnTable_Shop", 9),
				new Variable("MorningStreet_Chips_Shop", 10),
				new Variable("MorningStreet_Custard_Shop", 11),
				new Variable("MorningStreet_Banana_Shop", 12),
				new Variable("MorningStreet_Manoushe_Shop", 13),
				new Variable("MorningStreet_FriendWithAManoushe_Shop", 14),
				new Variable("MorningStreet_Manoushe500_Shop", 15),
				new Variable("MorningStreet_Manoushe250_Shop", 16),
				new Variable("MorningStreet_Manoushe100_Shop", 17),
				new Variable("MorningStreet_ManousheSmall_Shop", 18),
				new Variable("MorningStreet_ManousheMedium_Shop", 19),
				new Variable("MorningStreet_ManousheLarge_Shop", 20),
				new Variable("RecessSchool_ManousheInsideCover_Shop", 21),
				new Variable("RecessSchool_ManousheOnTable_Shop", 22),
				new Variable("RecessSchool_Manoushe_Shop", 23),
				new Variable("RecessSchool_LentilSoup_Shop", 24),
				new Variable("RecessSchool_Chips_Shop", 25),
				new Variable("RecessSchool_Candy_Shop", 26),
				new Variable("RecessSchool_FriendWithManoushe_Shop", 27),
				new Variable("RecessSchool_Manoushe500_Shop", 28),
				new Variable("RecessSchool_Manoushe250_Shop", 29),
				new Variable("RecessSchool_Manoushe100_Shop", 30),
				new Variable("RecessSchool_ManousheSmall_Shop", 31),
				new Variable("RecessSchool_ManousheMedium_Shop", 32),
				new Variable("RecessSchool_ManousheLarge_Shop", 33),
				new Variable("LunchHome_SimpleSalad_Table", 34),
				new Variable("LunchHome_ClassicSalad_Fridge", 35),
				new Variable("LunchHome_Stew_Table", 36),
				new Variable("LunchHome_Pizza_Table", 37),
				new Variable("LunchHome_Mother_Kitchen", 38),
				new Variable("LunchHome_SmallPizza_Table", 39),
				new Variable("LunchHome_MediumPizza_Table", 40),
				new Variable("LunchHome_LargePizza_Table", 41),
				new Variable("EveningDinner_CheeseSandwich_Table", 42),
				new Variable("EveningDinner_CheeseSandwichIngredients_Fridge", 43),
				new Variable("EveningDinner_Croissant_Table", 44),
				new Variable("EveningDinner_Soup_Table", 45),
				new Variable("EveningDinner_Mother_Kitchen", 46),
				new Variable("EveningDinner_OneEgg_Table", 47),
				new Variable("EveningDinner_TwoEggs_Table", 48),
				new Variable("EveningDinner_ThreeEggs_Table", 49)
			};
			variables.ForEach(o => _context.Variables.Add(o));

			GameSession issa = new GameSession("Issa", "Boy", 21);
			//GameSession ephrem = new GameSession("Ephrem", "Boy", 22);
			//GameSession yehya = new GameSession("Yehya", "Girl", 19);

			issa.Variables.Add(variables.ElementAt(1));
			issa.Variables.Add(variables.ElementAt(3));
			issa.Variables.Add(variables.ElementAt(6));
			issa.Variables.Add(variables.ElementAt(7));
			issa.Variables.Add(variables.ElementAt(11));
			issa.Variables.Add(variables.ElementAt(20));
			issa.Variables.Add(variables.ElementAt(23));
			issa.Variables.Add(variables.ElementAt(20));
			issa.Variables.Add(variables.ElementAt(30));
			issa.Variables.Add(variables.ElementAt(33));
			issa.Variables.Add(variables.ElementAt(35));
			issa.Variables.Add(variables.ElementAt(38));

			issa.Choices.Add(variables.ElementAt(3));
			issa.Choices.Add(variables.ElementAt(6));
			issa.Choices.Add(variables.ElementAt(11));
			issa.Choices.Add(variables.ElementAt(30));

			_context.Sessions.Add(issa);
		}
	}
}