using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class For_input : MonoBehaviour
{
    public GameObject inputField;


    public GameObject the_button;
    public int index = 0;
    void OnMouseUp() {
        
        if (inputField.GetComponent<SetActive>().touch == true) {
            if (Input.GetMouseButtonUp(0)) {
                    Debug.Log(gameObject.tag);
                    index = inputField.GetComponent<SetActive>().button;
                    if (gameObject.tag == "Blue")
                        the_button.GetComponent<Button_play>().input_arr[index].color = (int) Input_Class.Colors.Blue;
                    if (gameObject.tag == "Red")
                        the_button.GetComponent<Button_play>().input_arr[index].color = (int) Input_Class.Colors.Red;
                    if (gameObject.tag == "green")
                        the_button.GetComponent<Button_play>().input_arr[index].color = (int) Input_Class.Colors.Green;
                    if (gameObject.tag == "forrward")
                        the_button.GetComponent<Button_play>().input_arr[index].direct = (int) Input_Class.Directs.forward;
                    if (gameObject.tag == "left")
                        the_button.GetComponent<Button_play>().input_arr[index].direct = (int) Input_Class.Directs.left;
                    if (gameObject.tag == "right")
                        the_button.GetComponent<Button_play>().input_arr[index].direct = (int) Input_Class.Directs.right;

                }
        }
    }
}

