using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroRPG.Migrations
{
    public partial class AddUserHeroRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Heroes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_UserId",
                table: "Heroes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Users_UserId",
                table: "Heroes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Users_UserId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_UserId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Heroes");
        }
    }
}
