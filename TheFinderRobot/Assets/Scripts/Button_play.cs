using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_play : MonoBehaviour
{
    private int func_count;
    [SerializeField] GameObject button;
    int[,] moves1 = new int[0,0];
    int[,] moves2 = new int[0,0];
    int[,] moves3 = new int[0,0];
    int[,] moves4 = new int[0,0];
    int[,] moves5 = new int[0,0];
    Choosebutton choosebutton;
    Robot_move player;
    public List <Functionclass> func = new List<Functionclass>();

    public void Awake() {
        ReStart();
    }

    public void ReStart() {
        func = new List<Functionclass>();
        player = GameObject.Find("Player").GetComponent<Robot_move>();
        choosebutton = button.GetComponent<Choosebutton>();
        func_count = choosebutton.func_num.Count;
        for (int i = 0; i < func_count; i++) {
            func.Add(new Functionclass(choosebutton.func_num[i]));
        }
    }

    private void AddAction(ref int[,] arr, int dir, int col) {
        if (dir != 0) {
            int[,] newArr = new int[arr.GetLength(0)+1, 2];
            for (int a = 0; a < arr.GetLength(0); a++){
                for (int i = 0; i < arr.GetLength(1); i++) {
                    newArr[a, i] = arr[a, i];
                }            
            }
            newArr[newArr.GetLength(0)-1, 0] = dir;
            newArr[newArr.GetLength(0)-1, 1] = col;
            arr = newArr;
        }
    }
    
    private ref int[,] ListChoser(int num) {
        if (num == 1) {
            return ref moves1;
        }
        else if (num == 2) {
            return ref moves2;
        }
        else if (num == 3) {
            return ref moves3;
        }
        else if (num == 4) {
            return ref moves4;
        }
        return ref moves5;
    }

    private void UpToDate() {
        moves1 = new int[0, 0];
        moves2 = new int[0, 0];
        moves3 = new int[0, 0];
        moves4 = new int[0, 0];
        moves5 = new int[0, 0];        
    }

    public void Start_btn() {        
        if (player.readyToStart) {
            UpToDate();
            for (int i = 0; i < func_count; i++) {
                int[,] list = ListChoser(i+1);
                for (int j = 0; j < choosebutton.func_num[i]; j++) {
                    AddAction(ref list, func[i].input_arr[j].direct, func[i].input_arr[j].color);
                }
                ListChoser(i+1) = list;
            }        
            player.MovesInit(moves1, moves2, moves3, moves4, moves5);
        }
    }
}
