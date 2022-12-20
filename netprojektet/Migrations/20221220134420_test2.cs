using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netprojektet.Migrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "profile_in_project");

            migrationBuilder.CreateTable(
                name: "profile_in_project",
                columns: table => new
                {
                    Projectid = table.Column<int>(type: "int", nullable: false),
                    Profileid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__profile___16385FD84E45517A", x => new { x.Projectid, x.Profileid });
                    table.ForeignKey(
                        name: "fk_profiles_id",
                        column: x => x.Profileid,
                        principalTable: "Profile",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_project_id",
                        column: x => x.Projectid,
                        principalTable: "project",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_profile_in_Project_ProjectID",
                table: "profile_in_Project",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_profile_in_project_Profileid",
                table: "profile_in_project",
                column: "Profileid");

            migrationBuilder.AddForeignKey(
                name: "fk_profilesssss_id",
                table: "profile_in_Project",
                column: "profileid",
                principalTable: "Profile",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_profilessss_id",
                table: "profile_has_education");

            migrationBuilder.DropTable(
                name: "profile_in_project");

            migrationBuilder.DropPrimaryKey(
                name: "PK__profile_ProjectID",
                table: "profile_in_Project");

            migrationBuilder.DropIndex(
                name: "IX_profile_in_Project_ProjectID",
                table: "profile_in_Project");

            migrationBuilder.RenameTable(
                name: "profile_in_Project",
                newName: "profile_in_project");

            migrationBuilder.RenameColumn(
                name: "ProjectID",
                table: "profile_in_project",
                newName: "Projectid");

            migrationBuilder.RenameColumn(
                name: "profileid",
                table: "profile_in_project",
                newName: "Profileid");

            migrationBuilder.AddPrimaryKey(
                name: "PK__profile___16385FD84E45517A",
                table: "profile_in_project",
                columns: new[] { "Projectid", "Profileid" });

            migrationBuilder.CreateIndex(
                name: "IX_profile_in_project_Profileid",
                table: "profile_in_project",
                column: "Profileid");

            migrationBuilder.AddForeignKey(
                name: "fk_profiles_id",
                table: "profile_in_project",
                column: "Profileid",
                principalTable: "Profile",
                principalColumn: "ID");
        }
    }
}
