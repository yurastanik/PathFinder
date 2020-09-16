using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Savegame : MonoBehaviour
{

    public static Save sv = new Save();
    public LevelAppear la;

    private void Awake() {
        if (MaplevelChose.getsave) {
            
            DontDestroyOnLoad(this.gameObject);
            if (!PlayerPrefs.HasKey("Save")) {
#if UNITY_EDITOR
                Debug.Log("first game");
#endif
                sv.FirstEntry = true;
                sv.Education = false;
                sv.Chapter1 = false;
                sv.Chapter2 = false;
                sv.Chapter3 = false;
                sv.time = System.DateTime.Now.ToString();
            }
            else {
                //Debug.Log("NOT FIRST");

                sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
                                #if UNITY_EDITOR
                    Debug.Log("time in awake " + sv.time);
                #endif
                 DateTime realtime;
                if (sv.time.Length > 0)
                    realtime = System.DateTime.Parse(sv.time);
                else {
                    realtime =  System.DateTime.Now;
                    sv.time = realtime.ToString();
                    Debug.Log("нету времени єто ошибка ");
                }


                if (realtime.AddDays(1) < System.DateTime.UtcNow) {
        #if UNITY_EDITOR
                    Debug.Log(Savegame.sv.time);
                    Debug.Log("After add " + realtime.AddDays(1));
        #endif
                    Savegame.sv.hint = Savegame.sv.hint + 3;
                    Savegame.sv.time = System.DateTime.Now.ToString();
                }
                else
                    Debug.Log("TIME LESS");
                if (sv.mapNum == 0)
                    la.Getlevel();
                else {
                    SceneManager.LoadScene("Game", LoadSceneMode.Single);
                }
            }
        }

    }

#if UNITY_ANDROID || UNITY_IOS
    void OnApplicationPause(bool pause) {
       if (pause) {
            Debug.Log("SAving Pause");
            Debug.Log(sv.mapNum);
            sv.FirstEntry = false;
            if (sv.moves1 != null)
                sv.movesf1 = MapLoader.TwoDToOneDArray(sv.moves1);
            if (sv.moves2 != null)
                sv.movesf2 = MapLoader.TwoDToOneDArray(sv.moves2);
            if (sv.moves3 != null)
                sv.movesf3 = MapLoader.TwoDToOneDArray(sv.moves3);
            if (sv.moves4 != null)
                sv.movesf4 = MapLoader.TwoDToOneDArray(sv.moves4);
            if (sv.moves5 != null)
                sv.movesf5 = MapLoader.TwoDToOneDArray(sv.moves5);
            PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
        }
    }
#endif

    void OnApplicationQuit() {
#if UNITY_EDITOR
        Debug.Log("SAving Quit");
        Debug.Log(sv.mapNum);
#endif
        sv.FirstEntry = false;
        if (sv.moves1 != null) 
            sv.movesf1 = MapLoader.TwoDToOneDArray(sv.moves1);
        if (sv.moves2 != null)
            sv.movesf2 = MapLoader.TwoDToOneDArray(sv.moves2);
        if (sv.moves3 != null)
            sv.movesf3 = MapLoader.TwoDToOneDArray(sv.moves3);
        if (sv.moves4 != null)
            sv.movesf4 = MapLoader.TwoDToOneDArray(sv.moves4);
        if (sv.moves5 != null)
            sv.movesf5 = MapLoader.TwoDToOneDArray(sv.moves5);
        Debug.Log("Time in quit " + sv.time);
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
        //PlayerPrefs.DeleteAll();
    }

    void OnApplicationFocus(bool hasFocus) {

        Debug.Log("SAving Focus");
        Debug.Log(sv.mapNum);

        sv.FirstEntry = false;
        if (sv.moves1 != null) 
            sv.movesf1 = MapLoader.TwoDToOneDArray(sv.moves1);
        if (sv.moves2 != null)
            sv.movesf2 = MapLoader.TwoDToOneDArray(sv.moves2);
        if (sv.moves3 != null)
            sv.movesf3 = MapLoader.TwoDToOneDArray(sv.moves3);
        if (sv.moves4 != null)
            sv.movesf4 = MapLoader.TwoDToOneDArray(sv.moves4);
        if (sv.moves5 != null)
            sv.movesf5 = MapLoader.TwoDToOneDArray(sv.moves5);
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
    }
}

public enum Languages {
    English,
    Russion
}

[SerializeField]
public class Save {
    public bool FirstEntry;
    public bool Education;
    public bool Chapter1;
    public bool Chapter2;
    public bool Chapter3;
    public int mapNum = 0;
    public int lastNum = 1;
    public int[,] moves1;
    public int[,] moves2;
    public int[,] moves3;
    public int[,] moves4;
    public int[,] moves5;
    public int[] movesf1;
    public int[] movesf2;
    public int[] movesf3;
    public int[] movesf4;
    public int[] movesf5;
    public int speed = 2;
    public int hint = 100;
    public string time;
    public int Languages = -1;
    public bool music = true;
}

