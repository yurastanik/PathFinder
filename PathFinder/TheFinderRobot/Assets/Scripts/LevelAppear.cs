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
    [SerializeField] private GameObject ManualOrTutorial;
    [SerializeField] private GameObject credits;
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
        // //    SceneManager.LoadScene("Game", LoadSceneMode.Single);
        // StartCoroutine(GameObject.FindGameObjectWithTag("FadeInFadeOut").GetComponent<FadeInOut>().FadeIn("Game"));
        // }
        //menu.gameObject.SetActive(false);
        GameObject need = null;
        int sm = Savegame.sv.mapNum;
        if (Savegame.sv.mapNum <= 0)
            sm = 1;
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
        menu.gameObject.SetActive(false);
    }

    IEnumerator Alfa(Transform obj) {
        Color col = obj.gameObject.GetComponent<Text>().color;
        for (float a = 0.05f; a <= 1; a += 0.05f) {
            col.a = a;
            obj.gameObject.GetComponent<Text>().color = col;
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void ShowCredits() {
        credits.SetActive(true);
        menu.gameObject.SetActive(false);
        foreach (Transform child in credits.transform) {
            if (child.gameObject.name != "Inmenu")
                StartCoroutine(Alfa(child));
        }
    }

    public void Backinmenu() {
        if (Levelpanel.activeInHierarchy) {
            Levelpanel.gameObject.SetActive(false);
            foreach (Transform child in Levelpanel.transform) {
                Debug.Log(child.gameObject.name);
                Debug.Log(!child.gameObject.name.Contains("e"));
                Debug.Log(!child.gameObject.name.Contains("--"));
                if (!child.gameObject.name.Contains("e") && !child.gameObject.name.Contains("--"))
                    child.gameObject.SetActive(false);
            }
        }
        else if (ManualOrTutorial.activeInHierarchy)
            ManualOrTutorial.gameObject.SetActive(false);
        else if (Educatepanel.activeInHierarchy)
            Educatepanel.gameObject.SetActive(false);
        else if (credits.activeInHierarchy) {
            credits.SetActive(false);
            foreach (Transform child in credits.transform) {
                if (child.gameObject.name != "Inmenu") {
                    Color unvis = child.gameObject.GetComponent<Text>().color;
                    unvis.a = 0;
                    child.gameObject.GetComponent<Text>().color = unvis;
                }
            }
        }
        menu.gameObject.SetActive(true);
    }

    public void Back() {
        Educatepanel.gameObject.SetActive(false);
        ManualOrTutorial.gameObject.SetActive(true);
    }

    public void inChoose() {
        ManualOrTutorial.gameObject.SetActive(true);
        Levelpanel.gameObject.SetActive(false);
    }

    public void inManual() {
    }


    public void inTutorial() {
        Educatepanel.gameObject.SetActive(true);
        ManualOrTutorial.gameObject.SetActive(false);
    }

    //public void 


}
