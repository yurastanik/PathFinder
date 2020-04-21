using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_play : MonoBehaviour
{
    private int func_count;
    [SerializeField] GameObject button;
    Choosebutton choosebutton;
    public List <Functionclass> func = new List<Functionclass>();


    void Start() {
        choosebutton = button.GetComponent<Choosebutton>();
        func_count = choosebutton.func_num.Count;
        for (int i = 0; i < func_count; i++) {
             func.Add(new Functionclass(choosebutton.func_num[i]));
        }

    }
    public void Start_btn() {
        for (int i = 0; i < func_count; i++) {
            for (int j = 0; j < choosebutton.func_num[i]; j++)
                Debug.Log(func[i].input_arr[j].color + "and" + func[i].input_arr[j].direct);
        }
    }
}
