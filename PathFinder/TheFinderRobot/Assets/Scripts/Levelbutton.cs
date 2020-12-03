using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Levelbutton : MonoBehaviour {

    public void choose_level() {
        if (gameObject.GetComponent<Image>().sprite.name != "blocked") {
        // else {
    //#if UNITY_EDITOR
    //#endif
            if (gameObject.name == "first_chap")
                MaplevelChose.map_number = -1;
            else if (gameObject.name == "second_chap")
                MaplevelChose.map_number = -5;
            else if (gameObject.name == "third_chap")
                MaplevelChose.map_number = -8;
            else
                MaplevelChose.map_number = Int32.Parse(gameObject.transform.GetChild(0).GetComponent<Text>().text);
            Savegame.sv.mapNum = MaplevelChose.map_number;
            //FadeInOut.FadeIn("Game");
            StartCoroutine(GameObject.FindGameObjectWithTag("FadeInFadeOut").GetComponent<FadeInOut>().FadeIn("Game"));
        // }
            // SceneManager.LoadScene("Game", LoadSceneMode.Single);
        }
    }

}
