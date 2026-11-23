using System;
using UnityEngine;
using System.Collections.Generic;

public class Unidad : MonoBehaviour
{
    [Serializable]
    public struct ListElement {
        public UnitType tipo;
        public UnitData data;
    }

    [SerializeField] private UnitType tipo;
    [SerializeField] private Vector2Int posicion;
    [SerializeField] private ListElement[] database_array;
    public bool seleccionada = false;

    public UnitType Tipo => tipo;
    public Vector2Int Posicion => posicion;
    public TurnType owner = TurnType.Player; //se inicializara como dios manda al crearlas en las fabricas
    public int PuntosMovimiento => ObtenerPuntosMovimiento();
    public event Action<Unidad> AlMoverse; //Para el mapa de influencia

    private Dictionary<UnitType, UnitData> database;

    public int vida_actual = 10;
    private SpriteRenderer sprite;

    [HideInInspector] // Hides var below
    public ControladorMovimiento controlador;

    private void Awake()
    {
        InicializarBaseDatos();

        sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 1;
        sprite.sprite = getImage();
    }

    private void InicializarBaseDatos()
    {
        if (database != null) return;

        database = new Dictionary<UnitType, UnitData>();

        foreach (ListElement unit in database_array)
        {
            database.Add(unit.tipo, unit.data);
        }
    }

    private void Start() {
        controlador = FindObjectOfType<ControladorMovimiento>();
    }

    private Sprite getImage() {
        return database[tipo].imagen;
    }

    public int ObtenerPuntosMovimiento()
    {
        return database[tipo].puntos_movimiento;
    }

    public void Configurar(UnitType nuevoTipo, Vector2Int nuevaPosicion, TurnType nuevo_owner)
    {
        tipo = nuevoTipo;
        posicion = nuevaPosicion;
        owner = nuevo_owner;
        vida_actual = ObtenerVidaMax();
        sprite.sprite = getImage();

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

        FindObjectOfType<ControladorProduccion>().CerrarPanel();

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

        AlMoverse?.Invoke(this);//Para el mapa de influencia
    }

    public UnitData ObtenerDatos() {
        return database[tipo];
    }

    public UnitData ObtenerDatos(UnitType nuevoTipo)
    {
        InicializarBaseDatos();

        if (database.TryGetValue(nuevoTipo, out UnitData datos))
            return datos;

        Debug.LogError("No hay datos para la unidad: " + nuevoTipo);
        return null;
    }

    public UnitType ObtenerTipo() {
        return tipo;
    }

    public int ObtenerAlcance() {
        return database[tipo].alcance;
    }

    public int ObtenerVidaMax() {
        if (database[tipo])
            return database[tipo].vida_max;
        else
            Debug.LogError("huh");
            return 6;
    }

    public int ObtenerAtaque() {
        return database[tipo].ataque;
    }

    public void PerderVida(int vida) {
        vida_actual -= vida;
    }
}
