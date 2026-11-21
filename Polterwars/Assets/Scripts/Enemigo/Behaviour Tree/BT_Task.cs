using UnityEngine;
using System.Collections.Generic;


public class Task
{
    public List<Task> hijos;
    public Unidad unidad;


    public virtual void create_node() { }
    public virtual void add_hijos() { }

    public virtual void SetUnidad(Unidad unidad_new) { //funcion recurviva para que todas las
        unidad = unidad_new;

        if (hijos == null) return;
        foreach (Task c in hijos) {
            c.SetUnidad(unidad);
        }
    }

    public virtual bool run() {
        return true;
    }
}
