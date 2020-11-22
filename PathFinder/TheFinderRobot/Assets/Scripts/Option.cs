using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {
    [SerializeField] private GameObject langPanel;
    [SerializeField] private Button music_but;
    private Sprite spr1, spr2, spr3 ,spr4, spr5;
    [SerializeField] private Image flag;
    [SerializeField] private Langgame langclass;


    private void Start() {
        spr1 = Resources.Load<Sprite>("Sprites/menu/music_on");
        spr2 = Resources.Load<Sprite>("Sprites/menu/off_music");
        spr3 = Resources.Load<Sprite>("Sprites/menu/Russia-icon");
        spr4 = Resources.Load<Sprite>("Sprites/menu/us-flag-icon-20");
        spr5 = Resources.Load<Sprite>("Sprites/menu/ukraine");

        if (Savegame.firstEntry) {
            langPanel.gameObject.SetActive(true);
            Debug.Log("Swich (********************************************");
        }
        if (Savegame.sv.Languages == (int) Languages.Russion)
            flag.GetComponent<Image>().sprite = spr3;
        else if (Savegame.sv.Languages == (int) Languages.Ukraine)
            flag.GetComponent<Image>().sprite = spr5;
        else
            flag.GetComponent<Image>().sprite = spr4;
    }

    public void lang_button() {
        langPanel.gameObject.SetActive(true);
    }

    public void music() {
        if (Savegame.sv.music == true) {
            // Debug.Log("викл");
            music_but.GetComponent<Image>().sprite = spr2;
            Savegame.sv.music = false;
            GameObject.FindGameObjectsWithTag("Music")[0].GetComponent<MusicController>().ChangeVolume(0);
        }
        else {
            // Debug.Log("вкл");
            music_but.GetComponent<Image>().sprite = spr1;
            Savegame.sv.music = true;
            GameObject.FindGameObjectsWithTag("Music")[0].GetComponent<MusicController>().ChangeVolume(0.5f);
        }
    }

    public void choose_rus() {
        if (Savegame.sv.Languages != (int) Languages.Russion) {
            Savegame.sv.Languages = (int) Languages.Russion;
            flag.GetComponent<Image>().sprite = spr3;
            langclass.load_file();
        }
        langPanel.gameObject.SetActive(false);
    }

    public void choose_english() {
        if (Savegame.sv.Languages != (int) Languages.English) {
            Savegame.sv.Languages = (int) Languages.English;
            flag.GetComponent<Image>().sprite = spr4;
            langclass.load_file();
        }
        langPanel.gameObject.SetActive(false);
    }
    
    public void choose_ukraine() {
        if (Savegame.sv.Languages != (int) Languages.Ukraine) {
            Savegame.sv.Languages = (int) Languages.Ukraine;
            flag.GetComponent<Image>().sprite = spr5;
            langclass.load_file();
        }
        langPanel.gameObject.SetActive(false);
    }

    public void back() {

        langPanel.gameObject.SetActive(false);
    }
}
