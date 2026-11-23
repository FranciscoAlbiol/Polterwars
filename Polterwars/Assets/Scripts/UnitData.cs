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

    [Serializable]
    public struct MaterialCost //Para fabricacion
    {
        public Tipos_Material material;
        public int cantidad;
    }


    public UnitType tipo;
    public Sprite imagen;
    public int puntos_movimiento;
    public int alcance; //Para el mapa de influencia + ataque
    public int ataque;
    public int vida_max = 5;

    [Header("Coste de fabricacion")]
    public int coste_almas;
    public MaterialCost[] costes_materiales;

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
