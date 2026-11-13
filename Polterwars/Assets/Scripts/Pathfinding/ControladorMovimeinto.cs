using System;
using System.Collections.Generic;
using UnityEngine;

public class ControladorMovimiento : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private GridDatos gridDatos;
    private RadioAccion radioActual;

    private Unidad unidadSeleccionada;
    private List<Vector2Int> caminoActual = new List<Vector2Int>();
    

    private void Start()
    {
        gridDatos = new GridDatos(gridManager);
        SuscribirseACasillas();
    }

    private void AlEntrarEnCasilla(Tile tile)
    {
        BorrarCamino();

        if (unidadSeleccionada == null)
            return;

        Vector2Int destino = new Vector2Int(
            Mathf.RoundToInt(tile.transform.position.x),
            Mathf.RoundToInt(tile.transform.position.y)
        );

        if (radioActual.EsAlcanzable(destino))
        {
            caminoActual = AStar.CalcularCamino(
                gridDatos,
                unidadSeleccionada.Posicion,
                destino,
                unidadSeleccionada
            );

            foreach (Vector2Int posicion in caminoActual)
            {
                Tile casillaCamino = gridManager.GetTile(posicion);

                if (casillaCamino != null)
                {
                    casillaCamino.SetHighlightColor(Color.blue);
                    casillaCamino.SetHighlight(true);
                }
            }
        }
    }

    private void AlSalirDeCasilla(Tile tile)
    {
        //Debug.Log("Raton fuera de la casilla: " + tile.name);
    }

    private void AlHacerClickEnCasilla(Tile tile)
    {
        print("Seleccionando casilla");
        if (unidadSeleccionada == null)
            return;

        Vector2Int destino = new Vector2Int(
            Mathf.RoundToInt(tile.transform.position.x),
            Mathf.RoundToInt(tile.transform.position.y)
        );

        print("Destino: " + destino);
        
        if (!radioActual.EsAlcanzable(destino))
            return;

        List<Vector2Int> camino = AStar.CalcularCamino(
            gridDatos,
            unidadSeleccionada.Posicion,
            destino,
            unidadSeleccionada
        );

        if (camino.Count == 0)
            return;

        unidadSeleccionada.MoverA(destino);
        bool puedeTomar = gridDatos.Puede_Tomar_Refineria(destino, unidadSeleccionada.owner);
        gameObject.SendMessage("PerformPlayerAction");

        LimpiarResaltados();

        radioActual = Dijkstra.CalcularRadio(
            gridDatos,
            unidadSeleccionada.Posicion,
            unidadSeleccionada,
            unidadSeleccionada.PuntosMovimiento
        );

        MostrarRadio();
    }

    private void SuscribirseACasillas()
    {
        List<Tile> casillas = gridManager.GetAllTiles();

        foreach (Tile tile in casillas)
        {
            tile.AlEntrarRaton += AlEntrarEnCasilla;
            tile.AlSalirRaton += AlSalirDeCasilla;
            tile.AlHacerClick += AlHacerClickEnCasilla;
        }
    }

    public void SeleccionarUnidad(Unidad unidad)
    {
        LimpiarResaltados();
        unidadSeleccionada = unidad;

        radioActual = Dijkstra.CalcularRadio(
            gridDatos,
            unidadSeleccionada.Posicion,
            unidadSeleccionada,
            unidadSeleccionada.PuntosMovimiento
        );

        print("Radio actual: " + radioActual);

        MostrarRadio();

        Debug.Log("Unidad seleccionada: " + unidadSeleccionada.Tipo);
        Debug.Log("Radio de accion calculado.");
    }

    private void MostrarRadio()
    {
        List<Tile> casillas = gridManager.GetAllTiles();

        foreach (Tile tile in casillas)
        {
            Vector2Int posicion = new Vector2Int(
                Mathf.RoundToInt(tile.transform.position.x),
                Mathf.RoundToInt(tile.transform.position.y)
            );

            
            if (radioActual.EsAlcanzable(posicion))
            {
                tile.SetHighlightColor(new Color(1f, 1f, 1f, 0.4f));
                tile.SetHighlight(true);
            }
            else
            {
                tile.SetHighlight(false);
            }
        }
    }

    private void BorrarCamino()
    {
        foreach (Vector2Int posicion in caminoActual)
        {
            Tile tile = gridManager.GetTile(posicion);

            if (tile != null)
            {
                tile.SetHighlightColor(new Color(1f, 1f, 1f, 0.4f));
                tile.SetHighlight(true);
            }
        }

        caminoActual.Clear();
    }

    private void LimpiarResaltados()
    {
        List<Tile> casillas = gridManager.GetAllTiles();

        foreach (Tile tile in casillas)
        {
            tile.SetHighlight(false);
        }

        caminoActual.Clear();
    }
}