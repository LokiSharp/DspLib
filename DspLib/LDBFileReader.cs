using System.Text;
using DspLib.Algorithms;
using DspLib.Enum;
using DspLib.Protos;

namespace DspLib;

public class LDBFileReader
{
    public object ReadFile(string filePath)
    {
        using var reader = new BinaryReader(File.Open(filePath, FileMode.Open));
        // 读取文件头部信息
        var header = reader.ReadBytes(28);

        // 读取文件名
        var fileName = ReadUTF8String(reader);

        // 读取表名
        var tableName = ReadUTF8String(reader);

        // 读取签名
        var signature = ReadUTF8String(reader);

        // 读取数据数组长度
        var data_array_length = reader.ReadInt32();

        var protoSet = new object();

        switch (tableName)
        {
            // 根据表名读取数据数组
            case "Theme":
            {
                protoSet = new ThemeProtoSet
                {
                    TableName = tableName,
                    Signature = signature,
                    dataArray = new ThemeProto[data_array_length]
                };
                for (var i = 0; i < data_array_length; i++)
                    (protoSet as ThemeProtoSet)!.dataArray[i] = ReadThemeProto(reader);

                (protoSet as ThemeProtoSet)!.OnAfterDeserialize();
                break;
            }
            case "Item":
            {
                protoSet = new ItemProtoSet
                {
                    TableName = tableName,
                    Signature = signature,
                    dataArray = new ItemProto[data_array_length]
                };
                for (var i = 0; i < data_array_length; i++)
                    (protoSet as ItemProtoSet)!.dataArray[i] = ReadItemProto(reader);

                (protoSet as ItemProtoSet)!.OnAfterDeserialize();
                break;
            }
            case "Vein":
            {
                protoSet = new VeinProtoSet
                {
                    TableName = tableName,
                    Signature = signature,
                    dataArray = new VeinProto[data_array_length]
                };
                for (var i = 0; i < data_array_length; i++)
                    (protoSet as VeinProtoSet)!.dataArray[i] = ReadVeinProto(reader);

                (protoSet as VeinProtoSet)!.OnAfterDeserialize();
                break;
            }

            case "Recipe":
            {
                protoSet = new RecipeProtoSet
                {
                    TableName = tableName,
                    Signature = signature,
                    dataArray = new RecipeProto[data_array_length]
                };
                for (var i = 0; i < data_array_length; i++)
                    (protoSet as RecipeProtoSet)!.dataArray[i] = ReadRecipeProto(reader);

                (protoSet as RecipeProtoSet)!.OnAfterDeserialize();
                break;
            }
        }

        return protoSet;
    }

    private string ReadUTF8String(BinaryReader reader)
    {
        // 读取字符串长度
        var length = reader.ReadInt32();

        // 读取字符串
        var stringBytes = reader.ReadBytes(length);

        // 解码字符串
        var result = Encoding.UTF8.GetString(stringBytes);

        // 跳过填充字节
        var paddingLength = (4 - length % 4) % 4;
        reader.ReadBytes(paddingLength);

        return result;
    }

    private int[] ReadInt32Array(BinaryReader reader)
    {
        // 读取数组长度
        var length = reader.ReadInt32();
        var array = new int[length];
        for (var i = 0; i < length; i++) array[i] = reader.ReadInt32();

        return array;
    }

    private float[] ReadFloatArray(BinaryReader reader)
    {
        // 读取数组长度
        var length = reader.ReadInt32();
        var array = new float[length];
        for (var i = 0; i < length; i++) array[i] = reader.ReadSingle();

        return array;
    }

    private bool ReadBool(BinaryReader reader)
    {
        var result = reader.ReadBoolean();
        reader.ReadBytes(3);

        return result;
    }

    private Vector2 ReadVector2(BinaryReader reader)
    {
        return new Vector2(reader.ReadSingle(), reader.ReadSingle());
    }

    private ItemProto ReadItemProto(BinaryReader reader)
    {
        return new ItemProto
        {
            Name = ReadUTF8String(reader),
            ID = reader.ReadInt32(),
            SID = ReadUTF8String(reader),
            Type = (EItemType)reader.ReadInt32(),
            SubID = reader.ReadInt32(),
            MiningFrom = ReadUTF8String(reader),
            ProduceFrom = ReadUTF8String(reader),
            StackSize = reader.ReadInt32(),
            Grade = reader.ReadInt32(),
            Upgrades = ReadInt32Array(reader),
            IsFluid = ReadBool(reader),
            IsEntity = ReadBool(reader),
            CanBuild = ReadBool(reader),
            BuildInGas = ReadBool(reader),
            IconPath = ReadUTF8String(reader),
            ModelIndex = reader.ReadInt32(),
            ModelCount = reader.ReadInt32(),
            HpMax = reader.ReadInt32(),
            Ability = reader.ReadInt32(),
            HeatValue = reader.ReadInt64(),
            Potential = reader.ReadInt64(),
            ReactorInc = reader.ReadSingle(),
            FuelType = reader.ReadInt32(),
            AmmoType = (EAmmoType)reader.ReadInt32(),
            BombType = reader.ReadInt32(),
            CraftType = reader.ReadInt32(),
            BuildIndex = reader.ReadInt32(),
            BuildMode = reader.ReadInt32(),
            GridIndex = reader.ReadInt32(),
            UnlockKey = reader.ReadInt32(),
            PreTechOverride = reader.ReadInt32(),
            Productive = ReadBool(reader),
            MechaMaterialID = reader.ReadInt32(),
            DropRate = reader.ReadSingle(),
            EnemyDropLevel = reader.ReadInt32(),
            EnemyDropRange = ReadVector2(reader),
            EnemyDropCount = reader.ReadSingle(),
            EnemyDropMask = reader.ReadInt32(),
            EnemyDropMaskRatio = reader.ReadSingle(),
            DescFields = ReadInt32Array(reader),
            Description = ReadUTF8String(reader)
        };
    }

    private ThemeProto ReadThemeProto(BinaryReader reader)
    {
        return new ThemeProto
        {
            Name = ReadUTF8String(reader),
            ID = reader.ReadInt32(),
            SID = ReadUTF8String(reader),
            DisplayName = ReadUTF8String(reader),
            BriefIntroduction = ReadUTF8String(reader),
            PlanetType = (EPlanetType)reader.ReadInt32(),
            MaterialPath = ReadUTF8String(reader),
            Temperature = reader.ReadSingle(),
            Distribute = (EThemeDistribute)reader.ReadInt32(),
            Algos = ReadInt32Array(reader),
            ModX = ReadVector2(reader),
            ModY = ReadVector2(reader),
            EigenBit = reader.ReadInt32(),
            Vegetables0 = ReadInt32Array(reader),
            Vegetables1 = ReadInt32Array(reader),
            Vegetables2 = ReadInt32Array(reader),
            Vegetables3 = ReadInt32Array(reader),
            Vegetables4 = ReadInt32Array(reader),
            Vegetables5 = ReadInt32Array(reader),
            VeinSpot = ReadInt32Array(reader),
            VeinCount = ReadFloatArray(reader),
            VeinOpacity = ReadFloatArray(reader),
            RareVeins = ReadInt32Array(reader),
            RareSettings = ReadFloatArray(reader),
            GasItems = ReadInt32Array(reader),
            GasSpeeds = ReadFloatArray(reader),
            UseHeightForBuild = ReadBool(reader),
            Wind = reader.ReadSingle(),
            IonHeight = reader.ReadSingle(),
            WaterHeight = reader.ReadSingle(),
            WaterItemId = reader.ReadInt32(),
            Musics = ReadInt32Array(reader),
            SFXPath = ReadUTF8String(reader),
            SFXVolume = reader.ReadSingle(),
            CullingRadius = reader.ReadSingle(),
            IceFlag = reader.ReadInt32()
        };
    }

    private VeinProto ReadVeinProto(BinaryReader reader)
    {
        return new VeinProto
        {
            Name = ReadUTF8String(reader),
            ID = reader.ReadInt32(),
            SID = ReadUTF8String(reader),
            IconPath = ReadUTF8String(reader),
            Description = ReadUTF8String(reader),
            ModelIndex = reader.ReadInt32(),
            ModelCount = reader.ReadInt32(),
            CircleRadius = reader.ReadSingle(),
            MiningItem = reader.ReadInt32(),
            MiningTime = reader.ReadInt32(),
            MiningAudio = reader.ReadInt32(),
            MiningEffect = reader.ReadInt32(),
            MinerBaseModelIndex = reader.ReadInt32(),
            MinerCircleModelIndex = reader.ReadInt32()
        };
    }

    private RecipeProto ReadRecipeProto(BinaryReader reader)
    {
        return new RecipeProto
        {
            Name = ReadUTF8String(reader),
            ID = reader.ReadInt32(),
            SID = ReadUTF8String(reader),
            Type = (ERecipeType)reader.ReadInt32(),
            Handcraft = ReadBool(reader),
            Explicit = ReadBool(reader),
            TimeSpend = reader.ReadInt32(),
            Items = ReadInt32Array(reader),
            ItemCounts = ReadInt32Array(reader),
            Results = ReadInt32Array(reader),
            ResultCounts = ReadInt32Array(reader),
            GridIndex = reader.ReadInt32(),
            IconPath = ReadUTF8String(reader),
            Description = ReadUTF8String(reader),
            NonProductive = ReadBool(reader)
        };
    }
}