﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Levelbutton : MonoBehaviour
{
    public void choose_level() {
        // if (gameObject.GetComponent<Image>().sprite.name == "blocked");
        // else {
    //#if UNITY_EDITOR
            Debug.Log("CHOOse lvl");
            Debug.Log(gameObject.name);
    //#endif
            MaplevelChose.map_number = Int32.Parse(gameObject.transform.GetChild(0).GetComponent<Text>().text);
            Savegame.sv.mapNum = MaplevelChose.map_number;
            SceneManager.LoadScene("Game", LoadSceneMode.Single);
        // }
    }
}