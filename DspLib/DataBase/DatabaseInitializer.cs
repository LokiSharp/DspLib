using Dapper;
using DuckDB.NET.Data;

namespace DspLib.DataBase;

public class DatabaseInitializer(string connectionString)
{
    public void CreateTable()
    {
        using var connection = new DuckDBConnection(connectionString);
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
        var createSeedInfoTableQuery = $@"
CREATE TABLE SeedInfo
(
    SeedInfoId      INTEGER,
    种子号           INTEGER,
    巨星数           SMALLINT,
    最多卫星         SMALLINT,
    最多潮汐星       SMALLINT,
    潮汐星球数       SMALLINT,
    最多潮汐永昼永夜 SMALLINT,
    潮汐永昼永夜数   SMALLINT,
    熔岩星球数       SMALLINT,
    海洋星球数       SMALLINT,
    沙漠星球数       SMALLINT,
    冰冻星球数       SMALLINT,
    气态星球数       SMALLINT,
    总星球数量       SMALLINT,
    最高亮度         FLOAT,
    星球总亮度       FLOAT
);";

        return createSeedInfoTableQuery;
    }

    private string SeedGalaxyInfosTable()
    {
        var createSeedGalaxyInfosTableQuery = $@"
CREATE TABLE SeedGalaxyInfo
(
    SeedGalaxyInfoId   BIGINT,
    SeedInfoId         INT,
    恒星类型           SMALLINT,
    光谱类型           SMALLINT,
    恒星光度           FLOAT,
    星系距离           FLOAT,
    环盖首星           BOOLEAN,
    星系坐标x          FLOAT,
    星系坐标y          FLOAT,
    星系坐标z          FLOAT,
    潮汐星数           SMALLINT,
    最多卫星           SMALLINT,
    星球数量           SMALLINT,
    星球类型           CHAR(5),
    是否有水           BOOLEAN,
    有硫酸否           BOOLEAN,
    铁矿脉             SMALLINT,
    铜矿脉             SMALLINT,
    硅矿脉             SMALLINT,
    钛矿脉             SMALLINT,
    石矿脉             SMALLINT,
    煤矿脉             SMALLINT,
    原油涌泉           SMALLINT,
    可燃冰矿           SMALLINT,
    金伯利矿           SMALLINT,
    分形硅矿           SMALLINT,
    有机晶体矿         SMALLINT,
    光栅石矿           SMALLINT,
    刺笋矿脉           SMALLINT,
    单极磁矿           SMALLINT
);";

        return createSeedGalaxyInfosTableQuery;
    }

    private string SeedPlanetsTypeCountInfoTable()
    {
        var createSeedPlanetsTypeCountInfoTableQuery = $@"
CREATE TABLE SeedPlanetsTypeCountInfo
(
    SeedInfoId                  INT,
    地中海                      SMALLINT,
    气态巨星1                   SMALLINT,
    气态巨星2                   SMALLINT,
    冰巨星1                     SMALLINT,
    冰巨星2                     SMALLINT,
    干旱荒漠                    SMALLINT,
    灰烬冻土                    SMALLINT,
    海洋丛林                    SMALLINT,
    熔岩                        SMALLINT,
    冰原冻土                    SMALLINT,
    贫瘠荒漠                    SMALLINT,
    戈壁                        SMALLINT,
    火山灰                      SMALLINT,
    红石                        SMALLINT,
    草原                        SMALLINT,
    水世界                      SMALLINT,
    黑石盐滩                    SMALLINT,
    樱林海                      SMALLINT,
    飓风石林                    SMALLINT,
    猩红冰湖                    SMALLINT,
    气态巨星3                   SMALLINT,
    热带草原                    SMALLINT,
    橙晶荒漠                    SMALLINT,
    极寒冻土                    SMALLINT,
    潘多拉沼泽                  SMALLINT
);";

        return createSeedPlanetsTypeCountInfoTableQuery;
    }

    private string SeedStarsTypeCountInfoTable()
    {
        var createSeedStarsTypeCountInfoTableQuery = $@"
CREATE TABLE SeedStarsTypeCountInfo
(
    SeedInfoId                INT,
    M型恒星                   SMALLINT,
    K型恒星                   SMALLINT,
    G型恒星                   SMALLINT,
    F型恒星                   SMALLINT,
    A型恒星                   SMALLINT,
    B型恒星                   SMALLINT,
    O型恒星                   SMALLINT,
    X型恒星                   SMALLINT,
    M型巨星                   SMALLINT,
    K型巨星                   SMALLINT,
    G型巨星                   SMALLINT,
    F型巨星                   SMALLINT,
    A型巨星                   SMALLINT,
    B型巨星                   SMALLINT,
    O型巨星                   SMALLINT,
    X型巨星                   SMALLINT,
    白矮星                    SMALLINT,
    中子星                    SMALLINT,
    黑洞                      SMALLINT
);";

        return createSeedStarsTypeCountInfoTableQuery;
    }
}