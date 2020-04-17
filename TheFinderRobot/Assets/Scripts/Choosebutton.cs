using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choosebutton : MonoBehaviour
{
    [SerializeField] private GameObject InputField;
    [SerializeField] private GameObject Btnplay;
    [SerializeField] private Button[] button_list = new Button[15];
    [SerializeField] private Image Content;
    [SerializeField] private GameObject Scrol;


    private int btn;
    private int fun;
    [SerializeField] private Sprite[] s1;



    private void Awake() {
        for (int i = 0; i < 15; i++) {
            button_list[i].gameObject.SetActive(true);
        }
        s1 = Resources.LoadAll<Sprite>("Sprites/Button/input_button");
        Content.transform.position = new Vector3(Scrol.transform.position.x*2, Content.transform.position.y, Content.transform.position.z);
        // Debug.Log(Content.transform.position.x);
        // Debug.Log(Content.transform.position.y);
        // Debug.Log(Content.transform.position.z);
    }

    public void Red_button() {
        if (InputField.GetComponent<Inputbuttons>().touch == true) {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.8490566F, 0.2763439F, 0.2763439F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Red;
        }
    }
    public void Blue_button() {
        if (InputField.GetComponent<Inputbuttons>().touch == true) {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.2763439F, 0.6294675F, 0.8490566F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Blue;
        }
    }
    public void Green_button() {
        if (InputField.GetComponent<Inputbuttons>().touch == true) {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.3551086F, 0.7169812F,0.3980731F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Green;
        }
    }
    public void Top_button() {
        if (InputField.GetComponent<Inputbuttons>().touch == true) {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[2];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.forward;
        }
    }
    public void Left_button() {
        if (InputField.GetComponent<Inputbuttons>().touch == true) {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[1];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.left;
        }
    }
    public void Right_button() {
        if (InputField.GetComponent<Inputbuttons>().touch == true) {
            btn = InputField.GetComponent<Inputbuttons>().button;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[3];
            fun = InputField.GetComponent<Inputbuttons>().func;
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.right;
        }
    }
}
