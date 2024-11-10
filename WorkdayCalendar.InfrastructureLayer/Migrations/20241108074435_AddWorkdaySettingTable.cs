using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkdayCalendar.InfrastructureLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkdaySettingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkdaySettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WorkdayStart = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    WorkdayEnd = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkdaySettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkdaySettings");
        }
    }
}
