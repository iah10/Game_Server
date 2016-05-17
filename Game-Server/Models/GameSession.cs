using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Game_Server.Services;

namespace Game_Server.Models
{
	public partial class GameSession
	{
		[Key]
		public int Id { get; set; }

		public String PlayerId { get; set; }

		[Required]
		public String Gender { get; set; }
		[Required]
		public int Age { get; set; }

		//[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime Date { get; set; }

		public virtual ICollection<Variable> Choices { get; set; } 

		public virtual ICollection<Variable> Variables { get; set; }

		public GameSession()
		{
			Date = DateTime.Now;
			Choices = new HashSet<Variable>();
			Variables = new HashSet<Variable>();
		}

		public GameSession(String playerId, String gender, int age)
		{
			PlayerId = playerId;
			Gender = gender;
			Age = age;
			Date = DateTime.Now;
			Choices = new HashSet<Variable>();
			Variables = new HashSet<Variable>();
		}
	}
}