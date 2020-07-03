using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Button_play : MonoBehaviour
{
    private int func_count;
    [SerializeField] GameObject button;
    [SerializeField] private GameObject pausepanel;
    [SerializeField] private Button main_pause_btn;

    bool isPause = false;
    int[,] moves1 = new int[0,0];
    int[,] moves2 = new int[0,0];
    int[,] moves3 = new int[0,0];
    int[,] moves4 = new int[0,0];
    int[,] moves5 = new int[0,0];
    Choosebutton choosebutton;
    Robot_move player;
    public List <Functionclass> func = new List<Functionclass>();
    [SerializeField] public Sprite pause_btn;
    [SerializeField] public Sprite start_btn;

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

    public void ReturnBtn() {
        isPause = false;
        Image btn = transform.GetChild(1).GetComponent<Image>();
        btn.sprite = start_btn;
        btn.color = new Color(0.7458385f, 0.754717f, 0.01779993f, 1);
        float x_pos = transform.GetChild(2).transform.position.x;
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(2).transform.position = new Vector3(transform.GetChild(3).transform.position.x, transform.GetChild(2).transform.position.y, transform.GetChild(2).transform.position.z);
        transform.GetChild(3).transform.position = new Vector3(x_pos, transform.GetChild(3).transform.position.y, transform.GetChild(3).transform.position.z);
    }

    public void ActivateButton() {
        float stop_pos = transform.GetChild(3).transform.position.x;
        transform.GetChild(3).transform.position = new Vector3(transform.GetChild(2).transform.position.x, transform.GetChild(3).transform.position.y, transform.GetChild(3).transform.position.z);
        transform.GetChild(2).transform.position = new Vector3(stop_pos, transform.GetChild(2).transform.position.y, transform.GetChild(2).transform.position.z);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void Stop_btn() {
        player.StopGame();
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
    }

}
