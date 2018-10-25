using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SnowOverFlow.Data.Migrations
{
    public partial class Ziv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "continent",
                table: "Country");

            migrationBuilder.AddColumn<int>(
                name: "continentID",
                table: "Country",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Country_continentID",
                table: "Country",
                column: "continentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Country_Continent_continentID",
                table: "Country",
                column: "continentID",
                principalTable: "Continent",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Country_Continent_continentID",
                table: "Country");

            migrationBuilder.DropIndex(
                name: "IX_Country_continentID",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "continentID",
                table: "Country");

            migrationBuilder.AddColumn<int>(
                name: "continent",
                table: "Country",
                nullable: false,
                defaultValue: 0);
        }
    }
}
