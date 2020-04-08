using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapLoader : MonoBehaviour {

    [SerializeField] private string mapName;
    [SerializeField] private GameObject columnPrefab;

    private StreamReader stream;
    private string path;
    private Map loadedMap;

    private void Start() {
        path = Application.dataPath + "/Maps/" + mapName + ".json";
        stream = new StreamReader(path);
        loadedMap = JsonUtility.FromJson<Map>(stream.ReadToEnd());
        RenderMap(loadedMap.map, loadedMap.mapWidth);
    }

    public Map GetMap() {
        return loadedMap;
    }

    public void OnMapUpdate(int[,] newMap) {
        loadedMap.map = TwoDToOneDArray(newMap);
        RenderMap(loadedMap.map, loadedMap.mapWidth);
    }

    private void RenderMap(int[] map, int width) {
        int rows = map.Length / width;
        int col = 0;

        foreach (Transform child in transform)
            GameObject.Destroy(child);
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < width; x++) {
                col = map[(y * width) + x];
                if (col != 0)
                    CreateColumn(new Vector3Int(x * 2, 0, -(y * 2)), col);
            }
        }
    }

    private void CreateColumn(Vector3Int pos, int col) {
        GameObject newChild = GameObject.Instantiate(columnPrefab, transform);
        MeshRenderer topRend;

        newChild.transform.position = pos;
        Transform[] childs = newChild.GetComponentsInChildren<Transform>();
        for (int i = 0; i < childs.Length; i++) {
            if (childs[i].name == "Top") {
                topRend = childs[i].GetComponent<MeshRenderer>();
                topRend.material.color = GameColors.GetColor(col);
                break;
            }
        }
    }

    public static int[,] OneDToTwoDArray(int[] map, int width) {
        int rows = map.Length / width;
        int[,] res = new int[rows, width];

        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < width; x++) {
                res[y, x] = map[(y * width) + x];
            }
        }
        return res;
    }

    public static int[] TwoDToOneDArray(int[,] map) {
        int[] res = new int[map.Length];
        int i = 0;

        for (int y = 0; y < map.GetLength(0); y++) {
            for (int x = 0; x < map.GetLength(1); x++) {
                res[i] = map[y, x];
                i++;
            }
        }
        return res;
    }

}

public class Map {
    public int[] map;
    public int mapWidth;
    public int[] moves;
    public Vector2Int startPos;
    public string direction;
}
