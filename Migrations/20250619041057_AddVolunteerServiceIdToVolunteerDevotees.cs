using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HareKrishnaNamaHattaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddVolunteerServiceIdToVolunteerDevotees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerDevotees_VolunteerServices_VolunteerServiceId",
                table: "VolunteerDevotees");

            migrationBuilder.AlterColumn<int>(
                name: "VolunteerServiceId",
                table: "VolunteerDevotees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerDevotees_VolunteerServices_VolunteerServiceId",
                table: "VolunteerDevotees",
                column: "VolunteerServiceId",
                principalTable: "VolunteerServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerDevotees_VolunteerServices_VolunteerServiceId",
                table: "VolunteerDevotees");

            migrationBuilder.AlterColumn<int>(
                name: "VolunteerServiceId",
                table: "VolunteerDevotees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerDevotees_VolunteerServices_VolunteerServiceId",
                table: "VolunteerDevotees",
                column: "VolunteerServiceId",
                principalTable: "VolunteerServices",
                principalColumn: "Id");
        }
    }
}
