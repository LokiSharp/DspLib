using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DspLib.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlanetsTypeCountInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_galaxies_info",
                table: "galaxies_info");

            migrationBuilder.RenameTable(
                name: "galaxies_info",
                newName: "GalaxiesInfos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalaxiesInfos",
                table: "GalaxiesInfos",
                column: "GalaxiesInfoId");

            migrationBuilder.CreateTable(
                name: "SeedPlanetsTypeCountInfo",
                columns: table => new
                {
                    SeedPlanetsTypeCountInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    种子号 = table.Column<int>(type: "INT(4)", nullable: false),
                    地中海 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    气态巨星1 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    气态巨星2 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    冰巨星1 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    冰巨星2 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    干旱荒漠 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    灰烬冻土 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    海洋丛林 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    熔岩 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    冰原冻土 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    贫瘠荒漠 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    戈壁 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    火山灰 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    红石 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    草原 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    水世界 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    黑石盐滩 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    樱林海 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    飓风石林 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    猩红冰湖 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    气态巨星3 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    热带草原 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    橙晶荒漠 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    极寒冻土 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    潘多拉沼泽 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedPlanetsTypeCountInfo", x => x.SeedPlanetsTypeCountInfoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeedPlanetsTypeCountInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GalaxiesInfos",
                table: "GalaxiesInfos");

            migrationBuilder.RenameTable(
                name: "GalaxiesInfos",
                newName: "galaxies_info");

            migrationBuilder.AddPrimaryKey(
                name: "PK_galaxies_info",
                table: "galaxies_info",
                column: "GalaxiesInfoId");
        }
    }
}
