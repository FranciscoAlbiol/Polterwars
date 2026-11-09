
using UnityEngine;

public class Unidad : MonoBehaviour
{
    [SerializeField] private UnitType tipo;
    [SerializeField] private Vector2Int posicion;

    public UnitType Tipo => tipo;
    public Vector2Int Posicion => posicion;
    public int PuntosMovimiento => ObtenerPuntosMovimiento();

    private void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        sprite.sortingOrder = 1;

    }

    private int ObtenerPuntosMovimiento()
    {
        switch (tipo)
        {
            case UnitType.Peon:
                return 3;

            case UnitType.Barquito:
                return 5;

            case UnitType.Explorador:
                return 8;

            case UnitType.Aranya:
                return 6;

            case UnitType.Bailarina:
                return 4;

            case UnitType.Mono:
                return 4;

            case UnitType.Oso:
                return 3;

            case UnitType.Vudu:
                return 4;
        }

        return 0;
    }

    public void Configurar(UnitType nuevoTipo, Vector2Int nuevaPosicion)
    {
        tipo = nuevoTipo;
        posicion = nuevaPosicion;

        transform.position = new Vector3(
            posicion.x,
            posicion.y,
            0
        );
    }

    private void OnMouseDown()
    {
        ControladorMovimiento controlador =
            FindObjectOfType<ControladorMovimiento>();

        if (controlador != null)
        {
            controlador.SeleccionarUnidad(this);
        }
    }

    public void MoverA(Vector2Int nuevaPosicion)
    {
        posicion = nuevaPosicion;

        transform.position = new Vector3(
            posicion.x,
            posicion.y,
            0
        );
    }
}
