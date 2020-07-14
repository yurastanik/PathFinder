using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelAppear : MonoBehaviour
{
    [SerializeField] private GameObject Levelpanel;
    [SerializeField] private GameObject Educatepanel;
    [SerializeField] private GameObject menu;
    private GameObject saving;
    private Savegame save;
    private Save sv;

    private void Awake() {
        if (MaplevelChose.quit == true) {
                menu.gameObject.SetActive(false);
                Levelpanel.gameObject.SetActive(true);
        }
    }

    public void Getlevel() {
        if (!Savegame.sv.Education) {
            if (!Savegame.sv.Chapter1) {
                Debug.Log(gameObject.name);
                MaplevelChose.map_number = -1;
                SceneManager.LoadScene("Test_Buttons", LoadSceneMode.Single);
            }

            //Savegame.sv.Education = true;
            Debug.Log("change");
        }
        else {
            Debug.Log("already true");
        }
        //save = saving.GetComponent<Savegame>();
        //sv = saving.GetComponent<Save>();
        menu.gameObject.SetActive(false);
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
        menu.gameObject.SetActive(false);
        Educatepanel.gameObject.SetActive(true);
    }

    //public void 


}
