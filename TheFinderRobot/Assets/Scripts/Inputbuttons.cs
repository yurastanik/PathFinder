using UnityEngine;
using UnityEngine.UI;

public class Inputbuttons : MonoBehaviour
{

    public Button[] button_list = new Button[8];
    [SerializeField] private Button[] func_list = new Button[6];
    public int button = 0;
    public int func = 0;
    [SerializeField] private Image button_cancel;
    [SerializeField] private Image button_frame;
    [SerializeField] private Image func_frame;
    [SerializeField] private GameObject choosebutton;
    [SerializeField] private GameObject playbutton;
    private int func_num = 1;

    private bool move_btn;
    private bool move_func;

    private int buton_num = 0;
    private int next_buton_num = 0;
    private float delta_transofrm_x= 0;


    private float firs_pos = 0;
    private float width = 0;
    private float height = 0;

    private float width_frame = 0;
    private float height_frame = 0;

    private void Awake() {
        Debug.Log("button " + button);
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
        func_num = choosebutton.GetComponent<Choosebutton>().func_num.Count;
        for (int i = 0; i < func_num; i++)
            func_list[i].gameObject.SetActive(true);
        buton_num = choosebutton.GetComponent<Choosebutton>().func_num[0];
        if (buton_num == 7) {
            seven_activate();
        }
        else if (buton_num == 8) {
            eight_activate();
        }
        else {
            for (int i = 0; i < buton_num; i++) {
                button_list[i].gameObject.SetActive(true);
            }
        }
        button = 0;
        func = 0;
        button_list[0].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[0].GetComponent<RectTransform>().rect.width)*1.2F, button_list[0].GetComponent<RectTransform>().rect.height*1.2F);
        func_list[0].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[0].GetComponent<RectTransform>().rect.width)*1.2F, func_list[0].GetComponent<RectTransform>().rect.height*1.2F);
    }

    private void ReUpdate() {
        func_list[func].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[func].GetComponent<RectTransform>().rect.width)/1.2F, func_list[func].GetComponent<RectTransform>().rect.height/1.2F);
        button_list[button].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button].GetComponent<RectTransform>().rect.width)/1.2F, button_list[button].GetComponent<RectTransform>().rect.height/1.2F);
        move_btn = true;
        move_func = true;
    }

    private void Update() {
        if (move_btn) {
            button_frame.transform.position = Vector3.MoveTowards(button_frame.transform.position, button_list[button].transform.position, Time.deltaTime*2500f);
            if ((int) button_frame.transform.position.x == (int) button_list[button].transform.position.x) {
                move_btn = false;
            }
        }
        if (move_func) {
            func_frame.transform.position = Vector3.MoveTowards(func_frame.transform.position, func_list[func].transform.position, Time.deltaTime*2500f);
            if ((int) func_frame.transform.position.x == (int) func_list[func].transform.position.x)
                move_func = false;
        }
    }

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
        if (func_num != but) {
            func_list[func_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[func_num].GetComponent<RectTransform>().rect.width)/1.2F, func_list[func_num].GetComponent<RectTransform>().rect.height/1.2F);
            func_num = but;
            move_func = true;
            func_list[func_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((func_list[func_num].GetComponent<RectTransform>().rect.width)*1.2F, func_list[func_num].GetComponent<RectTransform>().rect.height*1.2F);
            delta_transofrm_x = firs_pos;
            button_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(width_frame, height_frame);
            for (int i = 0; i < buton_num; i++) {
                button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[0];
                button_list[i].image.color = new Color(0.7333333F, 0.7843138F, 0.8784314F, 1F);
                if (buton_num == 7 || buton_num == 8) {
                    button_list[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (width, height);
                    button_list[i].GetComponent<RectTransform>().localPosition = new Vector3(delta_transofrm_x, button_list[i].GetComponent<RectTransform>().localPosition.y, 0);
                    delta_transofrm_x += 296F;
                }               
            }
            next_buton_num = choosebutton.GetComponent<Choosebutton>().func_num[func];
            if (next_buton_num > buton_num) {
                for (int i = buton_num; i < next_buton_num; i++)
                    button_list[i].gameObject.SetActive(true);
            }
            else {
                for (int i = buton_num - 1; i > next_buton_num; i--)
                    button_list[i].gameObject.SetActive(false);
            }
            buton_num = next_buton_num;
            if (buton_num == 7)
                seven_activate();
            else if (buton_num == 8)
                eight_activate();
            if (buton_num >= 7)
                Buttonact(ref button, 0, false);
            else 
                Buttonact(ref button, 0, true);
            for (int i = 0; i < buton_num; i++) {
                if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].color == (int) Input_Class.Colors.Red)
                    button_list[i].image.color = new Color(0.8490566F, 0.2763439F, 0.2763439F, 1F);
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].color == (int) Input_Class.Colors.Blue)
                    button_list[i].image.color = new Color(0.2763439F, 0.6294675F, 0.8490566F, 1F);
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].color == (int) Input_Class.Colors.Green)
                    button_list[i].image.color = new Color(0.3551086F, 0.7169812F,0.3980731F, 1F);
                if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.left)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[6];
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.right)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[8];
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.forward)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[7];
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f1)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[1];
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f2)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[2];
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f3)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[3];
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f4)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[4];
                else if (playbutton.GetComponent<Button_play>().func[func_num].input_arr[i].direct == (int) Input_Class.Directs.f5)
                    button_list[i].image.sprite = choosebutton.GetComponent<Choosebutton>().s1[5];
            }
        }

    
    }
    private void Buttonact(ref int button_num, int button, bool flag) {
        if (button_num != button) {
            if (flag)
                button_list[button_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button_num].GetComponent<RectTransform>().rect.width)/1.2F, button_list[button_num].GetComponent<RectTransform>().rect.height/1.2F);
            button_num = button;
            move_btn = true;
            button_list[button_num].GetComponent<RectTransform>().sizeDelta = new Vector2 ((button_list[button_num].GetComponent<RectTransform>().rect.width)*1.2F, button_list[button_num].GetComponent<RectTransform>().rect.height*1.2F);
        }
    }

    private void seven_activate() {
        delta_transofrm_x = firs_pos - 36.2001F;
            for (int i = 0; i < buton_num; i++) {
                button_list[i].gameObject.SetActive(true);
                button_list[i].GetComponent<RectTransform>().sizeDelta = new Vector2 ((width)/1.2606F, height/1.255955F);
                button_list[i].GetComponent<RectTransform>().localPosition = new Vector3(delta_transofrm_x, button_list[i].GetComponent<RectTransform>().localPosition.y, 0);
                delta_transofrm_x += 260.1001F;
            }
            move_btn = true;
    }
    private void eight_activate() {
        delta_transofrm_x = firs_pos - 67;
            for (int i = 0; i < buton_num; i++) {
                button_list[i].gameObject.SetActive(true);
                button_list[i].GetComponent<RectTransform>().sizeDelta = new Vector2 (width/1.1382F, height/1.326F);
                button_list[i].GetComponent<RectTransform>().localPosition = new Vector3(delta_transofrm_x, button_list[i].GetComponent<RectTransform>().localPosition.y, 0);
                delta_transofrm_x += 232;
            }
            button_frame.GetComponent<RectTransform>().sizeDelta = new Vector2(width_frame/1.2668F, height_frame/1.325F);
            move_btn = true;
    }


}
