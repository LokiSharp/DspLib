using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Gen;

public class PlanetData
{
    public const int kMaxMeshCnt = 100;
    public const float kEnterAltitude = 1000f;
    public const float kBirthHeightShift = 1.45f;
    public int algoId;
    public Vector3 birthPoint;
    public Vector3 birthResourcePoint0;
    public Vector3 birthResourcePoint1;
    public bool calculated;
    public bool calculating;
    public PlanetRawData data;
    public bool[] dirtyFlags = new bool[100];
    public int factingCompletedStage = -1;
    public bool factoryLoaded;
    public bool factoryLoading;
    public GalaxyData galaxy;
    public float[] gasHeatValues;
    public int[] gasItems;
    public float[] gasSpeeds;
    public double gasTotalHeat;
    public float habitableBias;
    public int iceFlag;
    public int id;
    public int index;
    public int infoSeed;
    public float ionHeight;
    public float landPercent;
    public bool landPercentDirty;
    public bool levelized;
    public bool loaded;
    public bool loading;
    public float luminosity;
    public double mod_x;
    public double mod_y;
    public byte[] modData;
    public string name = "";
    public int number;
    public float obliquity;
    public double orbitalPeriod = 3600.0;
    public int orbitAround;
    public PlanetData orbitAroundPlanet;
    public float orbitInclination;
    public int orbitIndex;
    public float orbitLongitude;
    public float orbitPhase;
    public float orbitRadius = 1f;
    public string overrideName = "";
    public int precision = 160;
    public float radius = 200f;
    public double rotationPeriod = 480.0;
    public float rotationPhase;
    public Vector3 runtimeLocalSunDirection;
    public float runtimeOrbitPhase;
    public Quaternion runtimeOrbitRotation;
    public VectorLF3 runtimePosition;
    public VectorLF3 runtimePositionNext;
    public Quaternion runtimeRotation;
    public Quaternion runtimeRotationNext;
    public float runtimeRotationPhase;
    public Quaternion runtimeSystemRotation;
    public float scale = 1f;
    public int seed;
    public int segment = 5;
    public EPlanetSingularity singularity;
    public StarData star;
    public int style;
    public float sunDistance;
    public float temperatureBias;
    public int theme;
    public EPlanetType type;
    public VectorLF3 uPosition;
    public VectorLF3 uPositionNext;
    public Vector3 veinBiasVector;
    public VeinGroup[] veinGroups;
    public Mutex veinGroupsLock = new();
    public bool wanted;
    public float waterHeight;
    public int waterItemId;
    public float windStrength;

    public string displayName => !string.IsNullOrEmpty(overrideName) ? overrideName : name;

    public float realRadius => radius * scale;

    public string typeString
    {
        get
        {
            var typeString = "未知";
            var themeProto = LDB.themes.Select(theme);
            if (themeProto != null)
                typeString = themeProto.displayName;
            return typeString;
        }
    }

    public string briefString
    {
        get
        {
            var briefString = "";
            var themeProto = LDB.themes.Select(theme);
            if (themeProto != null)
                briefString = themeProto.BriefIntroduction;
            return briefString;
        }
    }

    public string singularityString
    {
        get
        {
            var singularityString = "";
            if (orbitAround > 0)
                singularityString += "卫星";
            if ((singularity & EPlanetSingularity.TidalLocked) != EPlanetSingularity.None)
                singularityString += "潮汐锁定永昼永夜";
            if ((singularity & EPlanetSingularity.TidalLocked2) != EPlanetSingularity.None)
                singularityString += "潮汐锁定1:2";
            if ((singularity & EPlanetSingularity.TidalLocked4) != EPlanetSingularity.None)
                singularityString += "潮汐锁定1:4";
            if ((singularity & EPlanetSingularity.LaySide) != EPlanetSingularity.None)
                singularityString += "横躺自转";
            if ((singularity & EPlanetSingularity.ClockwiseRotate) != EPlanetSingularity.None)
                singularityString += "反向自转";
            if ((singularity & EPlanetSingularity.MultipleSatellites) != EPlanetSingularity.None)
                singularityString += "多卫星";
            return singularityString;
        }
    }

    public void CalculateVeinGroups()
    {
        if (data == null)
            return;
        var veinPool = data.veinPool;
        var veinCursor = data.veinCursor;
        var num = 0;
        for (var index = 1; index < veinCursor; ++index)
        {
            int groupIndex = veinPool[index].groupIndex;
            if (groupIndex > num)
                num = groupIndex;
        }

        lock (veinGroupsLock)
        {
            if (veinGroups == null || veinGroups.Length != num + 1)
                veinGroups = new VeinGroup[num + 1];
            Array.Clear(veinGroups, 0, veinGroups.Length);
            veinGroups[0].SetNull();
            for (var index = 1; index < veinCursor; ++index)
                if (veinPool[index].id == index)
                {
                    int groupIndex = veinPool[index].groupIndex;
                    veinGroups[groupIndex].type = veinPool[index].type;
                    veinGroups[groupIndex].pos += veinPool[index].pos;
                    ++veinGroups[groupIndex].count;
                    veinGroups[groupIndex].amount += veinPool[index].amount;
                }

            veinGroups[0].type = EVeinType.None;
            for (var index = 0; index < veinGroups.Length; ++index)
                veinGroups[index].pos.Normalize();
        }
    }


    public void GenBirthPoints()
    {
        var dotNet35Random = new DotNet35Random(seed);
        dotNet35Random.Next();
        dotNet35Random.Next();
        dotNet35Random.Next();
        dotNet35Random.Next();
        GenBirthPoints(data, dotNet35Random.Next());
    }

    public void GenBirthPoints(PlanetRawData rawData, int _birthSeed)
    {
        var dotNet35Random = new DotNet35Random(_birthSeed);
        var pose = PredictPose(85.0);
        Vector3 vector3_1 = Maths.QInvRotateLF(pose.rotation, star.uPosition - (VectorLF3)pose.position * 40000.0);
        vector3_1.Normalize();
        var normalized1 = Vector3.Cross(vector3_1, Vector3.up).normalized;
        var normalized2 = Vector3.Cross(normalized1, vector3_1).normalized;
        var num1 = 0;
        int num2;
        for (num2 = 256; num1 < num2; ++num1)
        {
            var num3 = (float)(dotNet35Random.NextDouble() * 2.0 - 1.0) * 0.5f;
            var num4 = (float)(dotNet35Random.NextDouble() * 2.0 - 1.0) * 0.5f;
            var vector3_2 = vector3_1 + num3 * normalized1 + num4 * normalized2;
            vector3_2.Normalize();
            birthPoint = vector3_2 * (float)(realRadius + 0.20000000298023224 + 1.4500000476837158);
            var vector3_3 = Vector3.Cross(vector3_2, Vector3.up);
            normalized1 = vector3_3.normalized;
            vector3_3 = Vector3.Cross(normalized1, vector3_2);
            normalized2 = vector3_3.normalized;
            var flag = false;
            for (var index = 0; index < 10; ++index)
            {
                var vector2_1 = new Vector2((float)(dotNet35Random.NextDouble() * 2.0 - 1.0),
                    (float)(dotNet35Random.NextDouble() * 2.0 - 1.0)).normalized * 0.1f;
                var vector2_2 = -vector2_1;
                var num5 = (float)(dotNet35Random.NextDouble() * 2.0 - 1.0) * 0.06f;
                var num6 = (float)(dotNet35Random.NextDouble() * 2.0 - 1.0) * 0.06f;
                vector2_2.x += num5;
                vector2_2.y += num6;
                vector3_3 = vector3_2 + vector2_1.x * normalized1 + vector2_1.y * normalized2;
                var normalized3 = vector3_3.normalized;
                vector3_3 = vector3_2 + vector2_2.x * normalized1 + vector2_2.y * normalized2;
                var normalized4 = vector3_3.normalized;
                birthResourcePoint0 = normalized3.normalized;
                birthResourcePoint1 = normalized4.normalized;
                var num7 = realRadius + 0.2f;
                if (rawData.QueryHeight(vector3_2) > (double)num7 && rawData.QueryHeight(normalized3) > (double)num7 &&
                    rawData.QueryHeight(normalized4) > (double)num7)
                {
                    var vpos1 = normalized3 + normalized1 * 0.03f;
                    var vpos2 = normalized3 - normalized1 * 0.03f;
                    var vpos3 = normalized3 + normalized2 * 0.03f;
                    var vpos4 = normalized3 - normalized2 * 0.03f;
                    var vpos5 = normalized4 + normalized1 * 0.03f;
                    var vpos6 = normalized4 - normalized1 * 0.03f;
                    var vpos7 = normalized4 + normalized2 * 0.03f;
                    var vpos8 = normalized4 - normalized2 * 0.03f;
                    if (rawData.QueryHeight(vpos1) > (double)num7 && rawData.QueryHeight(vpos2) > (double)num7 &&
                        rawData.QueryHeight(vpos3) > (double)num7 && rawData.QueryHeight(vpos4) > (double)num7 &&
                        rawData.QueryHeight(vpos5) > (double)num7 && rawData.QueryHeight(vpos6) > (double)num7 &&
                        rawData.QueryHeight(vpos7) > (double)num7 && rawData.QueryHeight(vpos8) > (double)num7)
                    {
                        flag = true;
                        break;
                    }
                }
            }

            if (flag)
                break;
        }

        if (num1 < num2)
            return;
        birthPoint = new Vector3(0.0f, realRadius + 5f, 0.0f);
    }

    public void UpdateRuntimePose(double time)
    {
        var num1 = time / orbitalPeriod + orbitPhase / 360.0;
        var num2 = (int)(num1 + 0.1);
        var num3 = num1 - num2;
        runtimeOrbitPhase = (float)num3 * 360f;
        var num4 = num3 * (2.0 * Math.PI);
        var num5 = time / rotationPeriod + rotationPhase / 360.0;
        var num6 = (int)(num5 + 0.1);
        var angle1 = (num5 - num6) * 360.0;
        runtimeRotationPhase = (float)angle1;
        var vectorLf3_1 = Maths.QRotateLF(runtimeOrbitRotation,
            new VectorLF3((float)Math.Cos(num4) * orbitRadius, 0.0f, (float)Math.Sin(num4) * orbitRadius));
        if (orbitAroundPlanet != null)
        {
            vectorLf3_1.x += orbitAroundPlanet.runtimePosition.x;
            vectorLf3_1.y += orbitAroundPlanet.runtimePosition.y;
            vectorLf3_1.z += orbitAroundPlanet.runtimePosition.z;
        }

        runtimePosition = vectorLf3_1;
        runtimeRotation = runtimeSystemRotation * Quaternion.AngleAxis((float)angle1, Vector3.down);
        uPosition.x = star.uPosition.x + vectorLf3_1.x * 40000.0;
        uPosition.y = star.uPosition.y + vectorLf3_1.y * 40000.0;
        uPosition.z = star.uPosition.z + vectorLf3_1.z * 40000.0;
        runtimeLocalSunDirection = Maths.QInvRotate(runtimeRotation, -vectorLf3_1);
        var num7 = time + 1.0 / 60.0;
        var num8 = num7 / orbitalPeriod + orbitPhase / 360.0;
        var num9 = (int)(num8 + 0.1);
        var num10 = (num8 - num9) * (2.0 * Math.PI);
        var num11 = num7 / rotationPeriod + rotationPhase / 360.0;
        var num12 = (int)(num11 + 0.1);
        var angle2 = (num11 - num12) * 360.0;
        var vectorLf3_2 = Maths.QRotateLF(runtimeOrbitRotation,
            new VectorLF3((float)Math.Cos(num10) * orbitRadius, 0.0f, (float)Math.Sin(num10) * orbitRadius));
        if (orbitAroundPlanet != null)
        {
            vectorLf3_2.x += orbitAroundPlanet.runtimePositionNext.x;
            vectorLf3_2.y += orbitAroundPlanet.runtimePositionNext.y;
            vectorLf3_2.z += orbitAroundPlanet.runtimePositionNext.z;
        }

        runtimePositionNext = vectorLf3_2;
        runtimeRotationNext = runtimeSystemRotation * Quaternion.AngleAxis((float)angle2, Vector3.down);
        uPositionNext.x = star.uPosition.x + vectorLf3_2.x * 40000.0;
        uPositionNext.y = star.uPosition.y + vectorLf3_2.y * 40000.0;
        uPositionNext.z = star.uPosition.z + vectorLf3_2.z * 40000.0;
        var astrosData = new AstroData
        {
            uPos = uPosition,
            uRot = runtimeRotation,
            uPosNext = uPositionNext,
            uRotNext = runtimeRotationNext
        };
        galaxy.astrosData[id] = astrosData;
    }

    public Pose PredictPose(double time)
    {
        var num1 = time / orbitalPeriod + orbitPhase / 360.0;
        var num2 = (int)(num1 + 0.1);
        var num3 = (num1 - num2) * (2.0 * Math.PI);
        var num4 = time / rotationPeriod + rotationPhase / 360.0;
        var num5 = (int)(num4 + 0.1);
        var angle = (num4 - num5) * 360.0;
        var position = Maths.QRotate(runtimeOrbitRotation,
            new Vector3((float)Math.Cos(num3) * orbitRadius, 0.0f, (float)Math.Sin(num3) * orbitRadius));
        if (orbitAroundPlanet != null)
        {
            var pose = orbitAroundPlanet.PredictPose(time);
            position.x += pose.position.x;
            position.y += pose.position.y;
            position.z += pose.position.z;
        }

        return new Pose(position, runtimeSystemRotation * Quaternion.AngleAxis((float)angle, Vector3.down));
    }

    public void PredictUPose(double time, out VectorLF3 uPos, out Quaternion uRot)
    {
        var pose = PredictPose(time);
        uPos.x = pose.position.x * 40000.0 + star.uPosition.x;
        uPos.y = pose.position.y * 40000.0 + star.uPosition.y;
        uPos.z = pose.position.z * 40000.0 + star.uPosition.z;
        uRot = pose.rotation;
    }

    public VectorLF3 GetUniversalVelocityAtLocalPoint(double time, Vector3 lpoint)
    {
        var time1 = time;
        var time2 = time + 1.0 / 60.0;
        VectorLF3 uPos1;
        Quaternion uRot1;
        PredictUPose(time1, out uPos1, out uRot1);
        VectorLF3 uPos2;
        Quaternion uRot2;
        PredictUPose(time2, out uPos2, out uRot2);
        var vectorLf3 = uPos1 + (VectorLF3)(uRot1 * lpoint);
        return (uPos2 + (VectorLF3)(uRot2 * lpoint) - vectorLf3) / (1.0 / 60.0);
    }


    public override string ToString()
    {
        return "Planet " + displayName;
    }
}