using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeroRPG.Migrations
{
    public partial class Add_FightProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Defeats",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fights",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "Heroes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Defeats",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Fights",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "Heroes");
        }
    }
}
