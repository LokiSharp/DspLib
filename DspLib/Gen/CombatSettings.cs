using DspLib.Enum;

namespace DspLib.Gen;

public class CombatSettings
{
    public const float AGG_VALUE_0_DUMMY = -1f;
    public const float AGG_VALUE_1_PASSIVE = 0.0f;
    public const float AGG_VALUE_2_TORPID = 0.5f;
    public const float AGG_VALUE_3_NORMAL = 1f;
    public const float AGG_VALUE_4_SHARP = 2f;
    public const float AGG_VALUE_5_RAMPAGE = 3f;
    public const float INITIALLEVEL_0 = 0.0f;
    public const float INITIALLEVEL_1 = 1f;
    public const float INITIALLEVEL_2 = 2f;
    public const float INITIALLEVEL_3 = 3f;
    public const float INITIALLEVEL_4 = 4f;
    public const float INITIALLEVEL_5 = 5f;
    public const float INITIALLEVEL_6 = 6f;
    public const float INITIALLEVEL_7 = 7f;
    public const float INITIALLEVEL_8 = 8f;
    public const float INITIALLEVEL_9 = 9f;
    public const float INITIALLEVEL_10 = 10f;
    public const float INITIAL_GROWTH_LEVEL_0 = 0.0f;
    public const float INITIAL_GROWTH_LEVEL_1 = 0.25f;
    public const float INITIAL_GROWTH_LEVEL_2 = 0.5f;
    public const float INITIAL_GROWTH_LEVEL_3 = 0.75f;
    public const float INITIAL_GROWTH_LEVEL_4 = 1f;
    public const float INITIAL_GROWTH_LEVEL_5 = 1.5f;
    public const float INITIAL_GROWTH_LEVEL_6 = 2f;
    public const float INITIAL_EXPAND_LEVEL_0 = 0.01f;
    public const float INITIAL_EXPAND_LEVEL_1 = 0.25f;
    public const float INITIAL_EXPAND_LEVEL_2 = 0.5f;
    public const float INITIAL_EXPAND_LEVEL_3 = 0.75f;
    public const float INITIAL_EXPAND_LEVEL_4 = 1f;
    public const float INITIAL_EXPAND_LEVEL_5 = 1.5f;
    public const float INITIAL_EXPAND_LEVEL_6 = 2f;
    public const float MAX_DENSITY_LEVEL_0 = 1f;
    public const float MAX_DENSITY_LEVEL_1 = 1.5f;
    public const float MAX_DENSITY_LEVEL_2 = 2f;
    public const float MAX_DENSITY_LEVEL_3 = 2.5f;
    public const float MAX_DENSITY_LEVEL_4 = 3f;
    public const float GROWTH_SPEED_0 = 0.25f;
    public const float GROWTH_SPEED_1 = 0.5f;
    public const float GROWTH_SPEED_2 = 1f;
    public const float GROWTH_SPEED_3 = 2f;
    public const float GROWTH_SPEED_4 = 3f;
    public const float POWER_THREAT_LEVEL_0 = 0.01f;
    public const float POWER_THREAT_LEVEL_1 = 0.1f;
    public const float POWER_THREAT_LEVEL_2 = 0.2f;
    public const float POWER_THREAT_LEVEL_3 = 0.5f;
    public const float POWER_THREAT_LEVEL_4 = 1f;
    public const float POWER_THREAT_LEVEL_5 = 2f;
    public const float POWER_THREAT_LEVEL_6 = 5f;
    public const float POWER_THREAT_LEVEL_7 = 8f;
    public const float POWER_THREAT_LEVEL_8 = 10f;
    public const float COMBAT_THREAT_LEVEL_0 = 0.01f;
    public const float COMBAT_THREAT_LEVEL_1 = 0.1f;
    public const float COMBAT_THREAT_LEVEL_2 = 0.2f;
    public const float COMBAT_THREAT_LEVEL_3 = 0.5f;
    public const float COMBAT_THREAT_LEVEL_4 = 1f;
    public const float COMBAT_THREAT_LEVEL_5 = 2f;
    public const float COMBAT_THREAT_LEVEL_6 = 5f;
    public const float COMBAT_THREAT_LEVEL_7 = 8f;
    public const float COMBAT_THREAT_LEVEL_8 = 10f;
    public const float COMBAT_EXP_MULTIPLIER_0 = 0.01f;
    public const float COMBAT_EXP_MULTIPLIER_1 = 0.1f;
    public const float COMBAT_EXP_MULTIPLIER_2 = 0.2f;
    public const float COMBAT_EXP_MULTIPLIER_3 = 0.5f;
    public const float COMBAT_EXP_MULTIPLIER_4 = 1f;
    public const float COMBAT_EXP_MULTIPLIER_5 = 2f;
    public const float COMBAT_EXP_MULTIPLIER_6 = 5f;
    public const float COMBAT_EXP_MULTIPLIER_7 = 8f;
    public const float COMBAT_EXP_MULTIPLIER_8 = 10f;
    public float aggressiveness;
    public float battleExpFactor;
    public float battleThreatFactor;
    public float growthSpeedFactor;
    public float initialColonize;
    public float initialGrowth;
    public float initialLevel;
    public float maxDensity;
    public float powerThreatFactor;

    public EAggressiveLevel aggressiveLevel => (EAggressiveLevel)((aggressiveness + 1.0) * 10.0 + 0.5);

    public float aggressiveHatredCoef
    {
        get
        {
            switch ((EAggressiveLevel)((aggressiveness + 1.0) * 10.0 + 0.5))
            {
                case EAggressiveLevel.Passive:
                case EAggressiveLevel.Torpid:
                    return 0.6f;
                case EAggressiveLevel.Normal:
                    return 1f;
                case EAggressiveLevel.Sharp:
                    return 4f;
                case EAggressiveLevel.Rampage:
                    return 6f;
                default:
                    return 0.0f;
            }
        }
    }

    public bool isEnemyPassive => aggressiveness <= 0.0;

    public bool isEnemyHostile => aggressiveness >= 0.0;

    public float difficulty
    {
        get
        {
            var num1 = aggressiveness >= -0.10000000149011612
                ? aggressiveness >= 0.25
                    ? aggressiveness >= 0.75
                        ? aggressiveness >= 1.5 ? aggressiveness >= 2.5 ? 1.125f : 0.875f : 0.75f
                        : 0.5f
                    : 0.0f
                : -0.2f;
            var num2 = initialLevel * 0.8f;
            var num3 = initialGrowth >= 0.15000000596046448
                ? initialGrowth >= 0.30000001192092896
                    ? initialGrowth >= 0.64999997615814209
                        ? initialGrowth >= 0.800000011920929
                            ? initialGrowth >= 1.1499999761581421
                                ? initialGrowth >= 1.6499999761581421 ? 1.5f : 1.25f
                                : 1f
                            : 0.75f
                        : 0.5f
                    : 0.25f
                : 0.0f;
            var num4 = initialColonize >= 0.15000000596046448
                ? initialColonize >= 0.30000001192092896
                    ? initialColonize >= 0.64999997615814209
                        ? initialColonize >= 0.800000011920929
                            ? initialColonize >= 1.1499999761581421
                                ? initialColonize >= 1.6499999761581421 ? 1.5f : 1.25f
                                : 1f
                            : 0.75f
                        : 0.5f
                    : 0.25f
                : 0.0f;
            var num5 = maxDensity - 1f;
            var num6 = growthSpeedFactor >= 0.34999999403953552
                ? growthSpeedFactor >= 0.75
                    ? growthSpeedFactor >= 1.5 ? growthSpeedFactor >= 2.5 ? 1.5f : 1.2f : 1f
                    : 0.7f
                : 0.3f;
            var num7 = powerThreatFactor >= 0.05000000074505806
                ? powerThreatFactor >= 0.15000000596046448
                    ? powerThreatFactor >= 0.25
                        ? powerThreatFactor >= 0.550000011920929
                            ? powerThreatFactor >= 1.1499999761581421
                                ? powerThreatFactor >= 2.1500000953674316
                                    ? powerThreatFactor >= 5.1500000953674316
                                        ? powerThreatFactor >= 8.1499996185302734 ? 2f : 1.8f
                                        : 1.5f
                                    : 1.2f
                                : 1f
                            : 0.8f
                        : 0.6f
                    : 0.3f
                : 0.125f;
            var num8 = battleThreatFactor >= 0.05000000074505806
                ? battleThreatFactor >= 0.15000000596046448
                    ? battleThreatFactor >= 0.25
                        ? battleThreatFactor >= 0.550000011920929
                            ? battleThreatFactor >= 1.1499999761581421
                                ? battleThreatFactor >= 2.1500000953674316
                                    ? battleThreatFactor >= 5.1500000953674316
                                        ? battleThreatFactor >= 8.1499996185302734 ? 2f : 1.8f
                                        : 1.5f
                                    : 1.2f
                                : 1f
                            : 0.8f
                        : 0.6f
                    : 0.3f
                : 0.125f;
            var num9 = battleExpFactor >= 0.05000000074505806
                ? battleExpFactor >= 0.15000000596046448
                    ? battleExpFactor >= 0.25
                        ? battleExpFactor >= 0.550000011920929
                            ? battleExpFactor >= 1.1499999761581421
                                ? battleExpFactor >= 2.1500000953674316
                                    ? battleExpFactor >= 5.1500000953674316
                                        ? battleExpFactor >= 8.1499996185302734 ? 18f : 16f
                                        : 14f
                                    : 12f
                                : 10f
                            : 6f
                        : 3f
                    : 1f
                : 0.0f;
            var num10 = num1 < 0.0 ? 0.0 : 0.25 + num1 * (num7 * 0.5 + num8 * 0.5);
            var num11 = (float)(0.375 + 0.625 * ((num2 + (double)num9) / 10.0));
            var num12 = (float)(0.375 + 0.625 *
                ((num4 * 0.60000002384185791 + num3 * 0.40000000596046448 * (num4 * 0.75 + 0.25)) *
                 0.60000002384185791 +
                 num6 * 0.40000000596046448 * (num4 * 0.800000011920929 + 0.20000000298023224) +
                 num5 * 0.28999999165534973 * (num4 * 0.5 + 0.5)));
            double num13 = num11;
            return (int)(num10 * num13 * num12 * 10000.0 + 0.5) / 10000f;
        }
    }

    public void SetDefault()
    {
        aggressiveness = 1f;
        initialLevel = 0.0f;
        initialGrowth = 1f;
        initialColonize = 1f;
        maxDensity = 1f;
        growthSpeedFactor = 1f;
        powerThreatFactor = 1f;
        battleThreatFactor = 1f;
        battleExpFactor = 1f;
    }

    public void Export(BinaryWriter w)
    {
        w.Write(0);
        w.Write(aggressiveness);
        w.Write(initialLevel);
        w.Write(initialGrowth);
        w.Write(initialColonize);
        w.Write(maxDensity);
        w.Write(growthSpeedFactor);
        w.Write(powerThreatFactor);
        w.Write(battleThreatFactor);
        w.Write(battleExpFactor);
    }

    public void Import(BinaryReader r)
    {
        r.ReadInt32();
        aggressiveness = r.ReadSingle();
        initialLevel = r.ReadSingle();
        initialGrowth = r.ReadSingle();
        initialColonize = r.ReadSingle();
        maxDensity = r.ReadSingle();
        growthSpeedFactor = r.ReadSingle();
        powerThreatFactor = r.ReadSingle();
        battleThreatFactor = r.ReadSingle();
        battleExpFactor = r.ReadSingle();
    }

    public static bool operator ==(CombatSettings lhs, CombatSettings rhs)
    {
        return lhs.aggressiveness == (double)rhs.aggressiveness && lhs.initialLevel == (double)rhs.initialLevel &&
               lhs.initialGrowth == (double)rhs.initialGrowth && lhs.initialColonize == (double)rhs.initialColonize &&
               lhs.maxDensity == (double)rhs.maxDensity && lhs.growthSpeedFactor == (double)rhs.growthSpeedFactor &&
               lhs.powerThreatFactor == (double)rhs.powerThreatFactor &&
               lhs.battleThreatFactor == (double)rhs.battleThreatFactor &&
               lhs.battleExpFactor == (double)rhs.battleExpFactor;
    }

    public static bool operator !=(CombatSettings lhs, CombatSettings rhs)
    {
        return lhs.aggressiveness != (double)rhs.aggressiveness || lhs.initialLevel != (double)rhs.initialLevel ||
               lhs.initialGrowth != (double)rhs.initialGrowth || lhs.initialColonize != (double)rhs.initialColonize ||
               lhs.maxDensity != (double)rhs.maxDensity || lhs.growthSpeedFactor != (double)rhs.growthSpeedFactor ||
               lhs.powerThreatFactor != (double)rhs.powerThreatFactor ||
               lhs.battleThreatFactor != (double)rhs.battleThreatFactor ||
               lhs.battleExpFactor != (double)rhs.battleExpFactor;
    }

    public override bool Equals(object obj)
    {
        return obj is CombatSettings combatSettings && this == combatSettings;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}