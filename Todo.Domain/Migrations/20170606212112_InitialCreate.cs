using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Todo.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    LastEdit = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoChecklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<DateTime>(nullable: false),
                    LastEdit = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoCheckListItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Checked = table.Column<bool>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    TodoChecklistId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoCheckListItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoCheckListItem_TodoChecklists_TodoChecklistId",
                        column: x => x.TodoChecklistId,
                        principalTable: "TodoChecklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoCheckListItem_TodoChecklistId",
                table: "TodoCheckListItem",
                column: "TodoChecklistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoCheckListItem");

            migrationBuilder.DropTable(
                name: "TodoNotes");

            migrationBuilder.DropTable(
                name: "TodoChecklists");
        }
    }
}
