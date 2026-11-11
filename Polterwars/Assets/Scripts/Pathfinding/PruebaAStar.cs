
using System.Collections.Generic;
using UnityEngine;

public class PruebaAStar : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Vector2Int posicionInicial;
    [SerializeField] private Vector2Int posicionDestino;
    [SerializeField] private Unidad unidad;

    private GridDatos gridDatos;

    void Start()
    {
        gridDatos = new GridDatos(gridManager);

        List<Vector2Int> camino = AStar.CalcularCamino(
            gridDatos,
            posicionInicial,
            posicionDestino,
            unidad
        );

        if (camino.Count > 0)
        {
            Debug.Log("Camino encontrado:");

            foreach (Vector2Int casilla in camino)
            {
                Debug.Log("Casilla: " + casilla);

                Tile tile = gridManager.GetTile(casilla);

                if (tile != null)
                {
                    tile.SetHighlightColor(Color.blue);
                    tile.SetHighlight(true);
                }
            }
        }
        else
        {
            Debug.Log("No se ha encontrado ning�n camino.");
        }
    }
}
