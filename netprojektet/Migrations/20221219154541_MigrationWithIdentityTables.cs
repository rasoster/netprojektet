using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace netprojektet.Migrations
{
    /// <inheritdoc />
    public partial class MigrationWithIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "competence",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__competen__3214EC27203929FD", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "education",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__educatio__3214EC2721B925D8", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "experience",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__experien__3214EC27D02F9207", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Visitors = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PicUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Profile__3214EC27AD0E7C31", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    content = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    times = table.Column<DateTime>(type: "datetime", nullable: true),
                    seen = table.Column<bool>(type: "bit", nullable: true),
                    reciever = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__message__3214EC2753A3CFF9", x => x.ID);
                    table.ForeignKey(
                        name: "fk_message_reciever",
                        column: x => x.reciever,
                        principalTable: "Profile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "profile_has_competence",
                columns: table => new
                {
                    Profileid = table.Column<int>(type: "int", nullable: false),
                    Competenceid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__profile___809453B2A4295BDF", x => new { x.Profileid, x.Competenceid });
                    table.ForeignKey(
                        name: "fk_competence_id",
                        column: x => x.Competenceid,
                        principalTable: "competence",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_profilesss_id",
                        column: x => x.Profileid,
                        principalTable: "Profile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "profile_has_education",
                columns: table => new
                {
                    profileid = table.Column<int>(type: "int", nullable: false),
                    educationid = table.Column<int>(type: "int", nullable: false),
                    startdate = table.Column<DateTime>(type: "date", nullable: true),
                    enddate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__profile___60E42F3DC720B0F8", x => new { x.profileid, x.educationid });
                    table.ForeignKey(
                        name: "fk_education_id",
                        column: x => x.educationid,
                        principalTable: "education",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_profilessss_id",
                        column: x => x.profileid,
                        principalTable: "Profile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "profile_has_experience",
                columns: table => new
                {
                    profileid = table.Column<int>(type: "int", nullable: false),
                    experienceid = table.Column<int>(type: "int", nullable: false),
                    startdate = table.Column<DateTime>(type: "date", nullable: true),
                    enddate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__profile___09F2273E03F81676", x => new { x.profileid, x.experienceid });
                    table.ForeignKey(
                        name: "fk_experience_id",
                        column: x => x.experienceid,
                        principalTable: "experience",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "fk_profiless_id",
                        column: x => x.profileid,
                        principalTable: "Profile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "project",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__project__3214EC2729578260", x => x.ID);
                    table.ForeignKey(
                        name: "fk_creator_id",
                        column: x => x.CreatorID,
                        principalTable: "Profile",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    profileID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3214EC2775F0E23F", x => x.ID);
                    table.ForeignKey(
                        name: "fk_profile_id",
                        column: x => x.profileID,
                        principalTable: "Profile",
                        principalColumn: "ID");
                });

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
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_message_reciever",
                table: "message",
                column: "reciever");

            migrationBuilder.CreateIndex(
                name: "IX_profile_has_competence_Competenceid",
                table: "profile_has_competence",
                column: "Competenceid");

            migrationBuilder.CreateIndex(
                name: "IX_profile_has_education_educationid",
                table: "profile_has_education",
                column: "educationid");

            migrationBuilder.CreateIndex(
                name: "IX_profile_has_experience_experienceid",
                table: "profile_has_experience",
                column: "experienceid");

            migrationBuilder.CreateIndex(
                name: "IX_profile_in_project_Profileid",
                table: "profile_in_project",
                column: "Profileid");

            migrationBuilder.CreateIndex(
                name: "IX_project_CreatorID",
                table: "project",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_users_profileID",
                table: "users",
                column: "profileID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "profile_has_competence");

            migrationBuilder.DropTable(
                name: "profile_has_education");

            migrationBuilder.DropTable(
                name: "profile_has_experience");

            migrationBuilder.DropTable(
                name: "profile_in_project");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "competence");

            migrationBuilder.DropTable(
                name: "education");

            migrationBuilder.DropTable(
                name: "experience");

            migrationBuilder.DropTable(
                name: "project");

            migrationBuilder.DropTable(
                name: "Profile");
        }
    }
}
