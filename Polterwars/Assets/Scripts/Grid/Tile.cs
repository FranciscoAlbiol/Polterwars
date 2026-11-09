using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor, offsetColor;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;

    public string terrainType;



    public void setTileColor(bool isOffset) {
        renderer.color = isOffset ? offsetColor : baseColor;
    }

    void OnMouseEnter() {
        highlight.SetActive(true);
        print("mouse entered");
    }

    void OnMouseExit() {
        highlight.SetActive(false);
        print("mouse exited");
    }
}
