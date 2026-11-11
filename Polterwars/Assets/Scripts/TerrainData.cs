using UnityEngine;

[CreateAssetMenu(fileName = "TerrainData", menuName = "TerrainData")]
public class TerrainData : ScriptableObject
{
    public TerrainType tipo;
    public Color color_normal;
    public Color color_offset;
}
