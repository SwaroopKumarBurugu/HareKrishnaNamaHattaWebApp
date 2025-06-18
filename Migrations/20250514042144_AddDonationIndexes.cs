using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HareKrishnaNamaHattaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDonationIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Donations_Amount",
                table: "Donations",
                column: "Amount");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonationDate",
                table: "Donations",
                column: "DonationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Donations_Amount",
                table: "Donations");

            migrationBuilder.DropIndex(
                name: "IX_Donations_DonationDate",
                table: "Donations");
        }
    }
}
