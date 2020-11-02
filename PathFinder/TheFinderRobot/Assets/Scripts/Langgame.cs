using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//using Newtonsoft.Json;

public class Langgame : MonoBehaviour
{
    public static Languagesfill fills;
    private static string Lang_kod = "en";
    [SerializeField] private List<Text> allgametext;


    void Awake()
    {
        load_file();
    }

    public void load_file() {
        if (Savegame.sv.Languages == 1) {
            Lang_kod = "ru";
        }
        else {
            Lang_kod = "en";
            Debug.Log("Choose en");
        }
        var jsonTextFile = Resources.Load<TextAsset>("Lang/" + Lang_kod);
        string tileFile = jsonTextFile.text;
        fills = JsonUtility.FromJson<Languagesfill>(tileFile);
        change_text();
    }

    public void change_text() {
        fills.dict = new Dictionary<string,string>(fills.Languagetab.Count/2);
        for (int i = 0; i < fills.Languagetab.Count; i = i + 2)
            fills.dict.Add(fills.Languagetab[i], fills.Languagetab[i+1]);
        foreach(Text item in allgametext) {
            //Debug.Log("itrm name  = " + item.name  + "   in dict = " + fills.dict[item.name]);
            if (item.name == "Game speed") {
                item.text = fills.dict[item.name] +  Savegame.sv.speed/2;
            }
            else
                item.text = fills.dict[item.name];
            
        }
        

    }
}

public class Languagesfill {
    public List<string> Languagetab;
    public Dictionary <string,string> dict;
}
