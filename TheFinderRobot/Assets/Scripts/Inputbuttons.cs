using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inputbuttons : MonoBehaviour
{
    public int numer = 0;
    public Button[] button_list = new Button[6];
    public int button = 0;
    public int func = 0;
    [SerializeField] private Image frame;
    [SerializeField] private GameObject choosebutton;


    private bool move;


    private void Awake() {
        numer = choosebutton.GetComponent<Choosebutton>().func_num[0];
        //button_list = new Button[numer];
        for (int i = 0; i < numer; i++) {
            button_list[i].gameObject.SetActive(true);
        }
        button = 0;
        func = 0;
        button_list[0].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[0].GetComponent<RectTransform>().rect.width)*1.1F, button_list[0].GetComponent<RectTransform>().rect.height*1.1F);
    }

    private void Update() {
        if (move) {
            frame.transform.position = Vector3.MoveTowards (frame.transform.position, button_list[button].transform.position, Time.deltaTime*5000f);
            if (frame.transform.position == button_list[button].transform.position)
                move = false;
        }
    }

    public void firstbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/1.1F, button_list[button].GetComponent<RectTransform>().rect.height/1.1F);
        button = 0;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)*1.1F, button_list[button].GetComponent<RectTransform>().rect.height*1.1F);
    }

    private void secondbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/1.1F, button_list[button].GetComponent<RectTransform>().rect.height/1.1F);
        button = 1;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)*1.1F, button_list[button].GetComponent<RectTransform>().rect.height*1.1F);
    }

    private void threebutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/1.1F, button_list[button].GetComponent<RectTransform>().rect.height/1.1F);
        button = 2;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)*1.1F, button_list[button].GetComponent<RectTransform>().rect.height*1.1F);
    }

    private void fourbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/1.1F, button_list[button].GetComponent<RectTransform>().rect.height/1.1F);
        button = 3;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)*1.1F, button_list[button].GetComponent<RectTransform>().rect.height*1.1F);
    }
    private void fivebutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/1.1F, button_list[button].GetComponent<RectTransform>().rect.height/1.1F);
        button = 4;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)*1.1F, button_list[button].GetComponent<RectTransform>().rect.height*1.1F);
    }
    private void sixbutton() {
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/1.1F, button_list[button].GetComponent<RectTransform>().rect.height/1.1F);
        button = 5;
        move = true;
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)*1.1F, button_list[button].GetComponent<RectTransform>().rect.height*1.1F);
    }

}
