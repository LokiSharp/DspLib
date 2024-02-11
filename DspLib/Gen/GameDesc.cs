namespace DspLib.Gen;

public class GameDesc
{
    public CombatSettings combatSettings;
    public Version creationVersion;
    public int galaxySeed;

    public float resourceMultiplier;
    public int[] savedThemeIds;
    public int starCount;
    public bool isRareResource => resourceMultiplier <= 0.10010000318288803;

    public void SetForNewGame(int _galaxySeed, int _starCount)
    {
        combatSettings = new CombatSettings();
        galaxySeed = _galaxySeed;
        starCount = _starCount;
        var themes = LDB.themes;
        var length = themes.Length;
        savedThemeIds = new int[length];
        for (var index = 0; index < length; ++index)
            savedThemeIds[index] = themes.dataArray[index].ID;
        combatSettings.SetDefault();
    }
}