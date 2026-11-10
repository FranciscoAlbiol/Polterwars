
using System.Collections.Generic;
using UnityEngine;

public static class Dijkstra
{
    public static RadioAccion CalcularRadio(
        GridDatos grid,
        Vector2Int origen,
        UnitType unidad,
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

                foreach (NodoGrid vecino in vecinos)
                {
                    int costeMovimiento = CostesPM.ObtenerCoste(
                        unidad,
                        vecino.Terreno
                    );

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

        return resultado;
    }
}
