using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserDataLayer.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Frienships_Users_FriendId",
                table: "Frienships");

            migrationBuilder.DropIndex(
                name: "IX_Frienships_FriendId",
                table: "Frienships");

            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Frienships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FriendId",
                table: "Frienships",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Frienships_FriendId",
                table: "Frienships",
                column: "FriendId");

            migrationBuilder.AddForeignKey(
                name: "FK_Frienships_Users_FriendId",
                table: "Frienships",
                column: "FriendId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
