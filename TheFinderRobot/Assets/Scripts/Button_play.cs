using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_play : MonoBehaviour
{
    //public List <Input_Class> input_arr = new List<Input_Class>();
    int[] func_arr = new int[] { 6 };
    //private static int buttons = 0;
    public List <Functionclass> func = new List<Functionclass>();


    void Start()
    {
         for (int i = 0; i < func_arr.Length; i++) {
             func.Add(new Functionclass(func_arr[i]));
        }

    }
    public void Start_btn() {
        for (int i = 0; i < func_arr.Length; i++) {
            for (int j = 0; j < func_arr[i]; j++)
                Debug.Log(func[i].input_arr[j].color + "and" + func[i].input_arr[j].direct);
        }
    }
}
