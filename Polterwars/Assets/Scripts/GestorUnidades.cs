
using System.Collections.Generic;
using UnityEngine;

public class GestorUnidades : MonoBehaviour
{
    [SerializeField] private Unidad prefabUnidad;
    [SerializeField] private GridManager grid_manager;

    private List<Unidad> unidades = new List<Unidad>();

    private void Start()
    {
        CrearUnidad(UnitType.Peon, new Vector2Int(1, 1), TurnType.Player);
        CrearUnidad(UnitType.Barquito, new Vector2Int(3, 1), TurnType.Player);
        CrearUnidad(UnitType.Explorador, new Vector2Int(5, 1), TurnType.Player);
        
        Debug.Log("Unidades creadas: " + unidades.Count);
    }
    public Unidad CrearUnidad(UnitType tipo, Vector2Int posicion, TurnType duenyo)
    {
        Unidad nuevaUnidad = Instantiate(prefabUnidad);

        nuevaUnidad.Configurar(tipo, posicion, duenyo);
        unidades.Add(nuevaUnidad);

        Debug.Log(
            "Unidad creada: " + tipo +
            " | Posicion: " + nuevaUnidad.transform.position
        );

        grid_manager.Ocupar_Posicion(posicion);

        return nuevaUnidad;
    }
}
