using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class For_input : MonoBehaviour
{
    public GameObject inputField;
    public Component the_scrpit;

    public GameObject the_button;
 //   public ScriptableObject the_scrpit;
    public int index = 0;
    void OnMouseUp() {
        
        if (inputField.GetComponent<SetActive>().touch == true) {
            if (Input.GetMouseButtonUp(0)) {
                    Debug.Log(gameObject.tag);
                    index = inputField.GetComponent<SetActive>().button;
                    if (inputField.GetComponent<SetActive>().button_list[index].tag == "one")
                        the_scrpit = inputField.GetComponent<SetActive>().button_list[index].GetComponent<TouchOne1>();
                    if (inputField.GetComponent<SetActive>().button_list[index].tag == "two")
                        the_scrpit = inputField.GetComponent<SetActive>().button_list[index].GetComponent<Touchtwo>();
                    if (inputField.GetComponent<SetActive>().button_list[index].tag == "three")
                        the_scrpit = inputField.GetComponent<SetActive>().button_list[index].GetComponent<Touchthree>();
                    if (inputField.GetComponent<SetActive>().button_list[index].tag == "four")
                        the_scrpit = inputField.GetComponent<SetActive>().button_list[index].GetComponent<Touchfour>();
                    Debug.Log(the_scrpit);


                    //the_scrpit = inputField.GetComponent<SetActive>().button_list[index].GetComponent(the_scrpit).
                    // if (gameObject.tag == "Blue")
                    //     inputField.GetComponent<SetActive>().button_list[index].
                }
        }
    }
}

