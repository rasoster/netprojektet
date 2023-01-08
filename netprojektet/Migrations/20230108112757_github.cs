using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netprojektet.Migrations
{
    /// <inheritdoc />
    public partial class github : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<string>(
                name: "GitHubUrl",
                table: "Profile",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GitHubUrl",
                table: "Profile");


        }
    }
}
