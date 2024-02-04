using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DspLib.Migrations
{
    /// <inheritdoc />
    public partial class SeedInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalaxiesInfo");

            migrationBuilder.DropColumn(
                name: "种子号",
                table: "SeedStarsTypeCountInfo");

            migrationBuilder.DropColumn(
                name: "种子号",
                table: "SeedPlanetsTypeCountInfo");

            migrationBuilder.AddColumn<uint>(
                name: "SeedInfoId",
                table: "SeedStarsTypeCountInfo",
                type: "INT(4) UNSIGNED",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "SeedInfoId",
                table: "SeedPlanetsTypeCountInfo",
                type: "INT(4) UNSIGNED",
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateTable(
                name: "SeedInfo",
                columns: table => new
                {
                    SeedInfoId = table.Column<uint>(type: "INT(4) UNSIGNED", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    种子号 = table.Column<int>(type: "INT(4)", nullable: false),
                    巨星数 = table.Column<int>(type: "int", nullable: false),
                    最多卫星 = table.Column<int>(type: "int", nullable: false),
                    最多潮汐星 = table.Column<int>(type: "int", nullable: false),
                    潮汐星球数 = table.Column<int>(type: "int", nullable: false),
                    熔岩星球数 = table.Column<int>(type: "int", nullable: false),
                    海洋星球数 = table.Column<int>(type: "int", nullable: false),
                    沙漠星球数 = table.Column<int>(type: "int", nullable: false),
                    冰冻星球数 = table.Column<int>(type: "int", nullable: false),
                    气态星球数 = table.Column<int>(type: "int", nullable: false),
                    总星球数量 = table.Column<int>(type: "int", nullable: false),
                    最高亮度 = table.Column<float>(type: "float", nullable: false),
                    星球总亮度 = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedInfo", x => x.SeedInfoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SeedGalaxiesInfo",
                columns: table => new
                {
                    SeedGalaxiesInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeedInfoId = table.Column<uint>(type: "INT(4) UNSIGNED", nullable: false),
                    恒星类型 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    光谱类型 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: true),
                    恒星光度 = table.Column<float>(type: "float", nullable: false),
                    星系距离 = table.Column<float>(type: "float", nullable: false),
                    环盖首星 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    星系坐标x = table.Column<float>(type: "float", nullable: false),
                    星系坐标y = table.Column<float>(type: "float", nullable: false),
                    星系坐标z = table.Column<float>(type: "float", nullable: false),
                    潮汐星数 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    最多卫星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    星球数量 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    星球类型 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    是否有水 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    有硫酸否 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    铁矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    铜矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    硅矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    钛矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    石矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    煤矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    原油涌泉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    可燃冰矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    金伯利矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    分形硅矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    有机晶体矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    光栅石矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    刺笋矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    单极磁矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedGalaxiesInfo", x => x.SeedGalaxiesInfoId);
                    table.ForeignKey(
                        name: "FK_SeedGalaxiesInfo_SeedInfo_SeedInfoId",
                        column: x => x.SeedInfoId,
                        principalTable: "SeedInfo",
                        principalColumn: "SeedInfoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SeedStarsTypeCountInfo_SeedInfoId",
                table: "SeedStarsTypeCountInfo",
                column: "SeedInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeedPlanetsTypeCountInfo_SeedInfoId",
                table: "SeedPlanetsTypeCountInfo",
                column: "SeedInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeedGalaxiesInfo_SeedInfoId",
                table: "SeedGalaxiesInfo",
                column: "SeedInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeedPlanetsTypeCountInfo_SeedInfo_SeedInfoId",
                table: "SeedPlanetsTypeCountInfo",
                column: "SeedInfoId",
                principalTable: "SeedInfo",
                principalColumn: "SeedInfoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeedStarsTypeCountInfo_SeedInfo_SeedInfoId",
                table: "SeedStarsTypeCountInfo",
                column: "SeedInfoId",
                principalTable: "SeedInfo",
                principalColumn: "SeedInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeedPlanetsTypeCountInfo_SeedInfo_SeedInfoId",
                table: "SeedPlanetsTypeCountInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_SeedStarsTypeCountInfo_SeedInfo_SeedInfoId",
                table: "SeedStarsTypeCountInfo");

            migrationBuilder.DropTable(
                name: "SeedGalaxiesInfo");

            migrationBuilder.DropTable(
                name: "SeedInfo");

            migrationBuilder.DropIndex(
                name: "IX_SeedStarsTypeCountInfo_SeedInfoId",
                table: "SeedStarsTypeCountInfo");

            migrationBuilder.DropIndex(
                name: "IX_SeedPlanetsTypeCountInfo_SeedInfoId",
                table: "SeedPlanetsTypeCountInfo");

            migrationBuilder.DropColumn(
                name: "SeedInfoId",
                table: "SeedStarsTypeCountInfo");

            migrationBuilder.DropColumn(
                name: "SeedInfoId",
                table: "SeedPlanetsTypeCountInfo");

            migrationBuilder.AddColumn<int>(
                name: "种子号",
                table: "SeedStarsTypeCountInfo",
                type: "INT(4)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "种子号",
                table: "SeedPlanetsTypeCountInfo",
                type: "INT(4)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GalaxiesInfo",
                columns: table => new
                {
                    GalaxiesInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    光栅石矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    光谱类型 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: true),
                    分形硅矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    刺笋矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    单极磁矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    原油涌泉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    可燃冰矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    恒星光度 = table.Column<float>(type: "float", nullable: false),
                    恒星类型 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    星球数量 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    星球类型 = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    星系坐标x = table.Column<float>(type: "float", nullable: false),
                    星系坐标y = table.Column<float>(type: "float", nullable: false),
                    星系坐标z = table.Column<float>(type: "float", nullable: false),
                    星系距离 = table.Column<float>(type: "float", nullable: false),
                    是否有水 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    最多卫星 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    有机晶体矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    有硫酸否 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    潮汐星数 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    煤矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    环盖首星 = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    石矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    硅矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    种子号码 = table.Column<int>(type: "INT(4)", nullable: false),
                    金伯利矿 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    钛矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    铁矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false),
                    铜矿脉 = table.Column<byte>(type: "TINYINT(1) UNSIGNED", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalaxiesInfo", x => x.GalaxiesInfoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
