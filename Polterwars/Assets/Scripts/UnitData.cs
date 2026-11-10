using UnityEngine;
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
    public TerrainListElement[] coste_por_terreno;

}
