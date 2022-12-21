using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netprojektet.Migrations
{
    /// <inheritdoc />
    public partial class withpKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileUserName",
                table: "users",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Profile",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Profile_UserName",
                table: "Profile",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_users_ProfileUserName",
                table: "users",
                column: "ProfileUserName",
                unique: true,
                filter: "[ProfileUserName] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Profile_ProfileUserName",
                table: "users",
                column: "ProfileUserName",
                principalTable: "Profile",
                principalColumn: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Profile_ProfileUserName",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_ProfileUserName",
                table: "users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Profile_UserName",
                table: "Profile");

            migrationBuilder.DropColumn(
                name: "ProfileUserName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Profile");
        }
    }
}
