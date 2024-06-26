﻿// <auto-generated />
using Brasserie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Brasserie.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Brasserie.Models.Beer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("AlcoholLevel")
                        .HasColumnType("double");

                    b.Property<long>("BrewerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("BrewerId");

                    b.ToTable("Beers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            AlcoholLevel = 12.0,
                            BrewerId = 1L,
                            Name = "Leffe Blonde",
                            Price = 4.0
                        },
                        new
                        {
                            Id = 2L,
                            AlcoholLevel = 4.0,
                            BrewerId = 1L,
                            Name = "Jupiler Kriek",
                            Price = 2.0
                        },
                        new
                        {
                            Id = 3L,
                            AlcoholLevel = 3.0,
                            BrewerId = 2L,
                            Name = "Maes 25",
                            Price = 3.0
                        });
                });

            modelBuilder.Entity("Brasserie.Models.Brewer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Brewers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Premier Brasseur"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Second Brasseur"
                        });
                });

            modelBuilder.Entity("Brasserie.Models.Quote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("TotalPrice")
                        .HasColumnType("double");

                    b.Property<long>("WholesalerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("Brasserie.Models.QuoteDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BeerId")
                        .HasColumnType("bigint");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("QuoteId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("QuoteId");

                    b.ToTable("QuoteDetails");
                });

            modelBuilder.Entity("Brasserie.Models.Sale", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BeerId")
                        .HasColumnType("bigint");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<long>("WholesalerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Brasserie.Models.Stock", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("BeerId")
                        .HasColumnType("bigint");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("double");

                    b.Property<long>("WholesalerId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("BeerId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Stocks");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            BeerId = 1L,
                            QuantityInStock = 10,
                            UnitPrice = 2.5,
                            WholesalerId = 1L
                        },
                        new
                        {
                            Id = 2L,
                            BeerId = 2L,
                            QuantityInStock = 20,
                            UnitPrice = 3.0,
                            WholesalerId = 1L
                        },
                        new
                        {
                            Id = 3L,
                            BeerId = 3L,
                            QuantityInStock = 30,
                            UnitPrice = 3.5,
                            WholesalerId = 2L
                        });
                });

            modelBuilder.Entity("Brasserie.Models.Wholesaler", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Wholesalers");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Premier Grossiste"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Second Grossiste"
                        });
                });

            modelBuilder.Entity("Brasserie.Models.Beer", b =>
                {
                    b.HasOne("Brasserie.Models.Brewer", "Brewer")
                        .WithMany("Beers")
                        .HasForeignKey("BrewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewer");
                });

            modelBuilder.Entity("Brasserie.Models.Quote", b =>
                {
                    b.HasOne("Brasserie.Models.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("Brasserie.Models.QuoteDetail", b =>
                {
                    b.HasOne("Brasserie.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brasserie.Models.Quote", "Quote")
                        .WithMany("Details")
                        .HasForeignKey("QuoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Quote");
                });

            modelBuilder.Entity("Brasserie.Models.Sale", b =>
                {
                    b.HasOne("Brasserie.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brasserie.Models.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("Brasserie.Models.Stock", b =>
                {
                    b.HasOne("Brasserie.Models.Beer", "Beer")
                        .WithMany()
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Brasserie.Models.Wholesaler", "Wholesaler")
                        .WithMany()
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("Brasserie.Models.Brewer", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("Brasserie.Models.Quote", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
