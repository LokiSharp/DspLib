using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DspLib.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "galaxies_info",
                columns: table => new
                {
                    GalaxiesInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    种子号码 = table.Column<int>(type: "INT(4)", nullable: false),
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
                    table.PrimaryKey("PK_galaxies_info", x => x.GalaxiesInfoId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "galaxies_info");
        }
    }
}
