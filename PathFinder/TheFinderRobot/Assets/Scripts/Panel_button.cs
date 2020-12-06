using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Panel_button : MonoBehaviour
{
    public GameObject nxt;
    public GameObject prv;
    public Sprite[] emptis;
    bool flg = true;

    public void Awake() {
        unlock_to(Savegame.sv.lastNum);
    }

    public void next(){
        if (flg) {
            flg = false;
            bool next = false;
            Transform old = null;
            foreach (Transform child in transform) {
                if (next) {
                    StartCoroutine(move_left(old, child));
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
                    old = child;
                    next = true;
                }
            }
        }
    }

    public void prev() {
        if (flg) {
            flg = false;
            Transform buff = null;
            foreach (Transform child in transform) {
                if (child.gameObject.activeSelf) {
                    StartCoroutine(move_right(child, buff));
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

    public void unlock_to(int last_card) {
        for (int card = 1; card <= last_card; card++) {
            foreach (Transform child in transform) {
                if (card >= Int16.Parse(child.name)) {
                    foreach (Transform childe in child) {
                        if (Int16.Parse(childe.GetChild(0).GetComponent<Text>().text) == card) {
                            childe.GetComponent<Image>().sprite = emptis[new System.Random().Next(0, emptis.Length)];
                            childe.GetChild(0).gameObject.SetActive(true);
                            Color buff = childe.GetChild(0).GetComponent<Text>().color;
                            buff.a = 1;
                            childe.GetChild(0).GetComponent<Text>().color = buff;
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    public IEnumerator move_left(Transform old_obj, Transform new_obj) {
        new_obj.gameObject.SetActive(true);
        for (float i = 0; i >= -1800; i-=200) {
            new_obj.GetComponent<RectTransform>().localPosition = new Vector3(i+1800, 0, 0);
            old_obj.GetComponent<RectTransform>().localPosition = new Vector3(i, 0, 0);
            visibiling(old_obj, new_obj);
            yield return new WaitForSeconds(0.05f);
        }
        old_obj.gameObject.SetActive(false);
        old_obj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        flg = true;
    }

    public IEnumerator move_right(Transform old_obj, Transform new_obj) {
        new_obj.gameObject.SetActive(true);
        for (float i = 0; i <= 1800; i+=200) {
            new_obj.GetComponent<RectTransform>().localPosition = new Vector3(i-1800, 0, 0);
            old_obj.GetComponent<RectTransform>().localPosition = new Vector3(i, 0, 0);
            visibiling(old_obj, new_obj);
            yield return new WaitForSeconds(0.05f);
        }
        old_obj.gameObject.SetActive(false);
        old_obj.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        flg = true;
    }

    public void visibiling(Transform old, Transform newo) {
        foreach (Transform chill in old) {
            Color col = chill.GetComponent<Image>().color;
            col.a -= 0.111111f;
            chill.GetComponent<Image>().color = col;
            if (chill.GetChild(0).gameObject.activeSelf) {
                Color color = chill.GetChild(0).GetComponent<Text>().color;
                color.a -= 0.111111f;
                chill.GetChild(0).GetComponent<Text>().color = color;
            }
        }
        foreach (Transform chill in newo) {
            Color col = chill.GetComponent<Image>().color;
            col.a += 0.111111f;
            chill.GetComponent<Image>().color = col;
            if (chill.GetChild(0).gameObject.activeSelf) {
                Color color = chill.GetChild(0).GetComponent<Text>().color;
                color.a += 0.111111f;
                chill.GetChild(0).GetComponent<Text>().color = color;
            }
        }
    }
}
