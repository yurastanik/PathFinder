using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Choosebutton : MonoBehaviour
{
    [SerializeField] private GameObject InputField;
    [SerializeField] private GameObject Btnplay;
    [SerializeField] private Button[] button_list = new Button[15];
    [SerializeField] private Image Content;
    [SerializeField] private GameObject Scrol;

    private int[,] movesf1;
    private int[,] movesf2;
    private int[,] movesf3;
    private int[,] movesf4;
    private int[,] movesf5;
    private int[,] movesf6;
    private int[] colors;
    private int[] moves = new int[] {1, 2, 3, 7}; 
    private MapLoader loader;
    private int btn;
    private int fun;
    public Sprite[] s1;

    public List<int> func_num = new List<int>();


    public void Awake() {
        loader = GameObject.Find("Map").GetComponent<MapLoader>();
        Map loadedMap = loader.GetMap();
        colors = loadedMap.colors;
        foreach (int i in colors)
            AddAction(ref moves, i);
        movesf1 = MapLoader.OneDToTwoDArray(loadedMap.movesf1, 2);
        ChooseAction(ref movesf1);
        func_num.Add(movesf1.GetLength(0));
        if (loadedMap.movesf2 != null) {
            movesf2 = MapLoader.OneDToTwoDArray(loadedMap.movesf2, 2);
            ChooseAction(ref movesf2);
            func_num.Add(movesf2.GetLength(0));
        }
        if (loadedMap.movesf3 != null) {
            movesf3 = MapLoader.OneDToTwoDArray(loadedMap.movesf3, 2);
            ChooseAction(ref movesf3);
            func_num.Add(movesf3.GetLength(0));
        }
        if (loadedMap.movesf4 != null) {
            movesf4 = MapLoader.OneDToTwoDArray(loadedMap.movesf4, 2);
            ChooseAction(ref movesf4);
            func_num.Add(movesf4.GetLength(0));
        }
        if (loadedMap.movesf5 != null) {
            movesf5 = MapLoader.OneDToTwoDArray(loadedMap.movesf5, 2);
            ChooseAction(ref movesf5);
            func_num.Add(movesf5.GetLength(0));
        }
        if (loadedMap.movesf6 != null) {
            movesf6 = MapLoader.OneDToTwoDArray(loadedMap.movesf6, 2);
            ChooseAction(ref movesf6);
            func_num.Add(movesf6.GetLength(0));
        }
        Array.Sort(moves);
        foreach (int i in moves) {
            button_list[i].gameObject.SetActive(true);
        }
        s1 = Resources.LoadAll<Sprite>("Sprites/Button/input_button");
        Content.transform.position = new Vector3(Scrol.transform.position.x*2F, Content.transform.position.y, Content.transform.position.z);
        // Debug.Log(Content.transform.position.x);
        // Debug.Log(Content.transform.position.y);
        // Debug.Log(Content.transform.position.z);
    }

    private void AddAction(ref int[] arr, int action) {
        int[] newArr = new int[arr.GetLength(0)+1];
        newArr[arr.GetLength(0)] = action;
        for (int a = 0; a < arr.GetLength(0); a++) 
            newArr[a] = arr[a];
        arr = newArr;
    }

    private void ChooseAction(ref int[,] arr) {
        for (int a = 0; a < arr.GetLength(0); a++) {
            if (arr[a, 0] > 3 && arr[a, 0] != 7) 
                AddAction(ref moves, arr[a, 0]);
        }
    }

    public void Red_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.8490566F, 0.2763439F, 0.2763439F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Red;
    }
    public void Blue_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.2763439F, 0.6294675F, 0.8490566F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Blue;
    }
    public void Green_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.3551086F, 0.7169812F,0.3980731F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Green;
    }
    public void Top_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[2];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.forward;
    }
    public void Left_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[1];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.left;
    }
    public void Right_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[3];
            fun = InputField.GetComponent<Inputbuttons>().func;
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.right;
    }
}
