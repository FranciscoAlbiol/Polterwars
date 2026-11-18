using UnityEngine;
using System.Collections.Generic;


public class Peon_Behaviour_Tree : Task //raiz
{

    public override void create_node() {
        hijos = new List<Task>();

        add_hijos();
        foreach (Task c in hijos) {
            c.create_node();
        }
        Debug.Log("I'm done giving birth");
    }

    public override void add_hijos() {
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

public class Peon_Movimiento : Task
{
    public override bool run() {
        Debug.Log("Imaginate que me muevo");
        return true;
    }
}

public class Peon_ElegirMoverse : Task 
{
    public override void create_node() {
        hijos = new List<Task>();

        add_hijos();
        foreach (Task c in hijos) {
            c.create_node();
        }
    }
    
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
