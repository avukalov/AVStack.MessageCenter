using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AVStack.MessageCenter.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AVTemplateGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Json = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AVTemplateGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AVTemplate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Body = table.Column<string>(type: "TEXT", maxLength: 2147483647, nullable: false),
                    TemplateGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    Json = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AVTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AVTemplate_AVTemplateGroup_TemplateGroupId",
                        column: x => x.TemplateGroupId,
                        principalTable: "AVTemplateGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AVTemplate_TemplateGroupId",
                table: "AVTemplate",
                column: "TemplateGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AVTemplate");

            migrationBuilder.DropTable(
                name: "AVTemplateGroup");
        }
    }
}
