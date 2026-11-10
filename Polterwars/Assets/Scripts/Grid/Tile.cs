using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    [SerializeField] private SpriteRenderer highlightRenderer;
    [SerializeField] private GameObject mouseHighlight;

    public event Action<Tile> AlEntrarRaton;
    public event Action<Tile> AlSalirRaton;
    public event Action<Tile> AlHacerClick;

    public TerrainType terrainType;

    public void SetTerrain(TerrainType terrain)
    {
        terrainType = terrain;
    }

    public void setTileColor(bool isOffset) {
        renderer.color = isOffset ? offsetColor : baseColor;
    }

    void OnMouseEnter()
    {
        mouseHighlight.SetActive(true);
        AlEntrarRaton?.Invoke(this);
        print("mouse entered");
    }

    void OnMouseExit()
    {
        mouseHighlight.SetActive(false);
        AlSalirRaton?.Invoke(this);
        print("mouse exited");
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
