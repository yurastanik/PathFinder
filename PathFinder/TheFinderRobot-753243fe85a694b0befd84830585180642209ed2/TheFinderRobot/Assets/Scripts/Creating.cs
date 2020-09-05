using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Creating : MonoBehaviour
{

    [SerializeField] public Button blocked;

    void Awake(){
        foreach (Transform child in transform) 
            GameObject.Destroy(child.gameObject);
        int x = -613;
        int y = 530;
        int nam = Convert.ToInt32(gameObject.name);
        for (int i = 0; i < 12; i++) {
            GameObject newChild = GameObject.Instantiate(blocked.gameObject, transform);
            newChild.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
            newChild.transform.GetChild(0).GetComponent<Text>().text = nam.ToString();
            nam += 1;
            if (x == -613)
                x = 0;
            else if (x == 0)
                x = 613;
            else if (x == 613) {
                x = -613;
                y = y - 529;
            }
        }
        
    }
}
