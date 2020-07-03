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
                sv.FirstEntry = 1;
            }

            else {
                sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Save"));
                Debug.Log("not first " +  sv.FirstEntry);
            }
        }

    }

    private void OnApplicationQuit() {
        Debug.Log("SAving");
        PlayerPrefs.SetString("Save", JsonUtility.ToJson(sv));
        // PlayerPrefs.DeleteAll();
    }



}

[SerializeField]
public class Save {
    public int FirstEntry;
}

