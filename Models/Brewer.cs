using System.ComponentModel.DataAnnotations.Schema;

namespace Brasserie.Models
{
	// Brasserie
	public class Brewer
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public required string Name { get; set; }

		public virtual ICollection<Beer> Beers { get; set; } = [];
	}
}
