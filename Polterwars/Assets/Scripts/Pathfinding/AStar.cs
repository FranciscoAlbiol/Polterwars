using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    public static List<Vector2Int> CalcularCamino(
        GridDatos grid,
        Vector2Int origen,
        Vector2Int destino,
        Unidad unidad)
    {
        List<Vector2Int> pendientes = new List<Vector2Int>();
        List<Vector2Int> visitados = new List<Vector2Int>();

        Dictionary<Vector2Int, int> costes = new Dictionary<Vector2Int, int>();
        Dictionary<Vector2Int, Vector2Int> padres = new Dictionary<Vector2Int, Vector2Int>();

        NodoGrid nodoDestino = grid.ObtenerNodo(destino);

        UnitData data = unidad.ObtenerDatos();
        Debug.Log("DATOS A ENVIAR EN STAR: " + data);

        if (nodoDestino == null ||
            CostesPM.ObtenerCoste(unidad.ObtenerDatos(), nodoDestino.Terreno) < 0)
        {
            return new List<Vector2Int>();
        }

        costes[origen] = 0;
        pendientes.Add(origen);

        while (pendientes.Count > 0)
        {
            Vector2Int actual = pendientes[0];

            foreach (Vector2Int posicion in pendientes)
            {
                int valorActual = costes[actual] + CalcularHeuristica(actual, destino);
                int valorNuevo = costes[posicion] + CalcularHeuristica(posicion, destino);

                if (valorNuevo < valorActual)
                {
                    actual = posicion;
                }
            }

            pendientes.Remove(actual);

            if (actual == destino)
            {
                return ReconstruirCamino(padres, origen, destino);
            }

            visitados.Add(actual);

            List<NodoGrid> vecinos = grid.ObtenerVecinos(actual);

            foreach (NodoGrid vecino in vecinos)
            {
                Vector2Int posicionVecina = vecino.Posicion;

                int costeMovimiento = CostesPM.ObtenerCoste(
                    unidad.ObtenerDatos(),
                    vecino.Terreno
                );

                if (costeMovimiento >= 0 &&
                    !visitados.Contains(posicionVecina))
                {
                    int nuevoCoste = costes[actual] + costeMovimiento;

                    if (!costes.ContainsKey(posicionVecina) ||
                        nuevoCoste < costes[posicionVecina])
                    {
                        costes[posicionVecina] = nuevoCoste;
                        padres[posicionVecina] = actual;

                        if (!pendientes.Contains(posicionVecina))
                        {
                            pendientes.Add(posicionVecina);
                        }
                    }
                }
            }
        }

        return new List<Vector2Int>();
    }

    private static int CalcularHeuristica(
        Vector2Int actual,
        Vector2Int destino)
    {
        return Mathf.Abs(actual.x - destino.x) +
               Mathf.Abs(actual.y - destino.y);
    }

    private static List<Vector2Int> ReconstruirCamino(
        Dictionary<Vector2Int, Vector2Int> padres,
        Vector2Int origen,
        Vector2Int destino)
    {
        List<Vector2Int> camino = new List<Vector2Int>();

        Vector2Int actual = destino;

        camino.Add(actual);

        while (actual != origen)
        {
            actual = padres[actual];
            camino.Add(actual);
        }

        camino.Reverse();

        return camino;
    }
}