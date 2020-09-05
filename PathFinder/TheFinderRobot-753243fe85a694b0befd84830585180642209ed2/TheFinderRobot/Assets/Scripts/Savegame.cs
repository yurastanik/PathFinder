﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Savegame : MonoBehaviour
{

    public static Save sv = new Save();

    private void Awake() {
        Debug.Log("first game 1 line ");
        if (MaplevelChose.getsave) {
            DontDestroyOnLoad(this.gameObject);
            if (!PlayerPrefs.HasKey("Save")) {
//#if UNITY_EDITOR
                Debug.Log("first game");
//#endif
                sv.FirstEntry = true;
                sv.Education = false;
                sv.Chapter1 = false;
                sv.Chapter2 = false;
                sv.Chapter3 = false;
                System.DateTime time = System.DateTime.Now;
                System.DateTime time2 = System.DateTime.UtcNow;
                Debug.Log("Time " + time);
                Debug.Log("Time 2 " + time);
            }
            else {
                Debug.Log("NOT FIRST");
                sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
//#if UNITY_EDITOR
                if (sv.movesf1.Length > 0)
                    Debug.Log(sv.movesf1[0]);
//#endif
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
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
//#if UNITY_EDITOR
        Debug.Log("SAving Quit");
        Debug.Log(sv.mapNum);
//#endif
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
        PlayerPrefs.DeleteAll();
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



[SerializeField]
public class Save {
    public bool FirstEntry;
    public bool Education;
    public bool Chapter1;
    public bool Chapter2;
    public bool Chapter3;
    public int mapNum = -1;
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
    public int hint = 3;
}

