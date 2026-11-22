using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public List<Unidad> lista_unidades_jugador;

    private void Awake()
    {
        // Evita que existan varios TurnManagers
        if (gameManager != null && gameManager != this)
        {
            Destroy(gameObject);
            return;
        }

        gameManager = this;

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add_unidades_jugador(Unidad unidad_new) {
        lista_unidades_jugador.Add(unidad_new);
    }

    public void remove_from_player_list(Unidad u) {
        lista_unidades_jugador.Remove(u);
    }
}
