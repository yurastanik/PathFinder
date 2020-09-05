using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MapLoader : MonoBehaviour {

    [SerializeField] private GameObject columnPrefab = null;
    [SerializeField] private GameObject starPrefab = null;

    private StreamReader stream;
    private string path;
    private Map loadedMap;
    public Panel_button panel;
    [SerializeField] private Camera camer;
    [SerializeField] private Education education;

    private void Awake() {
        MapNext(Savegame.sv.mapNum);
        if (Savegame.sv.mapNum != MaplevelChose.map_number) {
            MaplevelChose.map_number = Savegame.sv.mapNum;
            if (Savegame.sv.lastNum < Savegame.sv.mapNum)
                Savegame.sv.lastNum = Savegame.sv.mapNum;
        }
        MaplevelChose.getsave = false;
    }

    public void MapNext(int mapNum) {
        if (Savegame.sv.mapNum != mapNum) {
            Savegame.sv.mapNum = mapNum;
            if (Savegame.sv.lastNum < Savegame.sv.mapNum)
                Savegame.sv.lastNum = Savegame.sv.mapNum;
        }
        var jsonTextFile = Resources.Load<TextAsset>("Maps/Map" + mapNum);
        string tileFile = jsonTextFile.text;
        loadedMap = JsonUtility.FromJson<Map>(tileFile);
        RenderMap(loadedMap.map, loadedMap.targets, loadedMap.mapWidth);
        camer.transform.position = new Vector3(loadedMap.cameraPos.x, loadedMap.cameraPos.y, loadedMap.cameraPos.z); 
        camer.orthographicSize = loadedMap.cameraSize;
        if (mapNum < 0) {
            if (mapNum == -1) {
                education.FirstChapt(0);
            }
            else if (mapNum == -3) {
                education.FirstChapt(1);
            }
            else if (mapNum == -5) {
                education.FirstChapt(2);
            }
            else if (mapNum == -9) {
                education.FirstChapt(3);
            }
        }
    }

    public Map GetMap() {
        return loadedMap;
    }

    public void OnMapUpdate(int[,] newMap = null, int[,] newTargs = null) {
        int[] mapa = loadedMap.map;
        int[] targs = loadedMap.targets;
        if (newMap != null)
            mapa = TwoDToOneDArray(newMap);
        if (newTargs != null)
            targs = TwoDToOneDArray(newTargs);
        RenderMap(mapa, targs, loadedMap.mapWidth);
    }

    private void RenderMap(int[] map, int[] targets, int width) {
        int rows = map.Length / width;
        int rows_star = targets.Length / 2;
        int col = 0;
        foreach (Transform child in transform) 
            GameObject.Destroy(child.gameObject);
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < width; x++) {
                col = map[(y * width) + x];
                if (col != 0)
                    CreateColumn(new Vector3Int(x * 2, 0, y * -2), col);
            }
        }
        for (int x = 0; x < rows_star; x++) {
            int[,] targeti = OneDToTwoDArray(targets, 2);
            CreateStar(new Vector3(targeti[x, 1] * 2, (float) 0.2, targeti[x, 0] * -2));
        }
    }

    private void CreateStar(Vector3 pos) {
        GameObject newChild = GameObject.Instantiate(starPrefab, transform);
        newChild.transform.position = pos;
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
    public int[] colors;
    public int[] movesf1;
    public int[] movesf2;
    public int[] movesf3;
    public int[] movesf4;
    public int[] movesf5;
    public int[] movesf6;
    public int[] targets;
    public float cameraSize;
    public Vector2Int startPos;
    public Vector3 cameraPos;
    public string direction;
}
