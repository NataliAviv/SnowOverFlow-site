using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SnowOverFlow.Migrations
{
    public partial class ziv21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Country_CountryID",
                table: "Site");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Site",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Site_CountryID",
                table: "Site",
                newName: "IX_Site_CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Country_CountryId",
                table: "Site",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Site_Country_CountryId",
                table: "Site");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Site",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Site_CountryId",
                table: "Site",
                newName: "IX_Site_CountryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Country_CountryID",
                table: "Site",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
