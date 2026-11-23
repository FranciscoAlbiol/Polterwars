using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMovimiento : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private GameObject panelConstruccion;
    [SerializeField] private GestorFabricas gestorFabricas;
    [SerializeField] private Button botonRelojAveriado;
    [SerializeField] private Button botonCarpaFeria;
    [SerializeField] private Button botonMaquinaCoser;

    private GridDatos gridDatos;
    private RadioAccion radioActual;

    private Unidad unidadSeleccionada;
    private List<Vector2Int> caminoActual = new List<Vector2Int>();

    public TurnManager turnManager;
    public InfluenceMapManager influenceMapManager;

    


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
        if (turnManager.IsAITurn())
            return;

        FindObjectOfType<ControladorProduccion>().CerrarPanel();

        if (unidadSeleccionada == null)
        {
            SeleccionarCasillaConstruccion(tile);
            return;
        }

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


        if (camino.Count -1 <=0)
            return;

        Vector2Int posicionAnterior = unidadSeleccionada.Posicion;
        gridManager.Liberar_Posicion(posicionAnterior);

        unidadSeleccionada.MoverA(destino);
        gridManager.Ocupar_Posicion(destino);
        bool puedeTomar = gridDatos.Puede_Tomar_Refineria(destino, unidadSeleccionada.owner);
        turnManager.PerformPlayerAction();



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
        if(unidad.owner != TurnType.Player){
            if(unidadSeleccionada != null) {
                AtacarUnidad(unidadSeleccionada, unidad);
            }

            return;
        }
        if(unidad.seleccionada) {
            DeseleccionarUnidad(unidad);
            return;
        }
        LimpiarResaltados();
        panelConstruccion.SetActive(false);
        unidadSeleccionada = unidad;
        unidad.seleccionada = true;

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

    public void AtacarUnidad(Unidad unidad_atacante, Unidad unidad_atacada) {
        int alcance_atacante = unidad_atacante.ObtenerAlcance();

        RadioAccion radio_ataque = Dijkstra.CalcularRadio(
            gridDatos,
            unidad_atacante.Posicion,
            unidad_atacante,
            alcance_atacante
        );

        if (radio_ataque.EsAlcanzable(unidad_atacada.Posicion))
        {
            print("KICK HIS ASS");
            int ataque_unidad = unidad_atacante.ObtenerAtaque();
            unidad_atacada.PerderVida(ataque_unidad);
        }

    }

    public void DeseleccionarUnidad(Unidad unidad) {
        LimpiarResaltados();
        unidadSeleccionada = null;
        unidad.seleccionada = false;
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

    public GridDatos ObtenerDatosGrid() {
        return gridDatos;
    }

    private void SeleccionarCasillaConstruccion(Tile tile)//Esto igual lo muevo de script en algun momento
    {
        panelConstruccion.SetActive(false);

        print("Probando construccion");
        if (tile == null || tile.taken)
            return;

        if (tile.terrainType == TerrainType.Gotera ||
            tile.terrainType == TerrainType.Caja)
            return;

        if (influenceMapManager.GetSecurity(tile) <= 0)
            return;

        panelConstruccion.SetActive(true);

        Vector3 posicionPantalla = Camera.main.WorldToScreenPoint(
            tile.transform.position
        );

        posicionPantalla.x += 150;
        posicionPantalla.y += 50;

        panelConstruccion.transform.position = posicionPantalla;

        botonRelojAveriado.onClick.RemoveAllListeners();
        botonCarpaFeria.onClick.RemoveAllListeners();
        botonMaquinaCoser.onClick.RemoveAllListeners();

        botonRelojAveriado.onClick.AddListener(() =>
            ConstruirEnCasilla(TipoFabrica.RelojAveriado, tile));

        botonCarpaFeria.onClick.AddListener(() =>
            ConstruirEnCasilla(TipoFabrica.CarpaFeria, tile));

        botonMaquinaCoser.onClick.AddListener(() =>
            ConstruirEnCasilla(TipoFabrica.MaquinaCoser, tile));
    }

    public void ConstruirEnCasilla(TipoFabrica tipo, Tile tile)
    {
        Vector2Int posicion = new Vector2Int(
            Mathf.RoundToInt(tile.transform.position.x),
            Mathf.RoundToInt(tile.transform.position.y)
        );

        bool construida = gestorFabricas.ConstruirFabrica(
            tipo,
            posicion,
            TurnType.Player
        );

        if (construida)
        {
            panelConstruccion.SetActive(false);
        }
        else
        {
            Debug.Log("No se puede construir: comprueba los materiales y la casilla.");
        }
    }
}