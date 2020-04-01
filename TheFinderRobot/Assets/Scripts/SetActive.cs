using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public int numer = 4;
    static int num = 4;
  // num = number;
   public GameObject[] button_list = new GameObject[num];

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
