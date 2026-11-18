using UnityEngine;
using System.Collections.Generic;

public class IA_Turn_Controller : MonoBehaviour
{
    private Dictionary<UnitType, Task> behaviour_trees_database;
    [SerializeField] GestorUnidades gestorUnidades;
    private List<Unidad> lista_unidades_IA;

    void Start()
    {
        //CREAR DICCIONARIO DE ARBOLES DE COMPORTAMIENTO
        behaviour_trees_database = new Dictionary<UnitType, Task>();

        Task peon_BT = new Peon_Behaviour_Tree();
        peon_BT.create_node();

        behaviour_trees_database.Add(UnitType.Peon, peon_BT);

        //DEBUG: Crear unidades enemigas
        lista_unidades_IA = new List<Unidad>();
        lista_unidades_IA.Add(gestorUnidades.CrearUnidad(UnitType.Peon, new Vector2Int(5, 8), TurnType.AI));
    }

    public void make_action() {
        int index = Random.Range(0, lista_unidades_IA.Count -1);
        Unidad unidad_aleatoria = lista_unidades_IA[index];
        
        print("thinking about my next move. one moment./");
        
        Task unit_task = behaviour_trees_database[unidad_aleatoria.ObtenerTipo()];
        unit_task.run();

        print("y'all just porns to me");

        
    }
}
