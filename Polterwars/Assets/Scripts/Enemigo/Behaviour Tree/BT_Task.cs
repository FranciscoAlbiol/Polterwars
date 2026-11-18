using UnityEngine;
using System.Collections.Generic;


public class Task
{
    public List<Task> hijos;

    public virtual void create_node() { }
    public virtual void add_hijos() { }

    public virtual bool run() {
        return true;
    }
}
