using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAppear : MonoBehaviour
{
    [SerializeField] private GameObject Levelpanel;
    [SerializeField] private GameObject menu;

    public void Getlevel() {
        menu.gameObject.SetActive(false);
        Levelpanel.gameObject.SetActive(true);
    }
}
