using UnityEngine;
using UnityEngine.UI;

public class ControladorProduccion : MonoBehaviour
{
    [SerializeField] private GameObject panelProduccion;

    [SerializeField] private Button botonHombrecito;
    [SerializeField] private Button botonArana;
    [SerializeField] private Button botonBailarina;
    [SerializeField] private Button botonMono;
    [SerializeField] private Button botonMrEddy;
    [SerializeField] private Button botonMunecoVudu;
    [SerializeField] private GridManager gridManager;

    [SerializeField] private GestorUnidades gestorUnidades;
    [SerializeField] private TurnManager turnManager;

    private Fabrica fabricaSeleccionada;

    public void SeleccionarFabrica(Fabrica fabrica)
    {
        if (fabrica == null || fabrica.Owner != TurnType.Player)
            return;

        fabricaSeleccionada = fabrica;
        panelProduccion.SetActive(true);

        Vector3 posicionPantalla = Camera.main.WorldToScreenPoint(fabrica.transform.position);

        posicionPantalla.x += 150;
        posicionPantalla.y += 50;

        panelProduccion.transform.position = posicionPantalla;

        botonHombrecito.gameObject.SetActive(fabrica.Tipo == TipoFabrica.RelojAveriado);
        botonArana.gameObject.SetActive(fabrica.Tipo == TipoFabrica.RelojAveriado);

        botonBailarina.gameObject.SetActive(fabrica.Tipo == TipoFabrica.CarpaFeria);
        botonMono.gameObject.SetActive(fabrica.Tipo == TipoFabrica.CarpaFeria);

        botonMrEddy.gameObject.SetActive(fabrica.Tipo == TipoFabrica.MaquinaCoser);
        botonMunecoVudu.gameObject.SetActive(fabrica.Tipo == TipoFabrica.MaquinaCoser);

        botonHombrecito.onClick.RemoveAllListeners();
        botonHombrecito.onClick.AddListener(() => Fabricar(UnitType.Explorador));

        botonArana.onClick.RemoveAllListeners();
        botonArana.onClick.AddListener(() => Fabricar(UnitType.Aranya));

        botonBailarina.onClick.RemoveAllListeners();
        botonBailarina.onClick.AddListener(() => Fabricar(UnitType.Bailarina));

        botonMono.onClick.RemoveAllListeners();
        botonMono.onClick.AddListener(() => Fabricar(UnitType.Mono));

        botonMrEddy.onClick.RemoveAllListeners();
        botonMrEddy.onClick.AddListener(() => Fabricar(UnitType.Oso));

        botonMunecoVudu.onClick.RemoveAllListeners();
        botonMunecoVudu.onClick.AddListener(() => Fabricar(UnitType.Vudu));

    }

    public void CerrarPanel()
    {
        panelProduccion.SetActive(false);
        fabricaSeleccionada = null;
    }

    private Vector2Int BuscarCasillaLibre()
    {
        Vector2Int centro = Vector2Int.RoundToInt(fabricaSeleccionada.transform.position);

        Vector2Int[] direcciones =
        {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

        foreach (Vector2Int direccion in direcciones)
        {
            Vector2Int posicion = centro + direccion;
            Tile tile = gridManager.GetTile(posicion);

            if (tile != null && !tile.taken && tile.terrainType != TerrainType.Caja)
                return posicion;
        }

        return new Vector2Int(int.MinValue, int.MinValue);
    }

    private void Fabricar(UnitType tipo)
    {
        if (fabricaSeleccionada == null) return;

        Vector2Int posicion = BuscarCasillaLibre();

        if (posicion.x == int.MinValue)
        {
            Debug.Log("No hay casillas libres junto a la fábrica.");
            return;
        }

        if (gestorUnidades.FabricarUnidad(tipo, posicion, TurnType.Player))
            turnManager.PerformPlayerAction();
    }


}