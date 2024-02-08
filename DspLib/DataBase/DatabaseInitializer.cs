using Dapper;
using MySqlConnector;

namespace DspLib.DataBase;

public class DatabaseInitializer
{
    private readonly string connectionString = $"Server={Environment.GetEnvironmentVariable("Server")};" +
                                               $"Database={Environment.GetEnvironmentVariable("Database")};" +
                                               $"User={Environment.GetEnvironmentVariable("User")};" +
                                               $"Password={Environment.GetEnvironmentVariable("Password")};" +
                                               $"Port={Environment.GetEnvironmentVariable("Port")};" +
                                               $"Allow User Variables=true";

    public void CreateTable(int numOfTables)
    {
        using var connection = new MySqlConnection(connectionString);
        connection.Open();

        for (var i = 0; i < numOfTables; i++)
        {
            var createSeedInfoTableQuery = CreateSeedInfoTable(i);
            var createSeedGalaxyInfosTableQuery = SeedGalaxyInfosTable(i);
            var createSeedPlanetsTypeCountInfoTableQuery = SeedPlanetsTypeCountInfoTable(i);
            var createSeedStarsTypeCountInfoTableQuery = SeedStarsTypeCountInfoTable(i);

            connection.Execute(createSeedInfoTableQuery);
            connection.Execute(createSeedGalaxyInfosTableQuery);
            connection.Execute(createSeedPlanetsTypeCountInfoTableQuery);
            connection.Execute(createSeedStarsTypeCountInfoTableQuery);
        }
    }

    private string CreateSeedInfoTable(int tableIndex)
    {
        var createSeedInfoTableQuery = $@"
        CREATE TABLE SeedInfo{tableIndex}
        (
            SeedInfoId       BIGINT UNSIGNED NOT NULL PRIMARY KEY,
            种子号           INT UNSIGNED NOT NULL,
            巨星数           TINYINT UNSIGNED NOT NULL,
            最多卫星         TINYINT UNSIGNED NOT NULL,
            最多潮汐星       TINYINT UNSIGNED NOT NULL,
            潮汐星球数       TINYINT UNSIGNED NOT NULL,
            最多潮汐永昼永夜 TINYINT UNSIGNED NOT NULL,
            潮汐永昼永夜数   TINYINT UNSIGNED NOT NULL,
            熔岩星球数       TINYINT UNSIGNED NOT NULL,
            海洋星球数       TINYINT UNSIGNED NOT NULL,
            沙漠星球数       TINYINT UNSIGNED NOT NULL,
            冰冻星球数       TINYINT UNSIGNED NOT NULL,
            气态星球数       TINYINT UNSIGNED NOT NULL,
            总星球数量       TINYINT UNSIGNED NOT NULL,
            最高亮度         FLOAT NOT NULL,
            星球总亮度       FLOAT NOT NULL
        );";
        return createSeedInfoTableQuery;
    }

    private string SeedGalaxyInfosTable(int tableIndex)
    {
        var createSeedGalaxyInfosTableQuery = $@"
CREATE TABLE SeedGalaxyInfo{tableIndex}
(
    SeedGalaxyInfoId   BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    SeedInfoId         BIGINT UNSIGNED NOT NULL,
    恒星类型           TINYINT UNSIGNED NOT NULL,
    光谱类型           TINYINT UNSIGNED NOT NULL,
    恒星光度           FLOAT NOT NULL,
    星系距离           FLOAT NOT NULL,
    环盖首星           BOOLEAN NOT NULL,
    星系坐标x          FLOAT NOT NULL,
    星系坐标y          FLOAT NOT NULL,
    星系坐标z          FLOAT NOT NULL,
    潮汐星数           TINYINT UNSIGNED NOT NULL,
    最多卫星           TINYINT UNSIGNED NOT NULL,
    星球数量           TINYINT UNSIGNED NOT NULL,
    星球类型           CHAR(16) CHARACTER SET ascii,
    是否有水           BOOLEAN NOT NULL,
    有硫酸否           BOOLEAN NOT NULL,
    FOREIGN KEY (SeedInfoId) REFERENCES SeedInfo{tableIndex}(SeedInfoId)
);";
        return createSeedGalaxyInfosTableQuery;
    }

    private string SeedPlanetsTypeCountInfoTable(int tableIndex)
    {
        var createSeedPlanetsTypeCountInfoTableQuery = $@"
CREATE TABLE SeedPlanetsTypeCountInfo{tableIndex}
(
    SeedPlanetsTypeCountInfoId  BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    SeedInfoId                  BIGINT UNSIGNED NOT NULL,
    地中海                      TINYINT UNSIGNED NOT NULL,
    气态巨星1                   TINYINT UNSIGNED NOT NULL,
    气态巨星2                   TINYINT UNSIGNED NOT NULL,
    冰巨星1                     TINYINT UNSIGNED NOT NULL,
    冰巨星2                     TINYINT UNSIGNED NOT NULL,
    干旱荒漠                    TINYINT UNSIGNED NOT NULL,
    灰烬冻土                    TINYINT UNSIGNED NOT NULL,
    海洋丛林                    TINYINT UNSIGNED NOT NULL,
    熔岩                        TINYINT UNSIGNED NOT NULL,
    冰原冻土                    TINYINT UNSIGNED NOT NULL,
    贫瘠荒漠                    TINYINT UNSIGNED NOT NULL,
    戈壁                        TINYINT UNSIGNED NOT NULL,
    火山灰                      TINYINT UNSIGNED NOT NULL,
    红石                        TINYINT UNSIGNED NOT NULL,
    草原                        TINYINT UNSIGNED NOT NULL,
    水世界                      TINYINT UNSIGNED NOT NULL,
    黑石盐滩                    TINYINT UNSIGNED NOT NULL,
    樱林海                      TINYINT UNSIGNED NOT NULL,
    飓风石林                    TINYINT UNSIGNED NOT NULL,
    猩红冰湖                    TINYINT UNSIGNED NOT NULL,
    气态巨星3                   TINYINT UNSIGNED NOT NULL,
    热带草原                    TINYINT UNSIGNED NOT NULL,
    橙晶荒漠                    TINYINT UNSIGNED NOT NULL,
    极寒冻土                    TINYINT UNSIGNED NOT NULL,
    潘多拉沼泽                  TINYINT UNSIGNED NOT NULL,
    FOREIGN KEY (SeedInfoId) REFERENCES SeedInfo{tableIndex}(SeedInfoId)
);";
        return createSeedPlanetsTypeCountInfoTableQuery;
    }

    private string SeedStarsTypeCountInfoTable(int tableIndex)
    {
        var createSeedStarsTypeCountInfoTableQuery = $@"
CREATE TABLE SeedStarsTypeCountInfo{tableIndex}
(
    SeedStarsTypeCountInfoId  BIGINT UNSIGNED NOT NULL PRIMARY KEY,
    SeedInfoId                BIGINT UNSIGNED NOT NULL,
    M型恒星                   TINYINT UNSIGNED NOT NULL,
    K型恒星                   TINYINT UNSIGNED NOT NULL,
    G型恒星                   TINYINT UNSIGNED NOT NULL,
    F型恒星                   TINYINT UNSIGNED NOT NULL,
    A型恒星                   TINYINT UNSIGNED NOT NULL,
    B型恒星                   TINYINT UNSIGNED NOT NULL,
    O型恒星                   TINYINT UNSIGNED NOT NULL,
    X型恒星                   TINYINT UNSIGNED NOT NULL,
    M型巨星                   TINYINT UNSIGNED NOT NULL,
    K型巨星                   TINYINT UNSIGNED NOT NULL,
    G型巨星                   TINYINT UNSIGNED NOT NULL,
    F型巨星                   TINYINT UNSIGNED NOT NULL,
    A型巨星                   TINYINT UNSIGNED NOT NULL,
    B型巨星                   TINYINT UNSIGNED NOT NULL,
    O型巨星                   TINYINT UNSIGNED NOT NULL,
    X型巨星                   TINYINT UNSIGNED NOT NULL,
    白矮星                    TINYINT UNSIGNED NOT NULL,
    中子星                    TINYINT UNSIGNED NOT NULL,
    黑洞                      TINYINT UNSIGNED NOT NULL,
    FOREIGN KEY (SeedInfoId) REFERENCES SeedInfo{tableIndex}(SeedInfoId)
);";
        return createSeedStarsTypeCountInfoTableQuery;
    }
}