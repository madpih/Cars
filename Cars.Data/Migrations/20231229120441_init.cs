using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cars.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitialReg = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Engine = table.Column<int>(type: "int", nullable: false),
                    Fuel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mileage = table.Column<int>(type: "int", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
