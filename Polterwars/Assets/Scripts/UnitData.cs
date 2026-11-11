using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "UnitData", menuName = "Stats de unidad")]
public class UnitData : ScriptableObject
{
    [Serializable]
    public struct TerrainListElement {
        public TerrainType tipo;
        public int cost;
    }


    public UnitType tipo;
    public Sprite imagen;
    public int puntos_movimiento;
    public TerrainListElement[] coste_por_terreno_array;

    private Dictionary<TerrainType, int> coste_por_terreno_cache;
    public Dictionary<TerrainType, int> coste_por_terreno
    {
        get
        {
            if (coste_por_terreno_cache == null)
            {
                coste_por_terreno_cache = new Dictionary<TerrainType, int>();
                if (coste_por_terreno_array != null)
                {
                    foreach (var terrain in coste_por_terreno_array)
                    {
                        if (!coste_por_terreno_cache.ContainsKey(terrain.tipo))
                        {
                            coste_por_terreno_cache.Add(terrain.tipo, terrain.cost);
                        }
                    }
                }
            }
            return coste_por_terreno_cache;
        }
    }

}
