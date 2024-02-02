using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DspLib.Migrations
{
    /// <inheritdoc />
    public partial class SeedStarsTypeCountInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GalaxiesInfos",
                table: "GalaxiesInfos");

            migrationBuilder.RenameTable(
                name: "GalaxiesInfos",
                newName: "GalaxiesInfo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalaxiesInfo",
                table: "GalaxiesInfo",
                column: "GalaxiesInfoId");

            migrationBuilder.CreateTable(
                name: "SeedStarsTypeCountInfo",
                columns: table => new
                {
                    SeedStarsTypeCountInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    种子号 = table.Column<int>(type: "INT(4)", nullable: false),
                    M型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    K型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    G型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    F型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    A型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    B型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    O型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    X型恒星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    M型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    K型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    G型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    F型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    A型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    B型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    O型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    X型巨星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    白矮星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    中子星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    黑洞 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedStarsTypeCountInfo", x => x.SeedStarsTypeCountInfoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeedStarsTypeCountInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GalaxiesInfo",
                table: "GalaxiesInfo");

            migrationBuilder.RenameTable(
                name: "GalaxiesInfo",
                newName: "GalaxiesInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalaxiesInfos",
                table: "GalaxiesInfos",
                column: "GalaxiesInfoId");
        }
    }
}
