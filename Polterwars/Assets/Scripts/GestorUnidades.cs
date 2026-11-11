
using System.Collections.Generic;
using UnityEngine;

public class GestorUnidades : MonoBehaviour
{
    [SerializeField] private Unidad prefabUnidad;

    private List<Unidad> unidades = new List<Unidad>();

    private void Start()
    {
        CrearUnidad(UnitType.Peon, new Vector2Int(1, 1));
        CrearUnidad(UnitType.Barquito, new Vector2Int(3, 1));
        CrearUnidad(UnitType.Explorador, new Vector2Int(5, 1));
        Debug.Log("Unidades creadas: " + unidades.Count);
    }
    public Unidad CrearUnidad(UnitType tipo, Vector2Int posicion)
    {
        Unidad nuevaUnidad = Instantiate(prefabUnidad);

        nuevaUnidad.Configurar(tipo, posicion);

        unidades.Add(nuevaUnidad);

        Debug.Log(
            "Unidad creada: " + tipo +
            " | Posici�n: " + nuevaUnidad.transform.position
        );

        return nuevaUnidad;
    }
}
