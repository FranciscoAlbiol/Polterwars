using UnityEngine;
using System.Collections.Generic;
using System;

public static class CostesPM
{

    public static int ObtenerCoste(UnitData unidad_data, TerrainType terreno)
    {
        // 1. Guard against a null dictionary
        if (unidad_data.coste_por_terreno == null)
        {
            Debug.LogError($"El diccionario 'coste_por_terreno' en la unidad {unidad_data.tipo} es NULL.");
            return -1; // Or whatever fallback movement cost makes sense
        }

        // 2. Guard against missing terrain keys using TryGetValue
        if (unidad_data.coste_por_terreno.TryGetValue(terreno, out int coste))
        {
            return coste;
        }
        else
        {
            Debug.LogWarning($"El terreno {terreno} no está definido en el diccionario de costes de la unidad.");
            return -1; // Fallback cost
        }

        /*switch (unidad)
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

        return -1;*/
    }
}
