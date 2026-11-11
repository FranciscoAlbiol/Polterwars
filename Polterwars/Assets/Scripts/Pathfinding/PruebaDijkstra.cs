using UnityEngine;

public class PruebaDijkstra : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Vector2Int posicionInicial;
    [SerializeField] private Unidad unidad;
    [SerializeField] private int puntosMovimiento;

    private GridDatos gridDatos;

    void Start()
    {
        gridDatos = new GridDatos(gridManager);

        RadioAccion radio = Dijkstra.CalcularRadio(
            gridDatos,
            posicionInicial,
            unidad,
            puntosMovimiento
        );

        foreach (var casilla in radio.Costes)
        {
            Tile tile = gridManager.GetTile(casilla.Key);

            if (tile != null)
            {
                tile.SetHighlight(true);
            }

            Debug.Log("Casilla: " + casilla.Key +
                      " | Coste: " + casilla.Value);
        }
    }
}