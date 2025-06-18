using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HareKrishnaNamaHattaWebApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminReplyToContactMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminReply",
                table: "ContactMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminReply",
                table: "ContactMessages");
        }
    }
}
