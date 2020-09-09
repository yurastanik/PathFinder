using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

public class Inputbuttons : MonoBehaviour
{

    public Button[] button_list = new Button[8];
    [SerializeField] private Button[] func_list = new Button[6];

    public int button = 0;
    public int func = 0;
    [SerializeField] private Image button_cancel;
    [SerializeField] private Image button_frame;
   // [SerializeField] private Image func_frame;
    [SerializeField] private Choosebutton choosebutton;
    [SerializeField] private Button_play playbutton;
    [SerializeField] private Image Panel_input;
    private int func_num = 1;

    private bool move_btn = true;
    //private bool move_func;

    private int buton_num = 0;
    private int next_buton_num = 0;
    private float delta_transofrm_x= 0;


    private float firs_pos = 0;
    private float width = 0;
    private float height = 0;

    private float width_frame = 0;
    private float height_frame = 0;

    private float delata = 1.2F;
    private int spac = 190;
    private float delata_func = 1.2F;

    private void Awake() {
// #if UNITY_EDITOR
//         Debug.Log("button " + button);
// #endif
        FuncLoad(true);
    }

    public void FuncLoad(bool isFisrst) {
        if (!isFisrst){
            for (int i = 0; i < 6; i++) {
                func_list[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < 8; i++) {
                button_list[i].gameObject.SetActive(false);
            }
            ReUpdate();
        }
        firs_pos = button_list[0].GetComponent<RectTransform>().localPosition.x;
        width = button_list[0].GetComponent<RectTransform>().rect.width;
        height =  button_list[0].GetComponent<RectTransform>().rect.height;
        width_frame = button_frame.GetComponent<RectTransform>().rect.width;
        height_frame = button_frame.GetComponent<RectTransform>().rect.height;
        func_num = choosebutton.func_num.Count;
        for (int i = 0; i < func_num; i++) {
            if (i == 2)
                spac += 90;
            func_list[i].gameObject.SetActive(true);
            if (i > 1)
                spac += 270;
            else if (i == 0)
                spac -= 90;
        }
        buton_num = choosebutton.func_num[0];
        if (buton_num == 7) {
            seven_activate();
            delata = 1.4F;
        }
        else if (buton_num == 8) {
            eight_activate();
            delata = 1.1F;
        }
        else {
            delata = 1.2F;
            for (int i = 0; i < buton_num; i++) {
                button_list[i].gameObject.SetActive(true);
                if (i > 2 && spac > 100) {
                    spac -= 270;
                }
            }
        }
        Panel_input.GetComponent<HorizontalLayoutGroup>().padding.right = spac;
        button = 0;
        func = 0;
        button_list[0].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[0].GetComponent<RectTransform>().rect.width)*delata, button_list[0].GetComponent<RectTransform>().rect.height*delata);
        func_list[0].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[0].GetComponent<RectTransform>().rect.width)*delata_func, func_list[0].GetComponent<RectTransform>().rect.height*delata_func);
        func_list[0].GetComponent<RectTransform>().localPosition = new Vector3 (func_list[0].GetComponent<RectTransform>().localPosition.x, func_list[0].GetComponent<RectTransform>().localPosition.y + 20.5301F, func_list[0].GetComponent<RectTransform>().localPosition.z);
        func_list[0].GetComponent<Image>().color = new Color(0.8627451F, 0.8784314F, 0.7333333F, 1F);
        move_btn = false;
        Move(button_frame.transform, button_list[button].transform, 0.5F);
    }



    private void ReUpdate() {
        func_list[func].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[func].GetComponent<RectTransform>().rect.width)/delata_func, func_list[func].GetComponent<RectTransform>().rect.height/delata_func);
        func_list[func].GetComponent<RectTransform>().localPosition = new Vector3 (func_list[func].GetComponent<RectTransform>().localPosition.x, func_list[func].GetComponent<RectTransform>().localPosition.y + 20.5301F, func_list[func].GetComponent<RectTransform>().localPosition.z);
        func_list[func].GetComponent<Image>().color = new Color(0.7333333F, 0.7843137F, 0.8784314F, 1F);
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/delata, button_list[button].GetComponent<RectTransform>().rect.height/delata);
    }


    public void Move(Transform from, Transform to, float overTime)
{
    StartCoroutine(_Move(from, to, overTime));
}
    IEnumerator _Move(Transform from, Transform to, float overTime)
{
    Vector2 original = from.position;
    float timer = 0.0f;
    while (timer < overTime)
    {
        float step = Vector2.Distance(original, to.position) * (Time.deltaTime / overTime);
        from.position = Vector2.MoveTowards(from.position, to.position, step);
        timer += Time.deltaTime;
        yield return null;
    }
    move_btn = true;
}

    // private void Update() {
    //     if (move_btn) {

    //         button_frame.transform.position = Vector3.MoveTowards(button_frame.transform.position, button_list[button].transform.position, Time.deltaTime*2500f);

            //  #if UNITY_EDITOR
            //     Debug.Log("button_list[button].transform.position.x MATH = " + Math.Round( button_frame.transform.position.x, 2));
            //     Debug.Log("move frame , button_list[button].transform.position.x = " + button_list[button].transform.position.x + " button_frame.transform.position.x = " + button_frame.transform.position.x);
            // #endif
            // if ( button_frame.transform.position.x == button_list[button].transform.position.x) {
            //     move_btn = false;
            // }
 //       }
        // if (move_func) {
        //     func_frame.transform.position = Vector3.MoveTowards(func_frame.transform.position, func_list[func].transform.position, Time.deltaTime*2500f);
        //     if ((int) func_frame.transform.position.x == (int) func_list[func].transform.position.x)
        //         move_func = false;
        // }
//    }

    private void firstbutton() {
        Buttonact(ref button, 0, true);
    }

    private void secondbutton() {
        Buttonact(ref button, 1, true);
    }
    private void threebutton() {
        Buttonact(ref button, 2, true);
    }

    private void fourbutton() {
        Buttonact(ref button, 3, true);
    }
    private void fivebutton() {
        Buttonact(ref button, 4, true);
    }
    private void sixbutton() {
        Buttonact(ref button, 5, true);
    }
    public void sevenbutton() {
        Buttonact(ref button, 6, true);
    }
    public void eightbutton() {
        Buttonact(ref button, 7, true);
    }


    private void func_one() {
        FuncButton(ref func, 0);
    }
    private void func_two() {
        FuncButton(ref func, 1);
    }
    private void func_three() {
        FuncButton(ref func, 2);
    }
    private void func_four() {
        FuncButton(ref func, 3);
    }
    private void func_five() {
        FuncButton(ref func, 4);
    }
    private void func_six() {
        FuncButton(ref func, 5);
    }

    private void FuncButton(ref int func_num, int but) {
        if (func_num != but && move_btn != false) {
            func_list[func_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[func_num].GetComponent<RectTransform>().rect.width)/delata_func, func_list[func_num].GetComponent<RectTransform>().rect.height/delata_func);
            func_list[func_num].GetComponent<RectTransform>().localPosition = new Vector3 (func_list[func_num].GetComponent<RectTransform>().localPosition.x, func_list[func_num].GetComponent<RectTransform>().localPosition.y - 20.5301F, func_list[func_num].GetComponent<RectTransform>().localPosition.z);
            func_list[func_num].GetComponent<Image>().color = new Color(0.7333333F, 0.7843137F, 0.8784314F, 1F);
            func_num = but;
            func_list[func_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[func_num].GetComponent<RectTransform>().rect.width)*delata_func, func_list[func_num].GetComponent<RectTransform>().rect.height*delata_func);
            func_list[func_num].GetComponent<RectTransform>().localPosition = new Vector3 (func_list[func_num].GetComponent<RectTransform>().localPosition.x, func_list[func_num].GetComponent<RectTransform>().localPosition.y + 20.5301F, func_list[func_num].GetComponent<RectTransform>().localPosition.z);
            func_list[func_num].GetComponent<Image>().color = new Color(0.8627451F, 0.8784314F, 0.7333333F, 1F);
            button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/delata, button_list[button].GetComponent<RectTransform>().rect.height/delata);
            delta_transofrm_x = firs_pos;
            button_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(width_frame, height_frame);
            for (int i = 0; i < buton_num; i++) {
                button_list[i].image.sprite = choosebutton.s1[0];
                button_list[i].image.color = new Color(0.7333333F, 0.7843138F, 0.8784314F, 1F);
                if (buton_num == 7 || buton_num == 8) {
                    button_list[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (width, height);
                    button_list[i].GetComponent<RectTransform>().localPosition = new Vector3(delta_transofrm_x, button_list[i].GetComponent<RectTransform>().localPosition.y, 0);
                    delta_transofrm_x += 296F;
                }
            }
            next_buton_num = choosebutton.func_num[func];
            if (next_buton_num > buton_num) {
                for (int i = buton_num; i < next_buton_num; i++) {
                    button_list[i].gameObject.SetActive(true);
                    if (i > 1 && spac > 190)
                        spac -= 270;
                    else if (spac == 190)
                        spac -= 90;
                }
            }
            else {
                for (int i = buton_num - 1; i >= next_buton_num; i--) {
                    button_list[i].gameObject.SetActive(false);
                    if (i > 1)
                        spac += 270;
                }
            }
            buton_num = next_buton_num;
            if (buton_num == 7) {
                delata = 1.4F;
                seven_activate();
            }
            else if (buton_num == 8) {
                delata = 1.1F;
                eight_activate();

            }
            if (buton_num >= 7) {
            #if UNITY_EDITOR
                Debug.Log("more then 7");
            #endif
                Buttonact(ref button, 0, false);

            }
            else {
                delata = 1.2F;
                Buttonact(ref button, 0, false);
            }
            Panel_input.GetComponent<HorizontalLayoutGroup>().padding.right = spac;
            for (int i = 0; i < buton_num; i++) {
                if (playbutton.func[func_num].input_arr[i].color == (int) Input_Class.Colors.Red)
                    button_list[i].image.color = new Color(0.8490566F, 0.2763439F, 0.2763439F, 1F);
                else if (playbutton.func[func_num].input_arr[i].color == (int) Input_Class.Colors.Blue)
                    button_list[i].image.color = new Color(0.2763439F, 0.6294675F, 0.8490566F, 1F);
                else if (playbutton.func[func_num].input_arr[i].color == (int) Input_Class.Colors.Green)
                    button_list[i].image.color = new Color(0.3551086F, 0.7169812F,0.3980731F, 1F);
                if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.left)
                    button_list[i].image.sprite = choosebutton.s1[6];
                else if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.right)
                    button_list[i].image.sprite = choosebutton.s1[8];
                else if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.forward)
                    button_list[i].image.sprite = choosebutton.s1[7];
                else if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f1)
                    button_list[i].image.sprite = choosebutton.s1[1];
                else if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f2)
                    button_list[i].image.sprite = choosebutton.s1[2];
                else if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f3)
                    button_list[i].image.sprite = choosebutton.s1[3];
                else if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f4)
                    button_list[i].image.sprite = choosebutton.s1[4];
                else if (playbutton.func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f5)
                    button_list[i].image.sprite = choosebutton.s1[5];
            }
        }

    
    }
    private void Buttonact(ref int button_num, int button, bool flag) {
        Debug.Log("Buttonact");
        if ((button_num != button || flag == false) && move_btn == true) {
            if (flag)
                button_list[button_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button_num].GetComponent<RectTransform>().rect.width)/delata, button_list[button_num].GetComponent<RectTransform>().rect.height/delata);
            button_num = button;
            move_btn = false;
#if UNITY_EDITOR
        Debug.Log("make bigger");
#endif
            button_list[button_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button_num].GetComponent<RectTransform>().rect.width)*delata, button_list[button_num].GetComponent<RectTransform>().rect.height*delata);
            Move(button_frame.transform, button_list[button].transform, 0.45F);
        }
    }

    private void seven_activate() {
#if UNITY_EDITOR
        Debug.Log("SEVEN");
#endif
        delta_transofrm_x = firs_pos - 36.2001F;
            for (int i = 0; i < buton_num; i++) {
                button_list[i].gameObject.SetActive(true);
                button_list[i].GetComponent<RectTransform>().sizeDelta = new Vector2 ((width)/1.2606F, height/1.255955F);
                button_list[i].GetComponent<RectTransform>().localPosition = new Vector3(delta_transofrm_x, button_list[i].GetComponent<RectTransform>().localPosition.y, 0);
                delta_transofrm_x += 260.1001F;
                if (i > 2 && spac > 100) {
                    spac -= 270;
                }
            }
           // move_btn = true;
    }
    private void eight_activate() {
        delta_transofrm_x = firs_pos - 67;
            for (int i = 0; i < buton_num; i++) {
                button_list[i].gameObject.SetActive(true);
                button_list[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (width/1.1382F, height/1.326F);
                button_list[i].GetComponent<RectTransform>().localPosition = new Vector3(delta_transofrm_x, button_list[i].GetComponent<RectTransform>().localPosition.y, 0);
                delta_transofrm_x += 232;
                if (i > 2 && spac > 100) {
                    spac -= 270;
                }
            }
            button_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(width_frame/1.2668F, height_frame/1.325F);
            //move_btn = true;
    }


}
