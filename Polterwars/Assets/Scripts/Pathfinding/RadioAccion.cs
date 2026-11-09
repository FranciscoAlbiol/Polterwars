
using System.Collections.Generic;
using UnityEngine;

public class RadioAccion
{
    public Dictionary<Vector2Int, int> Costes { get; private set; }

    public RadioAccion()
    {
        Costes = new Dictionary<Vector2Int, int>();
    }

    public void AnadirCasilla(Vector2Int posicion, int coste)
    {
        Costes[posicion] = coste;
    }

    public bool EsAlcanzable(Vector2Int posicion)
    {
        return Costes.ContainsKey(posicion);
    }

    public int ObtenerCoste(Vector2Int posicion)
    {
        if (Costes.ContainsKey(posicion))
            return Costes[posicion];

        return -1;
    }
}
