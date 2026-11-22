using UnityEngine;
using System.Collections.Generic;


public class Peon_Behaviour_Tree : Task //raiz
{
    public override void add_hijos() { //El orden IMPORTA: Añadir de mas a menos prioritario / más condiciones a menos condiciones
        hijos.Add(new Peon_ElegirMoverse());
    }

    public override bool run() { //esto es un SELECTOR -> si tuviera mas hijos, seguiria probando hasta encontrar uno que pueda hacer (o hasta que se quede sin)
        foreach (Task c in hijos) {
            if (c.run())
                return true;
        }

        return false;
    }

}

//----------------------------------------------------------------------------------
//------------------------------
// MOVIMIENTO DEL PEON
//-----------------------------

public class Peon_Movimiento : Task
{
    public override bool run() {
        /*Vector2Int random_ass_pos = new Vector2Int(7, 3);
        unidad.MoverA(random_ass_pos);*/

        RadioAccion radioAccion = CalcularDikstra();

        Vector2Int destino = ObtenerCasillaMasAbajo(radioAccion, unidad.Posicion);
        EjecutarMovimiento(destino, radioAccion);


        return true;
    }

    private RadioAccion CalcularDikstra() {
        ControladorMovimiento controlador = unidad.controlador;
        GridDatos gridDatos = controlador.ObtenerDatosGrid();

        RadioAccion radioActual = Dijkstra.CalcularRadio(
            gridDatos,
            unidad.Posicion,
            unidad,
            unidad.PuntosMovimiento
        );

        return radioActual;
    }

    private Vector2Int ObtenerCasillaMasAbajo(RadioAccion radio, Vector2Int posicionActual)
    {
        Vector2Int mejorCasilla = default;
        bool found = false;
        int menorY = int.MaxValue;
        int menorCosteEnEseY = int.MaxValue;

        foreach (var par in radio.Costes)
        {
            Vector2Int pos = par.Key;
            int coste = par.Value;

            if (pos == posicionActual || coste <= 0)
                continue;


            if (!found || pos.y < menorY || (pos.y == menorY && coste < menorCosteEnEseY))
            {
                found = true;
                menorY = pos.y;
                menorCosteEnEseY = coste;
                mejorCasilla = pos;
            }
        }

        return mejorCasilla;
    }

    private bool EjecutarMovimiento(Vector2Int destino, RadioAccion radioActual)
    {
        Debug.Log("Destino: " + destino);
        ControladorMovimiento controlador = unidad.controlador;
        GridDatos gridDatos = controlador.ObtenerDatosGrid();

        if (!radioActual.EsAlcanzable(destino))
            return false;

        List<Vector2Int> camino = AStar.CalcularCamino(
            gridDatos,
            unidad.Posicion,
            destino,
            unidad
        );

        if (camino == null || camino.Count - 1 <= 0)
            return false;

        unidad.MoverA(destino);
        bool puedeTomar = gridDatos.Puede_Tomar_Refineria(destino, TurnType.AI);

        return true;
    }

}

public class Peon_ElegirMoverse : Task 
{
    public override void add_hijos() {
        hijos.Add(new Peon_Movimiento());
    }

    public override bool run() { //esto es un SELECTOR
        foreach (Task c in hijos) {
            if (c.run())
                return true;
        }

        return false;
    }
}

//----------------------------------------------------------------------------------
//------------------------------
// ATAQUE DEL PEON
//-----------------------------

//* Si tengo a alguien al lado, le ataco

public class Peon_ElegirAtacar : Task 
{   
    public override void add_hijos() {
        hijos.Add(new Peon_Movimiento());
    }

    public override bool run() { //esto es una SECUENCIA
        foreach (Task c in hijos) {
            if (!c.run())
                return false; //Si alguna de las condiciones no se cumple, no se realiza la accion
        }

        return true;
    }
}

public class Peon_ComprobarEnemigosRango : Task 
{
     
    public override void add_hijos() {
        hijos.Add(new Peon_Movimiento());
    }

    public override bool run() { //esto es una SECUENCIA
        foreach (Task c in hijos) {
            if (!c.run())
                return false; //Si alguna de las condiciones no se cumple, no se realiza la accion
        }

        return true;
    }
}
