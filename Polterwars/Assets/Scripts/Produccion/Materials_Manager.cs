using UnityEngine;
using System.Collections.Generic;

public class Materials_Manager : MonoBehaviour
{

    public Dictionary<Tipos_Material, int> materiales_jugador;
    public Dictionary<Tipos_Material, int> materiales_IA;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Inicializar_Listas();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void Inicializar_Listas() {
        materiales_jugador = new Dictionary<Tipos_Material, int>();
        materiales_jugador.Add(Tipos_Material.Madera, 0);
        materiales_jugador.Add(Tipos_Material.Hojalata, 0);
        materiales_jugador.Add(Tipos_Material.Algodon, 0);
        materiales_jugador.Add(Tipos_Material.Almas, 0);

        materiales_IA = new Dictionary<Tipos_Material, int>();
        materiales_IA.Add(Tipos_Material.Madera, 0);
        materiales_IA.Add(Tipos_Material.Hojalata, 0);
        materiales_IA.Add(Tipos_Material.Algodon, 0);
        materiales_IA.Add(Tipos_Material.Almas, 0);
        
    }

    public void dar_materiales(int cantidad, Tipos_Material material, TurnType jugador) {
        if(jugador == TurnType.Player) {
            materiales_jugador[material] += cantidad;
            print("Material añadido a jugador\n Tiene " + materiales_jugador[material]);
        }
        else {
            materiales_IA[material] += cantidad;
        }
    }

    public bool TieneMateriales(int cantidad, Tipos_Material material, TurnType jugador)
    {
        if (jugador == TurnType.Player)
            return materiales_jugador[material] >= cantidad;

        if (jugador == TurnType.AI)
            return materiales_IA[material] >= cantidad;

        return false;
    }

    public void GastarMateriales(int cantidad, Tipos_Material material, TurnType jugador)
    {
        if (!TieneMateriales(cantidad, material, jugador))
            return;

        if (jugador == TurnType.Player)
            materiales_jugador[material] -= cantidad;

        else if (jugador == TurnType.AI)
            materiales_IA[material] -= cantidad;
    }
}
