
using System.Collections.Generic;
using UnityEngine;

public static class Dijkstra
{
    public static RadioAccion CalcularRadio(
        GridDatos grid,
        Vector2Int origen,
        Unidad unidad,
        int puntosMovimiento)
    {
        RadioAccion resultado = new RadioAccion();

        List<Vector2Int> pendientes = new List<Vector2Int>();

        Dictionary<Vector2Int, int> costes = new Dictionary<Vector2Int, int>();

        costes[origen] = 0;
        pendientes.Add(origen);

        while (pendientes.Count > 0)
        {
            Vector2Int actual = pendientes[0];

            foreach (Vector2Int posicion in pendientes)
            {
                if (costes[posicion] < costes[actual])
                {
                    actual = posicion;
                }
            }

            pendientes.Remove(actual);

            int costeActual = costes[actual];

            if (costeActual <= puntosMovimiento)
            {
                resultado.AnadirCasilla(actual, costeActual);

                List<NodoGrid> vecinos = grid.ObtenerVecinos(actual);
                UnitData data = unidad.ObtenerDatos();

                foreach (NodoGrid vecino in vecinos)
                {
                    //Aqui, unidad.ObtenerDatos no es null
                    Debug.Log("PUNTOS MOVIMIENTO: " + data.puntos_movimiento);
                    Debug.Log("TIPO: " + data.tipo);

                    

                    //ESTO ES NULL ?????
                    int costeMovimiento = CostesPM.ObtenerCoste(
                        data,
                        vecino.Terreno
                    );

                    Debug.Log("COSTE DE MOVIMIENTO: " + costeMovimiento);

                    if (costeMovimiento >= 0)
                    {
                        int nuevoCoste = costeActual + costeMovimiento;

                        if (nuevoCoste <= puntosMovimiento)
                        {
                            Vector2Int posicionVecina = vecino.Posicion;

                            if (!costes.ContainsKey(posicionVecina) ||
                                nuevoCoste < costes[posicionVecina])
                            {
                                costes[posicionVecina] = nuevoCoste;

                                if (!pendientes.Contains(posicionVecina))
                                {
                                    pendientes.Add(posicionVecina);
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("RESULTADO DE DIJSKTRA: " + resultado);
        return resultado;
    }
}
