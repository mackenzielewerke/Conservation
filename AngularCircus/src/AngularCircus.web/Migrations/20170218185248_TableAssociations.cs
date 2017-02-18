using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AngularCircus.web.Migrations
{
    public partial class TableAssociations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CircusId",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShowDate",
                table: "Tickets",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Acts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PerformerId",
                table: "Acts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CircusId",
                table: "Tickets",
                column: "CircusId");

            migrationBuilder.CreateIndex(
                name: "IX_Acts_AnimalId",
                table: "Acts",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Acts_PerformerId",
                table: "Acts",
                column: "PerformerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acts_Animals_AnimalId",
                table: "Acts",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Acts_Performers_PerformerId",
                table: "Acts",
                column: "PerformerId",
                principalTable: "Performers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Circuses_CircusId",
                table: "Tickets",
                column: "CircusId",
                principalTable: "Circuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acts_Animals_AnimalId",
                table: "Acts");

            migrationBuilder.DropForeignKey(
                name: "FK_Acts_Performers_PerformerId",
                table: "Acts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Circuses_CircusId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_CircusId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Acts_AnimalId",
                table: "Acts");

            migrationBuilder.DropIndex(
                name: "IX_Acts_PerformerId",
                table: "Acts");

            migrationBuilder.DropColumn(
                name: "CircusId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ShowDate",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Acts");

            migrationBuilder.DropColumn(
                name: "PerformerId",
                table: "Acts");
        }
    }
}
