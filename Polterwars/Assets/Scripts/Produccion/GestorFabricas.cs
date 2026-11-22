
using System.Collections.Generic;
using UnityEngine;

public class GestorFabricas : MonoBehaviour
{
    [SerializeField] private Fabrica prefabFabrica;
    [SerializeField] private GridManager grid_manager;
    [SerializeField] private Materials_Manager materials_manager;

    public List<Fabrica> fabricas = new List<Fabrica>();

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
        return false;
    }

}
