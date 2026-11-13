using UnityEngine;

public class Refineria : MonoBehaviour
{
    public Tipos_Material material;
    public Vector2[] radio_toma;
    public TurnType owner = TurnType.None;

    private GridManager grid_manager;
    private Materials_Manager mat_manager;
    private bool tomada = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid_manager = FindObjectOfType<GridManager>();
        mat_manager = FindObjectOfType<Materials_Manager>();
        definir_radio_toma();

        grid_manager.refinerias.Add(this);
    }

    public void definir_radio_toma() {
        radio_toma = new [] { 
                new Vector2(transform.position.x, transform.position.y + 1),
                new Vector2(transform.position.x, transform.position.y - 1),
                new Vector2(transform.position.x + 1, transform.position.y),
                new Vector2(transform.position.x - 1, transform.position.y)
            };
    }

    public void tomar_refineria(TurnType new_owner) {
        owner = new_owner;
        tomada = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void crear_materiales() {
        if(tomada)
            mat_manager.dar_materiales(10, material, owner);
    }
}
