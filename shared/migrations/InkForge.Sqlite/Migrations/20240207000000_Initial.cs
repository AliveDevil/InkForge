using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkForge.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blobs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value_Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value_Updated = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_Deleted = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Value_ContentId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Blobs_Value_ContentId",
                        column: x => x.Value_ContentId,
                        principalTable: "Blobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoteVersions",
                columns: table => new
                {
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value_Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value_Updated = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_Deleted = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Value_ContentId = table.Column<string>(type: "TEXT", nullable: false),
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteVersions", x => x.Version);
                    table.ForeignKey(
                        name: "FK_NoteVersions_Blobs_Value_ContentId",
                        column: x => x.Value_ContentId,
                        principalTable: "Blobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteVersions_Notes_Value_ParentId",
                        column: x => x.Value_ParentId,
                        principalTable: "Notes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_Value_ContentId",
                table: "Notes",
                column: "Value_ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteVersions_Id_Version",
                table: "NoteVersions",
                columns: new[] { "Id", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NoteVersions_Value_ContentId",
                table: "NoteVersions",
                column: "Value_ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteVersions_Value_ParentId",
                table: "NoteVersions",
                column: "Value_ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteVersions");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Blobs");
        }
    }
}
