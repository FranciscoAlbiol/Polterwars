using UnityEngine;
using System.Collections.Generic;


public class Task
{
    public List<Task> hijos;

    public void add_hijos(Task hijo) {
        hijos.Add(hijo);
    }
}
