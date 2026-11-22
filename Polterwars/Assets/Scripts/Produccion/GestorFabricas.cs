
using System.Collections.Generic;
using UnityEngine;

public class GestorFabricas : MonoBehaviour
{
    [SerializeField] private Fabrica prefabFabrica;
    [SerializeField] private GridManager grid_manager;
    [SerializeField] private Materials_Manager materials_manager;

    public List<Fabrica> fabricas = new List<Fabrica>();

    public TurnManager turnManager;

    public Fabrica CrearFabrica(
        TipoFabrica tipo,
        Vector2Int posicion,
        TurnType duenyo)
    {
        Fabrica nuevaFabrica = Instantiate(prefabFabrica);

        nuevaFabrica.Configurar(tipo, duenyo, posicion);

        fabricas.Add(nuevaFabrica);
        grid_manager.fabricas.Add(nuevaFabrica);

        Debug.Log(
            "Fabrica creada: " + tipo +
            " | Posicion: " + posicion +
            " | Propietario: " + duenyo
        );

        grid_manager.Ocupar_Posicion(posicion);

        return nuevaFabrica;
    }

    private bool PuedeConstruirFabrica(TipoFabrica tipo, TurnType duenyo)
    {
        FabricaData datos = prefabFabrica.ObtenerDatos(tipo);

        if (datos == null)
            return false;

        if (!materials_manager.TieneMateriales(datos.coste_almas, Tipos_Material.Almas, duenyo))//si tiene almas
            return false;

        if (!materials_manager.TieneMateriales(datos.coste_material, datos.material, duenyo))//si tiene material
            return false;

        return true;
    }

    public bool ConstruirFabrica(TipoFabrica tipo, Vector2Int posicion, TurnType duenyo)
    {
        if (!PuedeConstruirFabrica(tipo, duenyo))
            return false;

        Tile tile = grid_manager.GetTile(posicion);

        if (tile == null || tile.taken)
            return false;

        if (tile.terrainType == TerrainType.Gotera || tile.terrainType == TerrainType.Caja)
            return false;

        FabricaData datos = prefabFabrica.ObtenerDatos(tipo);

        materials_manager.GastarMateriales(
            datos.coste_almas,
            Tipos_Material.Almas,
            duenyo
        );

        materials_manager.GastarMateriales(
            datos.coste_material,
            datos.material,
            duenyo
        );

        CrearFabrica(tipo, posicion, duenyo);

        if (duenyo == TurnType.Player)
        {
            turnManager.PerformPlayerAction();
        }

        return true;
    }

}
