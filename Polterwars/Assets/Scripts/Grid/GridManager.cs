using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform camera;

    private Dictionary<Vector2, Tile> tiles;

    public List<Refineria> refinerias;
    [SerializeField] private GameObject refineria_prefab;
    

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

        Generar_Refinerias();

        camera.position = new Vector3((float)width/2 - 0.5f, (float)height/2 - 0.5f, -10);
    }

    private void Generar_Refinerias() {
        int random_number = Random.Range(1, 5);

        for(int i = 0; i <= random_number; i++) {
            Tile tile = get_random_tile();
            while(tile.terrainType != TerrainType.Suelo) {
                tile = get_random_tile();
            }

            Vector2 posicion = new Vector2(
                tile.transform.position.x,
                tile.transform.position.y
            );

            var spawnedRefineria = Instantiate(refineria_prefab, posicion, Quaternion.identity);
        }

    }

    public Tile GetTile(Vector2 pos) {
        if(tiles.TryGetValue(pos, out var tile)) {
            return tile;
        }

        //print("No hay una celda en esa posicion");
        return null;
    }

    public List<Tile> GetAllTiles()
    {
        return new List<Tile>(tiles.Values);
    }

    public Tile get_random_tile() {
        int x = Random.Range(0, width);
        int y = Random.Range(0, height);

        return tiles[new Vector2(x,y)];

    }
}
