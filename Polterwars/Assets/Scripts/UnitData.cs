using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Stats de unidad")]
public class UnitData : ScriptableObject
{
    public UnitType tipo;
    public int puntos_movimiento;
}
