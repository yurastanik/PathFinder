﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {
    [SerializeField] private GameObject langPanel;
    [SerializeField] private Button music_but;
    private Sprite spr1, spr2, spr3 ,spr4;
    [SerializeField] private Image flag;


    private void Start() {
        spr1 = Resources.Load<Sprite>("Sprites/menu/music_on");
        spr2 = Resources.Load<Sprite>("Sprites/menu/off_music");
        spr3 = Resources.Load<Sprite>("Sprites/menu/Russia-icon");
        spr4 = Resources.Load<Sprite>("Sprites/menu/us-flag-icon-20");

        if (Savegame.sv.Languages == -1)
            langPanel.gameObject.SetActive(true);
        else
            Debug.Log("option , not first");
        if (Savegame.sv.Languages == (int) Languages.Russion)
            flag.GetComponent<Image>().sprite = spr3;
        else
            flag.GetComponent<Image>().sprite = spr4;
    }

    public void lang_button() {
        langPanel.gameObject.SetActive(true);
    }

    public void music() {
        if (Savegame.sv.music == true) {
            Debug.Log("викл");
            music_but.GetComponent<Image>().sprite = spr2;
            Savegame.sv.music = false;
        }
        else {
            Debug.Log("вкл");
            music_but.GetComponent<Image>().sprite = spr1;
            Savegame.sv.music = true;
        }
    }

    public void choose_rus() {
        Savegame.sv.Languages = (int) Languages.Russion;
        flag.GetComponent<Image>().sprite = spr3;
        langPanel.gameObject.SetActive(false);
    }

    public void choose_english() {
        Savegame.sv.Languages = (int) Languages.English;
        flag.GetComponent<Image>().sprite = spr4;
        langPanel.gameObject.SetActive(false);
    }
    
}