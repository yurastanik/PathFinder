using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
//using Newtonsoft.Json;

public class Lang : MonoBehaviour
{
    public static Languagesfill fills;
    private static string Lang_kod = "en";
    [SerializeField] private Text text1;


    void Awake()
    {
        load_file();
    }

    public void load_file() {
        if (Savegame.sv.Languages == 1)
            Lang_kod = "ru";
        else
            Lang_kod = "en";
        Lang_kod = "en";
        var jsonTextFile = Resources.Load<TextAsset>("Lang/" + Lang_kod);
        string tileFile = jsonTextFile.text;
         Debug.Log("string");
        Debug.Log(tileFile);
        fills = JsonUtility.FromJson<Languagesfill>(tileFile);
        //var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(tileFile);
        change_text();
    }

    public void change_text() {
        Debug.Log("dict");
        Dictionary <string,string> dict= new Dictionary<string,string>(5);
        for (int i = 0; i < fills.Languagetab.Count; i = i + 2)
            dict.Add(fills.Languagetab[i], fills.Languagetab[i+1]);
        // = fills.Languagetab.ToDictionary(k => k, k => k);
        Debug.Log(dict.Keys);
        Debug.Log(dict[text1.name]);
        
        //text1.text = fills.Languagetab[text1.name];
    }
}

public class Languagesfill {
    public List<string> Languagetab;
}
