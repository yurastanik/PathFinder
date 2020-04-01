using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchtwo : MonoBehaviour
{
    public GameObject inputField;
    
    public string Direct;
    public string Color;

    void OnMouseUp() {
         if (Input.GetMouseButtonUp(0)) {
            inputField.GetComponent<SetActive>().touch = true;
            inputField.GetComponent<SetActive>().button = 1;
             //    Debug.Log(gameObject.tag);
        }
    }
}
