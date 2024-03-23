using Dapper;
using Npgsql;

namespace DspLib.DataBase;

public class DatabaseInitializer(string connectionString)
{
    public void CreateTable()
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();


        var createSeedInfoTableQuery = CreateSeedInfoTable();
        var createSeedGalaxyInfoTableQuery = SeedGalaxyInfosTable();
        var createSeedPlanetsTypeCountInfoTableQuery = SeedPlanetsTypeCountInfoTable();
        var createSeedStarsTypeCountInfoTableQuery = SeedStarsTypeCountInfoTable();

        connection.Execute(createSeedInfoTableQuery);
        connection.Execute(createSeedGalaxyInfoTableQuery);
        connection.Execute(createSeedPlanetsTypeCountInfoTableQuery);
        connection.Execute(createSeedStarsTypeCountInfoTableQuery);
    }

    private string CreateSeedInfoTable()
    {
        var createSeedInfoTableQuery = @"
CREATE TABLE ""SeedInfo""
(
    ""SeedInfoId""       INT NOT NULL,
    种子号           INT NOT NULL,
    巨星数           SMALLINT NOT NULL,
    最多卫星         SMALLINT NOT NULL,
    最多潮汐星       SMALLINT NOT NULL,
    潮汐星球数       SMALLINT NOT NULL,
    最多潮汐永昼永夜 SMALLINT NOT NULL,
    潮汐永昼永夜数   SMALLINT NOT NULL,
    熔岩星球数       SMALLINT NOT NULL,
    海洋星球数       SMALLINT NOT NULL,
    沙漠星球数       SMALLINT NOT NULL,
    冰冻星球数       SMALLINT NOT NULL,
    气态星球数       SMALLINT NOT NULL,
    总星球数量       SMALLINT NOT NULL,
    最高亮度         FLOAT NOT NULL,
    星球总亮度       FLOAT NOT NULL,
    PRIMARY KEY (""SeedInfoId"")
) USING columnar;";

        return createSeedInfoTableQuery;
    }

    private string SeedGalaxyInfosTable()
    {
        var createSeedGalaxyInfosTableQuery = @"
CREATE TABLE ""SeedGalaxyInfo""
(
    ""SeedGalaxyInfoId""   BIGINT NOT NULL,
    ""SeedInfoId""         INT NOT NULL,
    恒星类型           SMALLINT NOT NULL,
    光谱类型           SMALLINT NOT NULL,
    恒星光度           FLOAT NOT NULL,
    星系距离           FLOAT NOT NULL,
    环盖首星           BOOLEAN NOT NULL,
    星系坐标x          FLOAT NOT NULL,
    星系坐标y          FLOAT NOT NULL,
    星系坐标z          FLOAT NOT NULL,
    潮汐星数           SMALLINT NOT NULL,
    最多卫星           SMALLINT NOT NULL,
    星球数量           SMALLINT NOT NULL,
    星球类型           CHAR(5),
    是否有水           BOOLEAN NOT NULL,
    有硫酸否           BOOLEAN NOT NULL,
    铁矿脉             SMALLINT NOT NULL,
    铜矿脉             SMALLINT NOT NULL,
    硅矿脉             SMALLINT NOT NULL,
    钛矿脉             SMALLINT NOT NULL,
    石矿脉             SMALLINT NOT NULL,
    煤矿脉             SMALLINT NOT NULL,
    原油涌泉           SMALLINT NOT NULL,
    可燃冰矿           SMALLINT NOT NULL,
    金伯利矿           SMALLINT NOT NULL,
    分形硅矿           SMALLINT NOT NULL,
    有机晶体矿         SMALLINT NOT NULL,
    光栅石矿           SMALLINT NOT NULL,
    刺笋矿脉           SMALLINT NOT NULL,
    单极磁矿           SMALLINT NOT NULL,
    PRIMARY KEY (""SeedGalaxyInfoId"", ""SeedInfoId"")
) USING columnar;";

        return createSeedGalaxyInfosTableQuery;
    }

    private string SeedPlanetsTypeCountInfoTable()
    {
        var createSeedPlanetsTypeCountInfoTableQuery = @"
CREATE TABLE ""SeedPlanetsTypeCountInfo""
(
    ""SeedInfoId""                  INT NOT NULL,
    地中海                      SMALLINT NOT NULL,
    气态巨星1                   SMALLINT NOT NULL,
    气态巨星2                   SMALLINT NOT NULL,
    冰巨星1                     SMALLINT NOT NULL,
    冰巨星2                     SMALLINT NOT NULL,
    干旱荒漠                    SMALLINT NOT NULL,
    灰烬冻土                    SMALLINT NOT NULL,
    海洋丛林                    SMALLINT NOT NULL,
    熔岩                        SMALLINT NOT NULL,
    冰原冻土                    SMALLINT NOT NULL,
    贫瘠荒漠                    SMALLINT NOT NULL,
    戈壁                        SMALLINT NOT NULL,
    火山灰                      SMALLINT NOT NULL,
    红石                        SMALLINT NOT NULL,
    草原                        SMALLINT NOT NULL,
    水世界                      SMALLINT NOT NULL,
    黑石盐滩                    SMALLINT NOT NULL,
    樱林海                      SMALLINT NOT NULL,
    飓风石林                    SMALLINT NOT NULL,
    猩红冰湖                    SMALLINT NOT NULL,
    气态巨星3                   SMALLINT NOT NULL,
    热带草原                    SMALLINT NOT NULL,
    橙晶荒漠                    SMALLINT NOT NULL,
    极寒冻土                    SMALLINT NOT NULL,
    潘多拉沼泽                  SMALLINT NOT NULL,
    PRIMARY KEY (""SeedInfoId"")
) USING columnar;";

        return createSeedPlanetsTypeCountInfoTableQuery;
    }

    private string SeedStarsTypeCountInfoTable()
    {
        var createSeedStarsTypeCountInfoTableQuery = @"
CREATE TABLE ""SeedStarsTypeCountInfo""
(
    ""SeedInfoId""                INT NOT NULL,
    M型恒星                   SMALLINT NOT NULL,
    K型恒星                   SMALLINT NOT NULL,
    G型恒星                   SMALLINT NOT NULL,
    F型恒星                   SMALLINT NOT NULL,
    A型恒星                   SMALLINT NOT NULL,
    B型恒星                   SMALLINT NOT NULL,
    O型恒星                   SMALLINT NOT NULL,
    X型恒星                   SMALLINT NOT NULL,
    M型巨星                   SMALLINT NOT NULL,
    K型巨星                   SMALLINT NOT NULL,
    G型巨星                   SMALLINT NOT NULL,
    F型巨星                   SMALLINT NOT NULL,
    A型巨星                   SMALLINT NOT NULL,
    B型巨星                   SMALLINT NOT NULL,
    O型巨星                   SMALLINT NOT NULL,
    X型巨星                   SMALLINT NOT NULL,
    白矮星                    SMALLINT NOT NULL,
    中子星                    SMALLINT NOT NULL,
    黑洞                      SMALLINT NOT NULL,
    PRIMARY KEY (""SeedInfoId"")
) USING columnar;";

        return createSeedStarsTypeCountInfoTableQuery;
    }
}