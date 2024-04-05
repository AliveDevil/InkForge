using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InkForge.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class _03_DropHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetadataHistory");

            migrationBuilder.DropTable(
                name: "NoteVersions");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Blobs",
                newName: "Value");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Notes",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_ParentId",
                table: "Notes",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Notes_ParentId",
                table: "Notes",
                column: "ParentId",
                principalTable: "Notes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Notes_ParentId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ParentId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Notes");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Blobs",
                newName: "Content");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "MetadataHistory",
                columns: table => new
                {
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataHistory", x => x.Version);
                });

            migrationBuilder.CreateTable(
                name: "NoteVersions",
                columns: table => new
                {
                    Version = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Value_ContentId = table.Column<string>(type: "TEXT", nullable: false),
                    Value_ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value_Created = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Value_Deleted = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Value_Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value_Updated = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
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
                name: "IX_MetadataHistory_Id_Version",
                table: "MetadataHistory",
                columns: new[] { "Id", "Version" },
                unique: true);

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
    }
}
