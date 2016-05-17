using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Game_Server.Models
{
	public partial class Variable
	{
		[Key]
		[Required]
		public String Name { get; set; }

		public String Description { get; set; }

		[Required]
		public int VariableNumber { get; set; }
		
		public virtual ICollection<GameSession> SessionsVaraiables { get; set; }

		public virtual ICollection<GameSession> SessionsChoices { get; set; }

		public Variable()
		{
		}

		public Variable(String name, int variableNum)
		{
			Name = name;
			VariableNumber = variableNum;
		}
	}
}