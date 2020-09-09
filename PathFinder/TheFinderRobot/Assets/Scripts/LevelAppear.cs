using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LevelAppear : MonoBehaviour
{
    [SerializeField] private GameObject Levelpanel;
    [SerializeField] private GameObject Educatepanel;
    public GameObject nxt;
    public GameObject prv;
    [SerializeField] public GameObject menu;
    private GameObject saving;
    private Savegame save;
    private Save sv;

    private void Awake() {
        if (MaplevelChose.quit == true) {
            menu.gameObject.SetActive(false);
            int sm = Savegame.sv.mapNum;
            if (Savegame.sv.mapNum <= 0)
                sm = 1;
            GameObject need = null;
            foreach (Transform child in Levelpanel.transform) {
                Debug.Log(sm);
                Debug.Log(child.name);
                if (Convert.ToInt32(child.gameObject.name) > sm) {
                    need.SetActive(true);
                    if (need.name == "1")
                        prv.SetActive(false);
                    else if (need.name == "97")
                        nxt.SetActive(false);
                    foreach (Transform chill in need.transform) {
                        Color color = chill.GetComponent<Image>().color;
                        color.a = 1;
                        chill.GetComponent<Image>().color = color;
                        if (chill.GetChild(0).gameObject.activeSelf) {
                            Color colora = chill.GetChild(0).GetComponent<Text>().color;
                            colora.a = 1;
                            chill.GetChild(0).GetComponent<Text>().color = colora;
                        }
                    }
                    break;
                }
                need = child.gameObject;
            }
            Levelpanel.gameObject.SetActive(true);
        }
    }

    public void Getlevel() {
        // if (!Savegame.sv.Education) {
        //     Debug.Log("NOT EDUCATE");
        //     MaplevelChose.map_number = Savegame.sv.mapNum;
        //     SceneManager.LoadScene("Game", LoadSceneMode.Single);
        // }
        //menu.gameObject.SetActive(false);
        GameObject need = null;
        int sm = Savegame.sv.mapNum;
        if (Savegame.sv.mapNum < 0) {
            sm = 1;
        }
        foreach (Transform child in Levelpanel.transform) {
            if (Convert.ToInt32(child.gameObject.name) > sm) {
                if (need != null) {
                    need.SetActive(true);
                    if (need.name == "1")
                        prv.SetActive(false);
                    else if (need.name == "97")
                        nxt.SetActive(false);
                    
                    foreach (Transform chill in need.transform) {
                        Color color = chill.GetComponent<Image>().color;
                        color.a = 1;
                        chill.GetComponent<Image>().color = color;
                        if (chill.GetChild(0).gameObject.activeSelf) {
                            Color colora = chill.GetChild(0).GetComponent<Text>().color;
                            colora.a = 1;
                            chill.GetChild(0).GetComponent<Text>().color = colora;
                        }
                    }
                    break;
                }
            }
            need = child.gameObject;
        }
        Levelpanel.gameObject.SetActive(true);
    }

    public void Backinmenu() {
        if (Levelpanel.activeInHierarchy)
            Levelpanel.gameObject.SetActive(false);
        if (Educatepanel.activeInHierarchy)
            Educatepanel.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void inEducate() {
        //menu.gameObject.SetActive(false);
        Educatepanel.gameObject.SetActive(true);
    }

    //public void 


}
