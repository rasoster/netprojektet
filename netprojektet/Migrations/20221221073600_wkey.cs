using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netprojektet.Migrations
{
    /// <inheritdoc />
    public partial class wkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_profile_id",
                table: "users");
            migrationBuilder.DropForeignKey(
                name: "fk_profile_user",
                table: "Profile");

            migrationBuilder.DropIndex(
                name: "IX_users_profileID",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Profile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Profile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_users_profileID",
                table: "users",
                column: "profileID");

            migrationBuilder.AddForeignKey(
                name: "fk_profile_id",
                table: "users",
                column: "profileID",
                principalTable: "Profile",
                principalColumn: "ID");
        }
    }
}
