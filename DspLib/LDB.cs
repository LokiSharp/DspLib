using DspLib.Protos;

namespace DspLib;

public static class LDB
{
    private static readonly string protoResDir = "Prototypes/";

    // private static PlayerProtoSet _players;
    private static ThemeProtoSet _themes;

    // private static VegeProtoSet _veges;
    private static VeinProtoSet _veins;

    private static ItemProtoSet _items;

    // private static EnemyProtoSet _enemies;
    // private static FleetProtoSet _fleets;
    // private static ModelProtoSet _models;
    private static RecipeProtoSet _recipes;

    // public static PlayerProtoSet players => LDB.LoadTable<PlayerProtoSet>(ref LDB._players);

    public static ThemeProtoSet themes => LoadTable(ref _themes);

    // public static VegeProtoSet veges => LDB.LoadTable<VegeProtoSet>(ref LDB._veges);

    public static VeinProtoSet veins => LoadTable(ref _veins);

    public static ItemProtoSet items => LoadTable(ref _items);

    // public static EnemyProtoSet enemies => LDB.LoadTable<EnemyProtoSet>(ref LDB._enemies);
    //
    // public static FleetProtoSet fleets => LDB.LoadTable<FleetProtoSet>(ref LDB._fleets);
    //
    // public static ModelProtoSet models => LDB.LoadTable<ModelProtoSet>(ref LDB._models);
    //
    public static RecipeProtoSet recipes => LoadTable(ref _recipes);
    // private static TechProtoSet _techs;
    // private static SignalProtoSet _signals;
    // private static AudioProtoSet _audios;
    // private static MIDIProtoSet _MIDIs;
    // private static EffectEmitterProtoSet _effectEmitters;
    // private static PromptProtoSet _prompts;
    // private static AdvisorTipProtoSet _advisorTips;
    // private static TutorialProtoSet _tutorial;
    // private static AchievementProtoSet _achievements;
    // private static MilestoneProtoSet _milestones;
    // private static JournalPatternProtoSet _journalPatterns;
    // private static CosmicMessageProtoSet _cosmicMessages;
    // private static DoodadProtoSet _doodads;
    // private static AbnormalityProtoSet _abnormalities;

    private static T LoadTable<T>(ref T tmp) where T : ProtoTable
    {
        if (tmp != null)
            return tmp;
        var path = protoResDir + typeof(T).Name + ".dat";
        var reader = new LDBFileReader();
        tmp = reader.ReadFile(path) as T;
        return tmp;
    }
    //
    // public static TechProtoSet techs => LDB.LoadTable<TechProtoSet>(ref LDB._techs);
    //
    // public static SignalProtoSet signals => LDB.LoadTable<SignalProtoSet>(ref LDB._signals);
    //
    // public static AudioProtoSet audios => LDB.LoadTable<AudioProtoSet>(ref LDB._audios);
    //
    // public static MIDIProtoSet MIDIs => LDB.LoadTable<MIDIProtoSet>(ref LDB._MIDIs);
    //
    // public static EffectEmitterProtoSet effectEmitters
    // {
    //   get => LDB.LoadTable<EffectEmitterProtoSet>(ref LDB._effectEmitters);
    // }
    //
    // public static PromptProtoSet prompts => LDB.LoadTable<PromptProtoSet>(ref LDB._prompts);
    //
    // public static AdvisorTipProtoSet advisorTips
    // {
    //   get => LDB.LoadTable<AdvisorTipProtoSet>(ref LDB._advisorTips);
    // }
    //
    // public static TutorialProtoSet tutorial => LDB.LoadTable<TutorialProtoSet>(ref LDB._tutorial);
    //
    // public static AchievementProtoSet achievements
    // {
    //   get => LDB.LoadTable<AchievementProtoSet>(ref LDB._achievements);
    // }
    //
    // public static MilestoneProtoSet milestones
    // {
    //   get => LDB.LoadTable<MilestoneProtoSet>(ref LDB._milestones);
    // }
    //
    // public static JournalPatternProtoSet journalPatterns
    // {
    //   get => LDB.LoadTable<JournalPatternProtoSet>(ref LDB._journalPatterns);
    // }
    //
    // public static CosmicMessageProtoSet cosmicMessages
    // {
    //   get => LDB.LoadTable<CosmicMessageProtoSet>(ref LDB._cosmicMessages);
    // }
    //
    // public static DoodadProtoSet doodads => LDB.LoadTable<DoodadProtoSet>(ref LDB._doodads);
    //
    // public static AbnormalityProtoSet abnormalities
    // {
    //   get => LDB.LoadTable<AbnormalityProtoSet>(ref LDB._abnormalities);
    // }

    public static string ItemName(int itemId)
    {
        var itemProto = items.Select(itemId);
        return itemProto != null ? itemProto.name : "";
    }

    public static string RecipeName(int recipeId)
    {
        var recipeProto = recipes.Select(recipeId);
        return recipeProto != null ? recipeProto.name : "";
    }
}