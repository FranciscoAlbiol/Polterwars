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

    public UnitType Tipo => tipo;
    public Vector2Int Posicion => posicion;
    public int PuntosMovimiento => ObtenerPuntosMovimiento();

    private Dictionary<UnitType, UnitData> database;

    private void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sortingOrder = 1;

        //inicializa el diccionario (unity no permite diccionarios en el inspector)
        database = new Dictionary<UnitType, UnitData>();
        foreach(ListElement unit in database_array) {
            UnitType tipo_a = unit.tipo;
            UnitData data_a = unit.data;

            database.Add(tipo_a, data_a);
        }

    }

    private int ObtenerPuntosMovimiento()
    {
        return database[tipo].puntos_movimiento;
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
