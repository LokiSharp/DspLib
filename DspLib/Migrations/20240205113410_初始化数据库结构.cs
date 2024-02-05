using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DspLib.Migrations
{
    /// <inheritdoc />
    public partial class 初始化数据库结构 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

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
                    最多潮汐永昼永夜 = table.Column<int>(type: "int", nullable: false),
                    潮汐永昼永夜数 = table.Column<int>(type: "int", nullable: false),
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
                name: "SeedGalaxyInfo",
                columns: table => new
                {
                    SeedGalaxyInfoId = table.Column<int>(type: "int", nullable: false)
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
                    有硫酸否 = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedGalaxyInfo", x => x.SeedGalaxyInfoId);
                    table.ForeignKey(
                        name: "FK_SeedGalaxyInfo_SeedInfo_SeedInfoId",
                        column: x => x.SeedInfoId,
                        principalTable: "SeedInfo",
                        principalColumn: "SeedInfoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SeedPlanetsTypeCountInfo",
                columns: table => new
                {
                    SeedPlanetsTypeCountInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeedInfoId = table.Column<uint>(type: "INT(4) UNSIGNED", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_SeedPlanetsTypeCountInfo_SeedInfo_SeedInfoId",
                        column: x => x.SeedInfoId,
                        principalTable: "SeedInfo",
                        principalColumn: "SeedInfoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SeedStarsTypeCountInfo",
                columns: table => new
                {
                    SeedStarsTypeCountInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SeedInfoId = table.Column<uint>(type: "INT(4) UNSIGNED", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_SeedStarsTypeCountInfo_SeedInfo_SeedInfoId",
                        column: x => x.SeedInfoId,
                        principalTable: "SeedInfo",
                        principalColumn: "SeedInfoId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SeedGalaxyInfo_SeedInfoId",
                table: "SeedGalaxyInfo",
                column: "SeedInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SeedPlanetsTypeCountInfo_SeedInfoId",
                table: "SeedPlanetsTypeCountInfo",
                column: "SeedInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeedStarsTypeCountInfo_SeedInfoId",
                table: "SeedStarsTypeCountInfo",
                column: "SeedInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeedGalaxyInfo");

            migrationBuilder.DropTable(
                name: "SeedPlanetsTypeCountInfo");

            migrationBuilder.DropTable(
                name: "SeedStarsTypeCountInfo");

            migrationBuilder.DropTable(
                name: "SeedInfo");
        }
    }
}
