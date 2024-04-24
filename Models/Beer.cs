using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Brasserie.Models
{
	// Biere
	public class Beer
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public required string Name {  get; set; }
		public required double Price { get; set; }
		public required double AlcoholLevel { get; set; }

		public long BrewerId { get; set; }
		public virtual Brewer? Brewer { get; set; }

	}
}
