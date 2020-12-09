using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeStarter : MonoBehaviour {

    [SerializeField]
    private GameObject fadeObj;

    void Awake() {
        fadeObj.SetActive(true);
    }

}
