using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brasserie.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceToQuoteDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteDetails_Quotes_QuoteId",
                table: "QuoteDetails");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Sales",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Quotes",
                type: "double",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<long>(
                name: "QuoteId",
                table: "QuoteDetails",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "QuoteDetails",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteDetails_Quotes_QuoteId",
                table: "QuoteDetails",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuoteDetails_Quotes_QuoteId",
                table: "QuoteDetails");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "QuoteDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Sales",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Quotes",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.AlterColumn<long>(
                name: "QuoteId",
                table: "QuoteDetails",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_QuoteDetails_Quotes_QuoteId",
                table: "QuoteDetails",
                column: "QuoteId",
                principalTable: "Quotes",
                principalColumn: "Id");
        }
    }
}
