
using System.Collections.Generic;
using UnityEngine;

public class GestorUnidades : MonoBehaviour
{
    [SerializeField] private Unidad prefabUnidad;
    [SerializeField] private GridManager grid_manager;
    [SerializeField] private InfluenceMapManager influenceMapManager;
    [SerializeField] private Materials_Manager materials_manager;

    private List<Unidad> unidades = new List<Unidad>();

    private void Start()
    {
        CrearUnidad(UnitType.Peon, new Vector2Int(1, 1), TurnType.Player);
        CrearUnidad(UnitType.Barquito, new Vector2Int(3, 6), TurnType.Player);
        CrearUnidad(UnitType.Explorador, new Vector2Int(5, 1), TurnType.Player);
        
        Debug.Log("Unidades creadas: " + unidades.Count);

        influenceMapManager.SuscribirUnidades();
        influenceMapManager.RecalculateInfluenceMap();
    }
    public Unidad CrearUnidad(UnitType tipo, Vector2Int posicion, TurnType duenyo)
    {
        Tile tile = grid_manager.GetTile(posicion);

        if (tile == null || tile.taken)
            return null;

        Unidad nuevaUnidad = Instantiate(prefabUnidad);

        nuevaUnidad.Configurar(tipo, posicion, duenyo);
        unidades.Add(nuevaUnidad);

        Debug.Log(
            "Unidad creada: " + tipo +
            " | Posicion: " + nuevaUnidad.transform.position
        );

        grid_manager.Ocupar_Posicion(posicion);

        if(duenyo == TurnType.Player) 
            GameManager.gameManager.add_unidades_jugador(nuevaUnidad);

        return nuevaUnidad;
    }

    private bool PuedeFabricarUnidad(UnitType tipo, TurnType duenyo)
    {
        UnitData datos = prefabUnidad.ObtenerDatos(tipo);

        if (datos == null) return false;

        if (!materials_manager.TieneMateriales(
            datos.coste_almas, Tipos_Material.Almas, duenyo))
            return false;

        foreach (UnitData.MaterialCost coste in datos.costes_materiales)
        {
            if (!materials_manager.TieneMateriales(
                coste.cantidad, coste.material, duenyo))
                return false;
        }

        return true;
    }

    public bool FabricarUnidad(UnitType tipo, Vector2Int posicion, TurnType duenyo)
    {
        if (!PuedeFabricarUnidad(tipo, duenyo))
            return false;

        Tile tile = grid_manager.GetTile(posicion);

        if (tile == null || tile.taken)
            return false;

        Unidad nuevaUnidad = CrearUnidad(tipo, posicion, duenyo);

        if (nuevaUnidad == null)
            return false;

        UnitData datos = prefabUnidad.ObtenerDatos(tipo);

        // Gastar Almas
        materials_manager.GastarMateriales(
            datos.coste_almas, Tipos_Material.Almas, duenyo);

        // Gastar los materiales adicionales
        foreach (UnitData.MaterialCost coste in datos.costes_materiales)
        {
            materials_manager.GastarMateriales(
                coste.cantidad, coste.material, duenyo);
        }

        return true;
    }
}
