using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Levelbutton : MonoBehaviour {

    public void choose_level() {
        // if (gameObject.GetComponent<Image>().sprite.name == "blocked");
        // else {
    //#if UNITY_EDITOR
    //#endif
            MaplevelChose.map_number = Int32.Parse(gameObject.transform.GetChild(0).GetComponent<Text>().text);
            Savegame.sv.mapNum = MaplevelChose.map_number;
            // FadeInOut.FadeIn("Game");
            StartCoroutine(GameObject.FindGameObjectWithTag("FadeInFadeOut").GetComponent<FadeInOut>().FadeIn("Game"));
        // }
    }

}
