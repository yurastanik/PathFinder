using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAppear : MonoBehaviour
{
    [SerializeField] private GameObject Levelpanel;
    [SerializeField] private GameObject Educatepanel;
    [SerializeField] private GameObject menu;

    private void Awake() {
        if (MaplevelChose.quit == true) {
                menu.gameObject.SetActive(false);
                Levelpanel.gameObject.SetActive(true);
        }
    }

    public void Getlevel() {
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
