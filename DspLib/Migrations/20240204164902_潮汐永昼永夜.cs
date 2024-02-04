using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DspLib.Migrations
{
    /// <inheritdoc />
    public partial class 潮汐永昼永夜 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "最多潮汐永昼永夜",
                table: "SeedInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "潮汐永昼永夜数",
                table: "SeedInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "最多潮汐永昼永夜",
                table: "SeedInfo");

            migrationBuilder.DropColumn(
                name: "潮汐永昼永夜数",
                table: "SeedInfo");
        }
    }
}
