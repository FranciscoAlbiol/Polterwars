
public static class CostesPM
{

    public static int ObtenerCoste(UnitType unidad, TerrainType terreno)
    {
        switch (unidad)
        {
            case UnitType.Peon:
            case UnitType.Explorador:
            case UnitType.Mono:
            case UnitType.Vudu:
                switch (terreno)
                {
                    case TerrainType.Suelo: return 1;
                    case TerrainType.Alfombra: return 2;
                    case TerrainType.Gotera: return 3;
                    case TerrainType.Caja: return -1;
                }
                break;

            case UnitType.Barquito:
                switch (terreno)
                {
                    case TerrainType.Gotera: return 1;
                    default: return -1;
                }

            case UnitType.Aranya:
                switch (terreno)
                {
                    case TerrainType.Suelo: return 2;
                    case TerrainType.Alfombra: return 1;
                    case TerrainType.Gotera: return 3;
                    case TerrainType.Caja: return 2;
                }
                break;

            case UnitType.Bailarina:
                switch (terreno)
                {
                    case TerrainType.Suelo:
                    case TerrainType.Alfombra:
                    case TerrainType.Gotera:
                        return 1;
                    case TerrainType.Caja:
                        return -1;
                }
                break;

            case UnitType.Oso:
                switch (terreno)
                {
                    case TerrainType.Suelo: return 1;
                    case TerrainType.Alfombra: return 1;
                    case TerrainType.Gotera: return 3;
                    case TerrainType.Caja: return -1;
                }
                break;
        }

        return -1;
    }
}
