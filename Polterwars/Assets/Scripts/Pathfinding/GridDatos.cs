using System.Collections.Generic;
using UnityEngine;

public class GridDatos
{
    private GridManager gridManager;

    public GridDatos(GridManager gridManager)
    {
        this.gridManager = gridManager;
    }

    public NodoGrid ObtenerNodo(Vector2Int posicion)
    {
        Tile tile = gridManager.GetTile(posicion);

        if (tile == null)
            return null;

        return new NodoGrid(posicion, tile.terrainType);
    }

    public bool EstaDentroDelGrid(Vector2Int posicion)
    {
        return gridManager.GetTile(posicion) != null;
    }

    public List<NodoGrid> ObtenerVecinos(Vector2Int posicion)
    {
        List<NodoGrid> vecinos = new List<NodoGrid>();

        Vector2Int[] direcciones =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        foreach (Vector2Int direccion in direcciones)
        {
            Vector2Int posicionVecina = posicion + direccion;

            if (EstaDentroDelGrid(posicionVecina))
            {
                vecinos.Add(ObtenerNodo(posicionVecina));
            }
        }

        return vecinos;
    }
}