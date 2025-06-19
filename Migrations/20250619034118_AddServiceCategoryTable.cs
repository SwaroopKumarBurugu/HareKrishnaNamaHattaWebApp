using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HareKrishnaNamaHattaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteerServiceId",
                table: "VolunteerDevotees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerServices_ServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "ServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerDevotees_VolunteerServiceId",
                table: "VolunteerDevotees",
                column: "VolunteerServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerServices_ServiceCategoryId",
                table: "VolunteerServices",
                column: "ServiceCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerDevotees_VolunteerServices_VolunteerServiceId",
                table: "VolunteerDevotees",
                column: "VolunteerServiceId",
                principalTable: "VolunteerServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerDevotees_VolunteerServices_VolunteerServiceId",
                table: "VolunteerDevotees");

            migrationBuilder.DropTable(
                name: "VolunteerServices");

            migrationBuilder.DropTable(
                name: "ServiceCategories");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerDevotees_VolunteerServiceId",
                table: "VolunteerDevotees");

            migrationBuilder.DropColumn(
                name: "VolunteerServiceId",
                table: "VolunteerDevotees");
        }
    }
}
