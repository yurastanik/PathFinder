using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_play : MonoBehaviour
{
    private int func_count;
    [SerializeField] private Choosebutton choosebutton;
    [SerializeField] private Robot_move player;


    [SerializeField] private GameObject pausepanel;
    [SerializeField] private Text sped;
    //[SerializeField] private Button main_pause_btn;
    [SerializeField] private GameObject hintmenu;
    [SerializeField] private Image hint_count;

    bool isPause = false;
    int[,] moves1 = new int[0,0];
    int[,] moves2 = new int[0,0];
    int[,] moves3 = new int[0,0];
    int[,] moves4 = new int[0,0];
    int[,] moves5 = new int[0,0];


    public List <Functionclass> func = new List<Functionclass>();
    [SerializeField] public Sprite pause_btn;
    [SerializeField] public Sprite start_btn;

    public void ReStart() {
        func = new List<Functionclass>();
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

    public void ReturnBtn() {
        isPause = false;
        Image btn = transform.GetChild(1).GetComponent<Image>();
        btn.sprite = start_btn;
        btn.color = new Color(0.7458385f, 0.754717f, 0.01779993f, 1);
        float x_pos = transform.GetChild(2).transform.position.x;
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).transform.position = new Vector3(transform.GetChild(3).transform.position.x, transform.GetChild(2).transform.position.y, transform.GetChild(2).transform.position.z);
        transform.GetChild(3).transform.position = new Vector3(x_pos, transform.GetChild(3).transform.position.y, transform.GetChild(3).transform.position.z);
    }

    public void ActivateButton() {
        float stop_pos = transform.GetChild(3).transform.position.x;
        transform.GetChild(3).transform.position = new Vector3(transform.GetChild(2).transform.position.x, transform.GetChild(3).transform.position.y, transform.GetChild(3).transform.position.z);
        transform.GetChild(2).transform.position = new Vector3(stop_pos, transform.GetChild(2).transform.position.y, transform.GetChild(2).transform.position.z);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void Stop_btn() {
        player.StopGame();
    }

    public void Speed() {
        Savegame.sv.speed += 2;
        sped.text = "Game speed x2";
        if (Savegame.sv.speed == 6) {
            Savegame.sv.speed += 1;
            sped.text = "Game speed x3";
        }
        else if (Savegame.sv.speed == 9) {
            Savegame.sv.speed -= 7;
            sped.text = "Game speed x1";
        }
        player.tot_speed();
    }

    public void Start_btn() {
        if (player.readyToStart) {
            ActivateButton();
            Image btn = transform.GetChild(1).GetComponent<Image>();
            btn.sprite = pause_btn;
            btn.color = new Color(1, 1, 1, 1);
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
        else {
            if (isPause) {
                player.PauseGame();
                Image btn = transform.GetChild(1).GetComponent<Image>();
                btn.sprite = pause_btn;
                btn.color = new Color(1, 1, 1, 1);
                isPause = false;
            }
            else {
                player.PauseGame();
                Image btn = transform.GetChild(1).GetComponent<Image>();
                btn.sprite = start_btn;
                btn.color = new Color(0.7458385f, 0.754717f, 0.01779993f, 1);
                isPause = true;
            }
        }
    }

    public void main_pause() {
        Time.timeScale = 0;
        if (Savegame.sv.speed == 4)
            sped.text = "Game speed x2";
        else if (Savegame.sv.speed == 2)
            sped.text = "Game speed x1";
        else if (Savegame.sv.speed == 7)
            sped.text = "Game speed x3";
        pausepanel.gameObject.SetActive(true);
    }
    public void resume() {
        pausepanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void quit() {
        Time.timeScale = 1;
        MaplevelChose.quit = true;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Savegame.sv.movesf1 = null;
        Savegame.sv.movesf2 = null;
        Savegame.sv.movesf3 = null;
        Savegame.sv.movesf4 = null;
        Savegame.sv.movesf5 = null;
        Savegame.sv.moves1 = null;
        Savegame.sv.moves2 = null;
        Savegame.sv.moves3 = null;
        Savegame.sv.moves4 = null;
        Savegame.sv.moves5 = null;
        Savegame.sv.mapNum = 0;
    }

    public void hint_menu() {
        if (Inputbuttons.move_btn == true) {
            Time.timeScale = 0;
            hint_count.GetComponentInChildren<Text>().text = Savegame.sv.hint + "        left";
            hintmenu.gameObject.SetActive(true);
        }
    }

    public void hintresume() {
        Time.timeScale = 1;
        hintmenu.gameObject.SetActive(false);
    }
}
