using System.ComponentModel.DataAnnotations.Schema;

namespace Brasserie.Models
{
	// Grossiste
	public class Wholesaler
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		public required string Name { get; set; }


	}
}
