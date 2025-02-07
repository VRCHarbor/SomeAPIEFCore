using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SomeAPIEFCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class RoadmapNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DateOfLoad = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roadMapCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roadMapCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roadMaps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roadMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roadMaps_roadMapCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "roadMapCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roadMapElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: false),
                    EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    RoadMapId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roadMapElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roadMapElements_roadMaps_RoadMapId",
                        column: x => x.RoadMapId,
                        principalTable: "roadMaps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_roadMapElements_RoadMapId",
                table: "roadMapElements",
                column: "RoadMapId");

            migrationBuilder.CreateIndex(
                name: "IX_roadMaps_CategoryId",
                table: "roadMaps",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");

            migrationBuilder.DropTable(
                name: "roadMapElements");

            migrationBuilder.DropTable(
                name: "roadMaps");

            migrationBuilder.DropTable(
                name: "roadMapCategories");
        }
    }
}
