
using UnityEngine;
using System.Collections.Generic;

public class InfluenceMapManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private const int MAX_INFLUENCE_DISTANCE = 4;

    private Dictionary<Tile, float> playerInfluence;
    private Dictionary<Tile, float> aiInfluence;

    private List<Unidad> units;

    private void Start()
    {
       
    }

    public void RecalculateInfluenceMap()
    {
        playerInfluence = new Dictionary<Tile, float>();
        aiInfluence = new Dictionary<Tile, float>();

        List<Tile> tiles = gridManager.GetAllTiles();

        // Inicializar los mapas de influencia.
        foreach (Tile tile in tiles)
        {
            playerInfluence[tile] = 0f;
            aiInfluence[tile] = 0f;
        }

        // Obtener las unidades actuales
        units = new List<Unidad>(
            FindObjectsByType<Unidad>(
            FindObjectsSortMode.None
            )
        );
        Debug.Log("UNIDADES ENCONTRADAS: " + units.Count);

        // Calcular la influencia de cada unidad
        foreach (Unidad unit in units)
        {
            ApplyUnitInfluence(unit, tiles);
        }

        VisualizarMapa();
    }

    private void ApplyUnitInfluence(
        Unidad unit,
        List<Tile> tiles)
    {
        Vector2Int unitPosition = unit.Posicion;

        float force = unit.ObtenerDatos().alcance; //Usa el alcance como potencia / fuerza

        Debug.Log("Unidad: " + unit.name);
        Debug.Log("Equipo: " + unit.owner);
        Debug.Log("Posición: " + unitPosition);
        Debug.Log("Alcance / fuerza: " + force);

        foreach (Tile tile in tiles)
        {
            Vector2Int tilePosition = new Vector2Int(
                Mathf.RoundToInt(tile.transform.position.x),
                Mathf.RoundToInt(tile.transform.position.y)
            );

            // Distancia de Manhattan
            int distance =
                Mathf.Abs(unitPosition.x - tilePosition.x) +
                Mathf.Abs(unitPosition.y - tilePosition.y);

            // Fórmula de influencia
            float influence = 0f;

            if (distance < MAX_INFLUENCE_DISTANCE)
            {
                influence = force *
                    (1f - (float)distance / MAX_INFLUENCE_DISTANCE);
            }

            // Acumular influencia según el equipo
            if (unit.owner == TurnType.Player)
            {
                playerInfluence[tile] += influence;
            }
            else if (unit.owner == TurnType.AI)
            {
                aiInfluence[tile] += influence;
            }
        }
    }

    public float GetSecurity(Tile tile)
    {
        return playerInfluence[tile] - aiInfluence[tile]; //De momento lo resta y puede salir negativo
    }

   private void VisualizarMapa()
    {
        foreach (Tile tile in gridManager.GetAllTiles())
        {
            float player = playerInfluence[tile];
            float ai = aiInfluence[tile];

            // Comprobar si cada equipo tiene influencia
            bool hasPlayer = player > 0;
            bool hasAI = ai > 0;

            // Calcular opacidad individual
            float playerIntensity = Mathf.Clamp01(player / 5f);
            float aiIntensity = Mathf.Clamp01(ai / 5f);

            float playerAlpha = Mathf.Lerp(0.5f, 1f, playerIntensity);
            float aiAlpha = Mathf.Lerp(0.5f, 1f, aiIntensity);

            // Obtener colores
            Color playerColor = tile.GetPlayerHighlightColor();
            playerColor.a = playerAlpha;

            Color enemyColor = tile.GetAIHighlightColor();
            enemyColor.a = aiAlpha;

            // Aplicar colores
            tile.SetPlayerInfluenceColor(playerColor);
            tile.SetAIInfluenceColor(enemyColor);

            // Activar o desactivar cada capa
            tile.SetPlayerInfluence(hasPlayer);
            tile.SetAIInfluence(hasAI);
        }
    }

    public void SuscribirUnidades()
    {
        Unidad[] unidades = FindObjectsByType<Unidad>(
            FindObjectsSortMode.None
        );

        foreach (Unidad unidad in unidades)
        {
            unidad.AlMoverse -= AlMoverseUnidad;
            unidad.AlMoverse += AlMoverseUnidad;
        }
    }

private void AlMoverseUnidad(Unidad unidad)
    {
        RecalculateInfluenceMap();
    }
}
