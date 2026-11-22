
using UnityEngine;

public class Fabrica : MonoBehaviour
{
    [System.Serializable]
    public struct ListElement
    {
        public TipoFabrica tipo;
        public FabricaData data;
    }

    [SerializeField] private TipoFabrica tipo;
    [SerializeField] private ListElement[] database_array;

    public TurnType owner = TurnType.None;
    public int vida_actual;

    private FabricaData datos;
    private SpriteRenderer sprite;

    public TipoFabrica Tipo => tipo;
    public TurnType Owner => owner;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void Configurar(
        TipoFabrica nuevoTipo,
        TurnType nuevoOwner,
        Vector2Int nuevaPosicion)
    {
        tipo = nuevoTipo;
        owner = nuevoOwner;

        datos = null;

        foreach (ListElement fabrica in database_array)
        {
            if (fabrica.tipo == tipo)
            {
                datos = fabrica.data;
                break;
            }
        }

        if (datos == null)
        {
            Debug.LogError("No hay datos para la fabrica: " + tipo);
            return;
        }

        sprite.sprite = datos.imagen;
        vida_actual = datos.vida_max;

        transform.position = new Vector3(
            nuevaPosicion.x,
            nuevaPosicion.y,
            0
        );
    }

    public void TomarFabrica(TurnType nuevoOwner)
    {
        owner = nuevoOwner;
    }

    public FabricaData ObtenerDatos()
    {
        return datos;
    }

    public void PerderVida(int cantidad)
    {
        vida_actual -= cantidad;

        if (vida_actual <= 0)
        {
            Destroy(gameObject);
        }
    }
}
