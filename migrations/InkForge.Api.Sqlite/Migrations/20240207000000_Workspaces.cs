using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkForge.Api.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Workspaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workspaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value_Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_OwnerId = table.Column<string>(type: "TEXT", nullable: false),
                    Value_Updated = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_Deleted = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workspaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workspaces_AspNetUsers_Value_OwnerId",
                        column: x => x.Value_OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkspaceVersions",
                columns: table => new
                {
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value_Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_OwnerId = table.Column<string>(type: "TEXT", nullable: false),
                    Value_Updated = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_Deleted = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkspaceVersions", x => x.Version);
                    table.ForeignKey(
                        name: "FK_WorkspaceVersions_AspNetUsers_Value_OwnerId",
                        column: x => x.Value_OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_Value_OwnerId",
                table: "Workspaces",
                column: "Value_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceVersions_Id_Version",
                table: "WorkspaceVersions",
                columns: new[] { "Id", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkspaceVersions_Value_OwnerId",
                table: "WorkspaceVersions",
                column: "Value_OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Workspaces");

            migrationBuilder.DropTable(
                name: "WorkspaceVersions");
        }
    }
}
