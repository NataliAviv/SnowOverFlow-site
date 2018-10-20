using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SnowOverFlow.Data.Migrations
{
    public partial class sites4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Continent",
                table: "Country",
                newName: "continent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "continent",
                table: "Country",
                newName: "Continent");
        }
    }
}
