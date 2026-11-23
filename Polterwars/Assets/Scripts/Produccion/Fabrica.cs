
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
    private ControladorProduccion controladorProduccion;

    public TipoFabrica Tipo => tipo;
    public TurnType Owner => owner;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        controladorProduccion = FindObjectOfType<ControladorProduccion>();
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

    public FabricaData ObtenerDatos()//para una fabrica que instancias
    {
        return datos;
    }

    public FabricaData ObtenerDatos(TipoFabrica nuevoTipo)//para un tipo de frabrica concreto sin necesidad de instanciarlo
    {
        foreach (ListElement fabrica in database_array)
        {
            if (fabrica.tipo == nuevoTipo)
                return fabrica.data;
        }

        return null;
    }

    public void PerderVida(int cantidad)
    {
        vida_actual -= cantidad;

        if (vida_actual <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current != null &&
            UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        FindObjectOfType<ControladorProduccion>().CerrarPanel();

        if (owner != TurnType.Player)
            return;

        if (controladorProduccion != null)
        {
            controladorProduccion.SeleccionarFabrica(this);
        }

    }
}
