 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savegame : MonoBehaviour
{

    public static Save sv = new Save();

    private void Awake() {
        if (MaplevelChose.getsave) {
            DontDestroyOnLoad(this.gameObject);
            if (!PlayerPrefs.HasKey("Save")) {
                Debug.Log("first");
                sv.FirstEntry = true;
                sv.Education = false;
                sv.Chapter1 = false;
                sv.Chapter2 = false;
                sv.Chapter3 = false;
            }
            else {
                sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
                Debug.Log("not first " +  sv.FirstEntry);
            }
        }

    }

    private void OnApplicationQuit() {
        Debug.Log("SAving");
        sv.FirstEntry = false;
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
        //PlayerPrefs.DeleteAll();
    }



}

[SerializeField]
public class Save {
    public bool FirstEntry;
    public bool Education;
    public bool Chapter1;
    public bool Chapter2;
    public bool Chapter3;

}

