using UnityEngine;
using System;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    [Serializable]
    public struct ListElement {
        public TerrainType tipo;
        public TerrainData data;
    }


    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private SpriteRenderer highlightRenderer;
    [SerializeField] private GameObject mouseHighlight;

    public event Action<Tile> AlEntrarRaton;
    public event Action<Tile> AlSalirRaton;
    public event Action<Tile> AlHacerClick;

    public TerrainType terrainType;
    [SerializeField] private ListElement[] database_array;
    private Dictionary<TerrainType, TerrainData> database;

    private void Awake()
    {
        //inicializa el diccionario (unity no permite diccionarios en el inspector)
        database = new Dictionary<TerrainType, TerrainData>();
        foreach(ListElement terrain in database_array) {
            TerrainType tipo_a = terrain.tipo;
            TerrainData data_a = terrain.data;

            database.Add(tipo_a, data_a);
            print("Added: " + tipo_a);  
        }
    }

    public void SetTerrain(TerrainType terrain)
    {
        terrainType = terrain;
    }

    public void setTileColor(bool isOffset) {
        //print("Tipo de terreno: " + terrainType);
        TerrainData data = database[terrainType];
        renderer.color = isOffset ? data.color_offset : data.color_normal;
    }

    void OnMouseEnter()
    {
        mouseHighlight.SetActive(true);
        AlEntrarRaton?.Invoke(this);
    }

    void OnMouseExit()
    {
        mouseHighlight.SetActive(false);
        AlSalirRaton?.Invoke(this);
    }

    public void SetHighlight(bool activo)
    {
        highlight.SetActive(activo);
    }

    public void SetHighlightColor(Color color)
    {
        highlightRenderer.color = color;
    }

    private void OnMouseDown()
    {
        AlHacerClick?.Invoke(this);
    }
}
