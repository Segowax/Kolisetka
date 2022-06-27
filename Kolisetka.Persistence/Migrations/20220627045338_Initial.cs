using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolisetka.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "DateCreated", "DateUpdated", "Description", "Name", "Price" },
                values: new object[] { 1, 2, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Piwo 500 ml, czeskie z nalewaka.", "Holba", 6.00m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "DateCreated", "DateUpdated", "Description", "Name", "Price" },
                values: new object[] { 2, 0, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Najsmaczniejsza golonka na całym Kozanownie!", "Golonka", 15.00m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "DateCreated", "DateUpdated", "Description", "Name", "Price" },
                values: new object[] { 3, 1, new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Woda 200 ml, w szklanej butelce.", "Kinga", 2.50m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
