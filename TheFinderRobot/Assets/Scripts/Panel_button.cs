using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel_button : MonoBehaviour
{
    public GameObject nxt;
    public GameObject prv;

    public void next(){
        bool next = false;
        foreach (Transform child in transform) {
            if (next) {
                child.gameObject.SetActive(true);
                if (child.gameObject.name == "1")
                    prv.SetActive(false);
                else if (child.gameObject.name == "97")
                    nxt.SetActive(false);
                else {
                    prv.SetActive(true);
                    nxt.SetActive(true);
                }
                break;
            }
            if (child.gameObject.activeSelf) {
                child.gameObject.SetActive(false);
                next = true;
            }
        }
    }

    public void prev() {
        Transform buff = null;
        foreach (Transform child in transform) {
            if (child.gameObject.activeSelf) {
                child.gameObject.SetActive(false);
                buff.gameObject.SetActive(true);
                if (buff.gameObject.name == "1")
                    prv.SetActive(false);
                else if (buff.gameObject.name == "97")
                    nxt.SetActive(false);
                else {
                    prv.SetActive(true);
                    nxt.SetActive(true);
                }
                break;
            }
            buff = child;
        }
    }
}
