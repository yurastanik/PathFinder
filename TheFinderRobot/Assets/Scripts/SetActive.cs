using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
   static int number = 4;
   public GameObject[] button_list = new GameObject[number];

   public bool touch = false;

   public int befor_button = -1;
   public int button = -1;



    // Update is called once per frame
    void Update()
    {
        if (befor_button != -1)
                button_list[befor_button].GetComponent<SpriteRenderer>().color = Color.white; 
        if (button != -1) {
                button_list[button].GetComponent<SpriteRenderer>().color = Color.red;
                befor_button = button;
        }
    }
}
