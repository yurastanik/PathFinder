using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchfour : MonoBehaviour {
    public GameObject inputField;

    public string Direct;
    public string Color;

     void OnMouseUp()
    {
         if (Input.GetMouseButtonUp(0)) {
       //if (Input.touchCount > 0) {
            inputField.GetComponent<SetActive>().touch = true;
                inputField.GetComponent<SetActive>().button = 3;
                 Debug.Log(gameObject.tag);

        }
    }
}
