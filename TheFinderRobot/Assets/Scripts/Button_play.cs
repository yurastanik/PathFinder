using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_play : MonoBehaviour
{
    public List <Input_Class> input_arr = new List<Input_Class>();
    public GameObject input_fild;
    public int buttons = 0;
    //public Input_Class this_button;
    
    void Start()
    {
        buttons = input_fild.GetComponent<SetActive>().numer;
        for (int i = 0; i < buttons; i++) {
            input_arr.Add( new Input_Class(0, 0));
            Debug.Log(input_arr[i].color + "and" + input_arr[i].direct);
        }
        
    }
    void OnMouseUp() {
        if (Input.GetMouseButtonUp(0)) {
            for (int i = 0; i < buttons; i++)
                Debug.Log(input_arr[i].color + "and" + input_arr[i].direct);
        }
    }
}
