using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform camera;

    private Dictionary<Vector2, Tile> tiles;
    

    void Awake() {
        GenerateGrid();
    }

    void GenerateGrid() {
        tiles = new Dictionary<Vector2, Tile>();
        for (int i = 0; i < width; i++) {
            for (int j = 0; j < height; j++) {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(i, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i}{j}";
                //prueba
                TerrainType terreno = TerrainType.Suelo;

                if (i == 2 && j == 2)
                {
                    terreno = TerrainType.Caja;
                }
                else if (i == 3 && j == 2)
                {
                    terreno = TerrainType.Alfombra;
                }
                else if (i == 2 && j == 3)
                {
                    terreno = TerrainType.Gotera;
                }

                spawnedTile.SetTerrain(terreno);
                //prueba
                bool isOffset = (i + j) % 2 == 1;
                spawnedTile.setTileColor(isOffset);

                tiles[new Vector2(i, j)] = spawnedTile;
            }
        }

        camera.position = new Vector3((float)width/2 - 0.5f, (float)height/2 - 0.5f, -10);
    }

    public Tile GetTile(Vector2 pos) {
        if(tiles.TryGetValue(pos, out var tile)) {
            return tile;
        }

        print("No hay una celda en esa posicion");
        return null;
    }

    public List<Tile> GetAllTiles()
    {
        return new List<Tile>(tiles.Values);
    }
}
