using DspLib.Galaxy;

namespace DspLib;

public static class StarsCompute
{
    public static void Compute(GameDesc gameDesc, out GalaxyData galaxyData)
    {
        var galaxy = UniverseGen.CreateGalaxy(gameDesc);
        galaxyData = galaxy;
        if (galaxy.stars.Length > 2)
            Parallel.ForEach(galaxy.stars, (Action<StarData>)(star =>
            {
                foreach (var planet in star.planets)
                    PlanetCompute(galaxy, star, planet);
            }));
        else
            foreach (var star in galaxy.stars)
            {
                Parallel.ForEach(star.planets,
                    (Action<PlanetData>)(planetData => PlanetCompute(galaxy, star, planetData)));
            }
    }

    public static void ComputeWithoutPlanetData(GameDesc gameDesc, out GalaxyData galaxyData)
    {
        var galaxy = UniverseGen.CreateGalaxy(gameDesc);
        galaxyData = galaxy;
    }

    public static void PlanetCompute(GalaxyData galaxyData, StarData star, PlanetData planetData)
    {
        var planetAlgorithm = PlanetModelingManager.Algorithm(planetData);
        planetData.data = new PlanetRawData(planetData.precision);
        planetData.data.CalcVerts();
        planetAlgorithm.GenerateTerrain(planetData.mod_x, planetData.mod_y);
        planetAlgorithm.planet.star = star;
        planetAlgorithm.planet.galaxy = galaxyData;
    }
}