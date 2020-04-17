using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inputbuttons : MonoBehaviour
{
    [SerializeField] public static int numer = 6;
    public Button[] button_list = new Button[numer];
    public bool touch;
    public int button = 0;
    public int func = 0;
    [SerializeField] private Image frame;


    private bool move;


    private void Awake() {
        for (int i = 0; i < numer; i++) {
            button_list[i].gameObject.SetActive(true);
        }
        touch = true;
        button = 0;
        func = 0;
        button_list[0].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 230);
    }

    private void Update() {
        if (move) {
            frame.transform.position = Vector3.MoveTowards (frame.transform.position, button_list[button].transform.position, Time.deltaTime*5000f);
            if (frame.transform.position == button_list[button].transform.position)
                move = false;
        }
    }

    private void firstbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(210.4F, 193.8F);
        button = 0;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 230);
    }

    private void secondbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(210.4F, 193.8F);
        button = 1;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 230);
    }

    private void threebutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(210.4F, 193.8F);
        button = 2;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 230);
    }

    private void fourbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(210.4F, 193.8F);
        button = 3;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 230);
    }
    private void fivebutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(210.4F, 193.8F);
        button = 4;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 230);
    }
    private void sixbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(210.4F, 193.8F);
        button = 5;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2(250, 230);

    }

}
