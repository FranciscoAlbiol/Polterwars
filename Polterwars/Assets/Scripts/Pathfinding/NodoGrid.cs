
using UnityEngine;

public class NodoGrid
{
    public Vector2Int Posicion { get; private set; }
    public TerrainType Terreno { get; set; }

    public NodoGrid(Vector2Int posicion, TerrainType terreno)
    {
        Posicion = posicion;
        Terreno = terreno;
    }
}
