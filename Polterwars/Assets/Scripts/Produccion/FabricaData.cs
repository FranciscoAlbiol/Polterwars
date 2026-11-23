using UnityEngine;

[CreateAssetMenu(fileName = "FabricaData", menuName = "Stats de fabrica")]
public class FabricaData : ScriptableObject
{
    public TipoFabrica tipo;
    public Sprite imagen;

    [Header("Construccion")]
    public int coste_almas = 500;
    public int coste_material = 100;
    public Tipos_Material material;

    public int vida_max = 15;

    [Header("Unidades que puede fabricar")]
    public UnitType[] unidades_fabricables;
}