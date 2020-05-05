using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Levelbutton : MonoBehaviour
{
    public void choose_level() {
        Debug.Log(gameObject.name);
        MaplevelChose.map_number = Int32.Parse(gameObject.name);
        SceneManager.LoadScene("Test_Buttons", LoadSceneMode.Single);
    }
}
