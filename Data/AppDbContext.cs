using Brasserie.Models;
using Microsoft.EntityFrameworkCore;


namespace Brasserie.Data
{
	public class AppDbContext : DbContext
	{
		//entities
		public DbSet<Beer> Beers { get; set; }
		public DbSet<Brewer> Brewers { get; set; }
		public DbSet<Wholesaler> Wholesalers { get; set; }
		public DbSet<Stock> Stocks { get; set; }
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Quote> Quotes { get; set; }
		public DbSet<QuoteDetail> QuoteDetails { get; set; }


		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Stock>()
				.HasOne(s => s.Wholesaler)
				.WithMany()
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Brewer>().HasData(
				new Brewer { Id = 1, Name = "Premier Brasseur", Beers = [] },
				new Brewer { Id = 2, Name = "Second Brasseur", Beers = []  });

			// modelBuilder.Entity<Brewer>().HasData(
			// 	new Brewer { Id = 2, Name = "Second Brasseur", Beers = []  });

			modelBuilder.Entity<Beer>().HasData(
				new Beer { Id = 1, Name = "Leffe Blonde", AlcoholLevel = 12, Price = 4, BrewerId = 1 },
				new Beer { Id = 2, Name = "Jupiler Kriek", AlcoholLevel = 4, Price = 2, BrewerId = 1 });

			// modelBuilder.Entity<Beer>().HasData(
			// 	new Beer { Id = 2, Name = "Jupiler Kriek", AlcoholLevel = 4, Price = 2, BrewerId = 1 });

			modelBuilder.Entity<Wholesaler>().HasData(
				new Wholesaler { Id = 1, Name = "Premier Grossiste" },
				new Wholesaler { Id = 2, Name = "Second Grossiste" });

			// modelBuilder.Entity<Wholesaler>().HasData(
			// 	new Wholesaler { Id = 2, Name = "Second Grossiste" });

			modelBuilder.Entity<Stock>().HasData(
				new Stock { Id = 1, BeerId = 1, WholesalerId = 1, QuantityInStock = 10 },
				new Stock { Id = 2, BeerId = 2, WholesalerId = 1, QuantityInStock = 20 });

		}
	}
}
