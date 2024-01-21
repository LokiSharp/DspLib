using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Galaxy;

public static class NameGen
{
    public static string[] con0 = new string[39]
    {
        "p",
        "t",
        "c",
        "k",
        "b",
        "d",
        "g",
        "f",
        "ph",
        "s",
        "sh",
        "th",
        "h",
        "v",
        "z",
        "th",
        "r",
        "ch",
        "tr",
        "dr",
        "m",
        "n",
        "l",
        "y",
        "w",
        "sp",
        "st",
        "sk",
        "sc",
        "sl",
        "pl",
        "cl",
        "bl",
        "gl",
        "fr",
        "fl",
        "pr",
        "br",
        "cr"
    };

    public static string[] con1 = new string[16]
    {
        "thr",
        "ex",
        "ec",
        "el",
        "er",
        "ev",
        "il",
        "is",
        "it",
        "ir",
        "up",
        "ut",
        "ur",
        "un",
        "gt",
        "phr"
    };

    public static string[] vow0 = new string[7]
    {
        "a",
        "an",
        "am",
        "al",
        "o",
        "u",
        "xe"
    };

    public static string[] vow1 = new string[23]
    {
        "ea",
        "ee",
        "ie",
        "i",
        "e",
        "a",
        "er",
        "a",
        "u",
        "oo",
        "u",
        "or",
        "o",
        "oa",
        "ar",
        "a",
        "ei",
        "ai",
        "i",
        "au",
        "ou",
        "ao",
        "ir"
    };

    public static string[] vow2 = new string[7]
    {
        "y",
        "oi",
        "io",
        "iur",
        "ur",
        "ac",
        "ic"
    };

    public static string[] ending = new string[18]
    {
        "er",
        "n",
        "un",
        "or",
        "ar",
        "o",
        "o",
        "ans",
        "us",
        "ix",
        "us",
        "iurs",
        "a",
        "eo",
        "urn",
        "es",
        "eon",
        "y"
    };

    public static string[] roman = new string[21]
    {
        "",
        "I",
        "II",
        "III",
        "IV",
        "V",
        "VI",
        "VII",
        "VIII",
        "IX",
        "X",
        "XI",
        "XII",
        "XIII",
        "XIV",
        "XV",
        "XVI",
        "XVII",
        "XVIII",
        "XIX",
        "XX"
    };

    public static string[] constellations = new string[88]
    {
        "Andromedae",
        "Antliae",
        "Apodis",
        "Aquarii",
        "Aquilae",
        "Arae",
        "Arietis",
        "Aurigae",
        "Bootis",
        "Caeli",
        "Camelopardalis",
        "Cancri",
        "Canum Venaticorum",
        "Canis Majoris",
        "Canis Minoris",
        "Capricorni",
        "Carinae",
        "Cassiopeiae",
        "Centauri",
        "Cephei",
        "Ceti",
        "Chamaeleontis",
        "Circini",
        "Columbae",
        "Comae Berenices",
        "Coronae Australis",
        "Coronae Borealis",
        "Corvi",
        "Crateris",
        "Crucis",
        "Cygni",
        "Delphini",
        "Doradus",
        "Draconis",
        "Equulei",
        "Eridani",
        "Fornacis",
        "Geminorum",
        "Gruis",
        "Herculis",
        "Horologii",
        "Hydrae",
        "Hydri",
        "Indi",
        "Lacertae",
        "Leonis",
        "Leonis Minoris",
        "Leporis",
        "Librae",
        "Lupi",
        "Lyncis",
        "Lyrae",
        "Mensae",
        "Microscopii",
        "Monocerotis",
        "Muscae",
        "Normae",
        "Octantis",
        "Ophiuchii",
        "Orionis",
        "Pavonis",
        "Pegasi",
        "Persei",
        "Phoenicis",
        "Pictoris",
        "Piscium",
        "Piscis Austrini",
        "Puppis",
        "Pyxidis",
        "Reticuli",
        "Sagittae",
        "Sagittarii",
        "Scorpii",
        "Sculptoris",
        "Scuti",
        "Serpentis",
        "Sextantis",
        "Tauri",
        "Telescopii",
        "Trianguli",
        "Trianguli Australis",
        "Tucanae",
        "Ursae Majoris",
        "Ursae Minoris",
        "Velorum",
        "Virginis",
        "Volantis",
        "Vulpeculae"
    };

    public static string[] alphabeta = new string[11]
    {
        "Alpha",
        "Beta",
        "Gamma",
        "Delta",
        "Epsilon",
        "Zeta",
        "Eta",
        "Theta",
        "Iota",
        "Kappa",
        "Lambda"
    };

    public static string[] alphabeta_letter = new string[11]
    {
        "α",
        "β",
        "γ",
        "δ",
        "ε",
        "ζ",
        "η",
        "θ",
        "ι",
        "κ",
        "λ"
    };

    public static string[] raw_star_names = new string[425]
    {
        "Acamar",
        "Achernar",
        "Achird",
        "Acrab",
        "Acrux",
        "Acubens",
        "Adhafera",
        "Adhara",
        "Adhil",
        "Agena",
        "Aladfar",
        "Albaldah",
        "Albali",
        "Albireo",
        "Alchiba",
        "Alcor",
        "Alcyone",
        "Alderamin",
        "Aldhibain",
        "Aldib",
        "Alfecca",
        "Alfirk",
        "Algedi",
        "Algenib",
        "Algenubi",
        "Algieba",
        "Algjebbath",
        "Algol",
        "Algomeyla",
        "Algorab",
        "Alhajoth",
        "Alhena",
        "Alifa",
        "Alioth",
        "Alkaid",
        "Alkalurops",
        "Alkaphrah",
        "Alkes",
        "Alkhiba",
        "Almach",
        "Almeisan",
        "Almuredin",
        "AlNa'ir",
        "Alnasl",
        "Alnilam",
        "Alnitak",
        "Alniyat",
        "Alphard",
        "Alphecca",
        "Alpheratz",
        "Alrakis",
        "Alrami",
        "Alrescha",
        "AlRijil",
        "Alsahm",
        "Alsciaukat",
        "Alshain",
        "Alshat",
        "Alshemali",
        "Alsuhail",
        "Altair",
        "Altais",
        "Alterf",
        "Althalimain",
        "AlTinnin",
        "Aludra",
        "AlulaAustralis",
        "AlulaBorealis",
        "Alwaid",
        "Alwazn",
        "Alya",
        "Alzirr",
        "AmazonStar",
        "Ancha",
        "Anchat",
        "AngelStern",
        "Angetenar",
        "Ankaa",
        "Anser",
        "Antecanis",
        "Apollo",
        "Arich",
        "Arided",
        "Arietis",
        "Arkab",
        "ArkebPrior",
        "Arneb",
        "Arrioph",
        "AsadAustralis",
        "Ascella",
        "Aschere",
        "AsellusAustralis",
        "AsellusBorealis",
        "AsellusPrimus",
        "Ashtaroth",
        "Asmidiske",
        "Aspidiske",
        "Asterion",
        "Asterope",
        "Asuia",
        "Athafiyy",
        "Atik",
        "Atlas",
        "Atria",
        "Auva",
        "Avior",
        "Azelfafage",
        "Azha",
        "Azimech",
        "BatenKaitos",
        "Becrux",
        "Beid",
        "Bellatrix",
        "Benatnasch",
        "Biham",
        "Botein",
        "Brachium",
        "Bunda",
        "Cajam",
        "Calbalakrab",
        "Calx",
        "Canicula",
        "Capella",
        "Caph",
        "Castor",
        "Castula",
        "Cebalrai",
        "Ceginus",
        "Celaeno",
        "Chara",
        "Chertan",
        "Choo",
        "Clava",
        "CorCaroli",
        "CorHydrae",
        "CorLeonis",
        "Cornu",
        "CorScorpii",
        "CorSepentis",
        "CorTauri",
        "Coxa",
        "Cursa",
        "Cymbae",
        "Cynosaura",
        "Dabih",
        "DenebAlgedi",
        "DenebDulfim",
        "DenebelOkab",
        "DenebKaitos",
        "DenebOkab",
        "Denebola",
        "Dhalim",
        "Dhur",
        "Diadem",
        "Difda",
        "DifdaalAuwel",
        "Dnoces",
        "Dubhe",
        "Dziban",
        "Dzuba",
        "Edasich",
        "ElAcola",
        "Elacrab",
        "Electra",
        "Elgebar",
        "Elgomaisa",
        "ElKaprah",
        "ElKaridab",
        "Elkeid",
        "ElKhereb",
        "Elmathalleth",
        "Elnath",
        "ElPhekrah",
        "Eltanin",
        "Enif",
        "Erakis",
        "Errai",
        "FalxItalica",
        "Fidis",
        "Fomalhaut",
        "Fornacis",
        "FumAlSamakah",
        "Furud",
        "Gacrux",
        "Gallina",
        "GarnetStar",
        "Gemma",
        "Genam",
        "Giausar",
        "GiedePrime",
        "Giedi",
        "Gienah",
        "Gienar",
        "Gildun",
        "Girtab",
        "Gnosia",
        "Gomeisa",
        "Gorgona",
        "Graffias",
        "Hadar",
        "Hamal",
        "Haris",
        "Hasseleh",
        "Hastorang",
        "Hatysa",
        "Heka",
        "Hercules",
        "Heze",
        "Hoedus",
        "Homam",
        "HyadumPrimus",
        "Icalurus",
        "Iclarkrav",
        "Izar",
        "Jabbah",
        "Jewel",
        "Jugum",
        "Juza",
        "Kabeleced",
        "Kaff",
        "Kaffa",
        "Kaffaljidma",
        "Kaitain",
        "KalbalAkrab",
        "Kat",
        "KausAustralis",
        "KausBorealis",
        "KausMedia",
        "Keid",
        "KeKouan",
        "Kelb",
        "Kerb",
        "Kerbel",
        "KiffaBoraelis",
        "Kitalpha",
        "Kochab",
        "Kornephoros",
        "Kraz",
        "Ksora",
        "Kuma",
        "Kurhah",
        "Kursa",
        "Lesath",
        "Maasym",
        "Maaz",
        "Mabsuthat",
        "Maia",
        "Marfik",
        "Markab",
        "Marrha",
        "Matar",
        "Mebsuta",
        "Megres",
        "Meissa",
        "Mekbuda",
        "Menkalinan",
        "Menkar",
        "Menkent",
        "Menkib",
        "Merak",
        "Meres",
        "Merga",
        "Meridiana",
        "Merope",
        "Mesartim",
        "Metallah",
        "Miaplacidus",
        "Mimosa",
        "Minelauva",
        "Minkar",
        "Mintaka",
        "Mirac",
        "Mirach",
        "Miram",
        "Mirfak",
        "Mirzam",
        "Misam",
        "Mismar",
        "Mizar",
        "Muhlifain",
        "Muliphein",
        "Muphrid",
        "Muscida",
        "NairalSaif",
        "NairalZaurak",
        "Naos",
        "Nash",
        "Nashira",
        "Navi",
        "Nekkar",
        "Nicolaus",
        "Nihal",
        "Nodus",
        "Nunki",
        "Nusakan",
        "OculusBoreus",
        "Okda",
        "Osiris",
        "OsPegasi",
        "Palilicium",
        "Peacock",
        "Phact",
        "Phecda",
        "Pherkad",
        "PherkadMinor",
        "Pherkard",
        "Phoenice",
        "Phurad",
        "Pishpai",
        "Pleione",
        "Polaris",
        "Pollux",
        "Porrima",
        "Postvarta",
        "Praecipua",
        "Procyon",
        "Propus",
        "Protrygetor",
        "Pulcherrima",
        "Rana",
        "RanaSecunda",
        "Rasalas",
        "Rasalgethi",
        "Rasalhague",
        "Rasalmothallah",
        "RasHammel",
        "Rastaban",
        "Reda",
        "Regor",
        "Regulus",
        "Rescha",
        "RigilKentaurus",
        "RiglalAwwa",
        "Rotanen",
        "Ruchba",
        "Ruchbah",
        "Rukbat",
        "Rutilicus",
        "Saak",
        "Sabik",
        "Sadachbia",
        "Sadalbari",
        "Sadalmelik",
        "Sadalsuud",
        "Sadatoni",
        "Sadira",
        "Sadr",
        "Saidak",
        "Saiph",
        "Salm",
        "Sargas",
        "Sarin",
        "Sartan",
        "Sceptrum",
        "Scheat",
        "Schedar",
        "Scheddi",
        "Schemali",
        "Scutulum",
        "SeatAlpheras",
        "Segin",
        "Seginus",
        "Shaula",
        "Shedir",
        "Sheliak",
        "Sheratan",
        "Singer",
        "Sirius",
        "Sirrah",
        "Situla",
        "Skat",
        "Spica",
        "Sterope",
        "Subra",
        "Suha",
        "Suhail",
        "SuhailHadar",
        "SuhailRadar",
        "Suhel",
        "Sulafat",
        "Superba",
        "Svalocin",
        "Syrma",
        "Tabit",
        "Tais",
        "Talitha",
        "TaniaAustralis",
        "TaniaBorealis",
        "Tarazed",
        "Tarf",
        "TaTsun",
        "Taygeta",
        "Tegmen",
        "Tejat",
        "TejatPrior",
        "Terebellum",
        "Theemim",
        "Thuban",
        "Tolimann",
        "Tramontana",
        "Tsih",
        "Tureis",
        "Unukalhai",
        "Vega",
        "Venabulum",
        "Venator",
        "Vendemiatrix",
        "Vespertilio",
        "Vildiur",
        "Vindemiatrix",
        "Wasat",
        "Wazn",
        "YedPosterior",
        "YedPrior",
        "Zaniah",
        "Zaurak",
        "Zavijava",
        "ZenithStar",
        "Zibel",
        "Zosma",
        "Zubenelakrab",
        "ZubenElgenubi",
        "Zubeneschamali",
        "ZubenHakrabi",
        "Zubra"
    };

    public static string[] raw_giant_names = new string[60]
    {
        "AH Scorpii",
        "Aldebaran",
        "Alpha Herculis",
        "Antares",
        "Arcturus",
        "AV Persei",
        "BC Cygni",
        "Betelgeuse",
        "BI Cygni",
        "BO Carinae",
        "Canopus",
        "CE Tauri",
        "CK Carinae",
        "CW Leonis",
        "Deneb",
        "Epsilon Aurigae",
        "Eta Carinae",
        "EV Carinae",
        "IX Carinae",
        "KW Sagittarii",
        "KY Cygni",
        "Mira",
        "Mu Cephei",
        "NML Cygni",
        "NR Vulpeculae",
        "PZ Cassiopeiae",
        "R Doradus",
        "R Leporis",
        "Rho Cassiopeiae",
        "Rigel",
        "RS Persei",
        "RT Carinae",
        "RU Virginis",
        "RW Cephei",
        "S Cassiopeiae",
        "S Cephei",
        "S Doradus",
        "S Persei",
        "SU Persei",
        "TV Geminorum",
        "U Lacertae",
        "UY Scuti",
        "V1185 Scorpii",
        "V354 Cephei",
        "V355 Cepheus",
        "V382 Carinae",
        "V396 Centauri",
        "V437 Scuti",
        "V509 Cassiopeiae",
        "V528 Carinae",
        "V602 Carinae",
        "V648 Cassiopeiae",
        "V669 Cassiopeiae",
        "V838 Monocerotis",
        "V915 Scorpii",
        "VV Cephei",
        "VX Sagittarii",
        "VY Canis Majoris",
        "WOH G64",
        "XX Persei"
    };

    public static string[] giant_name_formats = new string[7]
    {
        "HD {0:0000}{1:00}",
        "HDE {0:0000}{1:00}",
        "HR {0:0000}",
        "HV {0:0000}",
        "LBV {0:0000}-{1:00}",
        "NSV {0:0000}",
        "YSC {0:0000}-{1:00}"
    };

    public static string[] neutron_star_name_formats = new string[2]
    {
        "NTR J{0:00}{1:00}+{2:00}",
        "NTR J{0:00}{1:00}-{2:00}"
    };

    public static string[] black_hole_name_formats = new string[2]
    {
        "DSR J{0:00}{1:00}+{2:00}",
        "DSR J{0:00}{1:00}-{2:00}"
    };

    public static string RandomName(int seed)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num = (int)(dotNet35Random.NextDouble() * 1.8 + 2.3);
        var str1 = "";
        for (var index = 0; index < num; ++index)
            if (dotNet35Random.NextDouble() < 0.05000000074505806 && index == 0)
            {
                str1 += vow0[dotNet35Random.Next(vow0.Length)];
            }
            else
            {
                var str2 = dotNet35Random.NextDouble() < 0.97000002861022949 || num >= 4
                    ? str1 + con0[dotNet35Random.Next(con0.Length)]
                    : str1 + con1[dotNet35Random.Next(con1.Length)];
                str1 = index != num - 1 || dotNet35Random.NextDouble() >= 0.89999997615814209
                    ? dotNet35Random.NextDouble() >= 0.97000002861022949
                        ? str2 + vow2[dotNet35Random.Next(vow2.Length)]
                        : str2 + vow1[dotNet35Random.Next(vow1.Length)]
                    : str2 + ending[dotNet35Random.Next(ending.Length)];
            }

        if (str1.IndexOf("uu") >= 0)
            str1 = str1.Replace("uu", "u");
        if (str1.IndexOf("ooo") >= 0)
            str1 = str1.Replace("ooo", "oo");
        if (str1.IndexOf("eee") >= 0)
            str1 = str1.Replace("eee", "ee");
        if (str1.IndexOf("eea") >= 0)
            str1 = str1.Replace("eea", "ea");
        if (str1.IndexOf("aa") >= 0)
            str1 = str1.Replace("aa", "a");
        if (str1.IndexOf("yy") >= 0)
            str1 = str1.Replace("yy", "y");
        return str1.Substring(0, 1).ToUpper() + str1.Substring(1);
    }

    public static string RandomStarName(int seed, StarData starData, GalaxyData galaxy)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num = 0;
        while (num++ < 256)
        {
            var str = _RandomStarName(dotNet35Random.Next(), starData);
            var flag = false;
            for (var index = 0; index < galaxy.starCount; ++index)
                if (galaxy.stars[index] != null && galaxy.stars[index].name.Equals(str))
                {
                    flag = true;
                    break;
                }

            if (!flag)
                return str;
        }

        return "XStar";
    }

    private static string _RandomStarName(int seed, StarData starData)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var seed1 = dotNet35Random.Next();
        var num1 = dotNet35Random.NextDouble();
        var num2 = dotNet35Random.NextDouble();
        if (starData.type == EStarType.GiantStar)
        {
            if (num2 < 0.40000000596046448)
                return RandomGiantStarNameFromRawNames(seed1);
            return num2 < 0.699999988079071
                ? RandomGiantStarNameWithConstellationAlpha(seed1)
                : RandomGiantStarNameWithFormat(seed1);
        }

        if (starData.type == EStarType.NeutronStar)
            return RandomNeutronStarNameWithFormat(seed1);
        if (starData.type == EStarType.BlackHole)
            return RandomBlackHoleNameWithFormat(seed1);
        if (num1 < 0.60000002384185791)
            return RandomStarNameFromRawNames(seed1);
        return num1 < 0.93000000715255737
            ? RandomStarNameWithConstellationAlpha(seed1)
            : RandomStarNameWithConstellationNumber(seed1);
    }

    public static string RandomStarNameFromRawNames(int seed)
    {
        var index = new DotNet35Random(seed).Next() % raw_star_names.Length;
        return raw_star_names[index];
    }

    public static string RandomStarNameWithConstellationAlpha(int seed)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num1 = dotNet35Random.Next();
        var num2 = dotNet35Random.Next();
        var index1 = num1 % constellations.Length;
        var index2 = num2 % alphabeta.Length;
        var constellation = constellations[index1];
        return constellation.Length > 10
            ? alphabeta_letter[index2] + " " + constellation
            : alphabeta[index2] + " " + constellation;
    }

    public static string RandomStarNameWithConstellationNumber(int seed)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num1 = dotNet35Random.Next();
        var num2 = dotNet35Random.Next(27, 75);
        var index = num1 % constellations.Length;
        return num2 + " " + constellations[index];
    }

    public static string RandomGiantStarNameFromRawNames(int seed)
    {
        var index = new DotNet35Random(seed).Next() % raw_giant_names.Length;
        return raw_giant_names[index];
    }

    public static string RandomGiantStarNameWithConstellationAlpha(int seed)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num1 = dotNet35Random.Next();
        var num2 = dotNet35Random.Next(15, 26);
        var num3 = dotNet35Random.Next(0, 26);
        var index = num1 % constellations.Length;
        return (ushort)(65 + num2) + (char)(65 + num3) + " " + constellations[index];
    }

    public static string RandomGiantStarNameWithFormat(int seed)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num1 = dotNet35Random.Next();
        var num2 = dotNet35Random.Next(10000);
        var num3 = dotNet35Random.Next(100);
        var index = num1 % giant_name_formats.Length;
        return string.Format(giant_name_formats[index], num2, num3);
    }

    public static string RandomNeutronStarNameWithFormat(int seed)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num1 = dotNet35Random.Next();
        var num2 = dotNet35Random.Next(24);
        var num3 = dotNet35Random.Next(60);
        var num4 = dotNet35Random.Next(0, 60);
        var index = num1 % neutron_star_name_formats.Length;
        return string.Format(neutron_star_name_formats[index], num2, num3, num4);
    }

    public static string RandomBlackHoleNameWithFormat(int seed)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num1 = dotNet35Random.Next();
        var num2 = dotNet35Random.Next(24);
        var num3 = dotNet35Random.Next(60);
        var num4 = dotNet35Random.Next(0, 60);
        var index = num1 % black_hole_name_formats.Length;
        return string.Format(black_hole_name_formats[index], num2, num3, num4);
    }
}