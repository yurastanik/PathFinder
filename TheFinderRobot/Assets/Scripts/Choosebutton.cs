using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Choosebutton : MonoBehaviour
{
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject f1;
    [SerializeField] private GameObject f2;
    [SerializeField] private GameObject f3;
    [SerializeField] private GameObject f4;
    [SerializeField] private GameObject f5;
    [SerializeField] private GameObject InputField;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject Btnplay;
    [SerializeField] private Button[] button_list = new Button[15];
    [SerializeField] private Image Content;
    [SerializeField] private Image ContentPrefab;
    [SerializeField] private GameObject Scrol;
    [SerializeField] private GameObject Robot;
    [SerializeField] public Image button_frame;

    private int[,] movesf1;
    public bool new_speed = false;
    private bool destroy = true;
    private int[,] movesf2;
    private int[,] movesf3;
    private int[,] movesf4;
    private int[,] movesf5;
    private int[,] movesf6;
    private int[] colors;
    private int[] moves = new int[0];
    private Robot_move robot;
    private GameObject create;
    private MapLoader loader;
    private int btn;
    private int fun;
    public Sprite[] s1;
    public bool fade = false;
    public List<int> func_num = new List<int>();
    Vector3 start_frame;


    public void Awake() {
        robot = Robot.GetComponent<Robot_move>();
        s1 = Resources.LoadAll<Sprite>("Sprites/Button/input_button");
        ButtonLoad(true);
        if (Savegame.sv.movesf1 != null && Savegame.sv.movesf1.Length > 0)
            Debug.Log(Savegame.sv.movesf1[0]);
    }

    private void UpDatetion() { 
        for (int i = 0; i < Btnplay.GetComponent<Button_play>().func.Count; i++){
            int buttons = Btnplay.GetComponent<Button_play>().func[i].input_arr.Count;
            for (int a = 0; a < buttons; a++){
                InputField.GetComponent<Inputbuttons>().button_list[a].image.sprite = s1[0];
                InputField.GetComponent<Inputbuttons>().button_list[a].image.color = new Color(0.7333333f, 0.7843138f, 0.8784314f, 1f);
                Btnplay.GetComponent<Button_play>().func[i].input_arr[a].direct = 0;
                Btnplay.GetComponent<Button_play>().func[i].input_arr[a].color = 0;
            }
        }
        movesf2 = new int[0,0];
        movesf3 = new int[0,0];
        movesf4 = new int[0,0];
        movesf5 = new int[0,0];
        movesf6 = new int[0,0];
        moves = new int[0];
        func_num = new List<int>();
        new_speed = false;
        destroy = true;
    } 

    public void ButtonLoad(bool isFirst) {
        if (!isFirst) {
            for (int i = 1; i < 16; i++) {
                button_list[i].gameObject.SetActive(false);
            }
            UpDatetion();
        }
        loader = GameObject.Find("Map").GetComponent<MapLoader>();
        Map loadedMap = loader.GetMap();
        colors = loadedMap.colors;
        if (colors != null) {
            foreach (int i in colors)
                AddAction(ref moves, i);
        }
        movesf1 = MapLoader.OneDToTwoDArray(loadedMap.movesf1, 2);
        if (Savegame.sv.moves1 == null)
            Savegame.sv.moves1 = new int[movesf1.GetLength(0), movesf1.GetLength(1)];
        ChooseAction(ref movesf1);
        func_num.Add(movesf1.GetLength(0));
        if (loadedMap.movesf2 != null) {
            movesf2 = MapLoader.OneDToTwoDArray(loadedMap.movesf2, 2);
            if (Savegame.sv.moves2 == null)
                Savegame.sv.moves2 = new int[movesf2.GetLength(0), movesf2.GetLength(1)];
            ChooseAction(ref movesf2);
            func_num.Add(movesf2.GetLength(0));
        }
        if (loadedMap.movesf3 != null) {
            movesf3 = MapLoader.OneDToTwoDArray(loadedMap.movesf3, 2);
            if (Savegame.sv.moves3 == null)
                Savegame.sv.moves3 = new int[movesf3.GetLength(0), movesf3.GetLength(1)];
            ChooseAction(ref movesf3);
            func_num.Add(movesf3.GetLength(0));
        }
        if (loadedMap.movesf4 != null) {
            movesf4 = MapLoader.OneDToTwoDArray(loadedMap.movesf4, 2);
            if (Savegame.sv.moves4 == null)
                Savegame.sv.moves4 = new int[movesf4.GetLength(0), movesf4.GetLength(1)];
            ChooseAction(ref movesf4);
            func_num.Add(movesf4.GetLength(0));
        }
        if (loadedMap.movesf5 != null) {
            movesf5 = MapLoader.OneDToTwoDArray(loadedMap.movesf5, 2);
            if (Savegame.sv.moves5 == null)
                Savegame.sv.moves5 = new int[movesf5.GetLength(0), movesf5.GetLength(1)];
            ChooseAction(ref movesf5);
            func_num.Add(movesf5.GetLength(0));
        }
        foreach (int i in moves) {
            button_list[i].gameObject.SetActive(true);
        }
        Btnplay.GetComponent<Button_play>().ReStart();
        if (Savegame.sv.movesf1 != null && Savegame.sv.movesf1.Length > 0) {
            Savegame.sv.moves1 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf1, 2);
            for (int i = 0; i < Savegame.sv.moves1.GetLength(0); i++) {
                InputField.GetComponent<Inputbuttons>().button_list[i].image.color = ColorIndeed(Savegame.sv.moves1[i, 1]);
                Btnplay.GetComponent<Button_play>().func[0].input_arr[i].color = Savegame.sv.moves1[i, 1];
                Btnplay.GetComponent<Button_play>().func[0].input_arr[i].direct = Savegame.sv.moves1[i, 0];
                if (Savegame.sv.moves1[i, 0] != 0) {
                    if (Savegame.sv.moves1[i, 0] < 4) {
                        int n = 6;
                        for (int a = 1; a < 4; a++) {
                            n += 1;
                            if (n == 9)
                                n -= 3;
                            if (Savegame.sv.moves1[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                    else if (Savegame.sv.moves1[i, 0] > 6) {
                        int n = 0;
                        for (int a = 7; a < 12; a++) {
                            n += 1;
                            if (Savegame.sv.moves1[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                }
            }
        }
        if (Savegame.sv.movesf2 != null && Savegame.sv.movesf2.Length > 0) {
            Savegame.sv.moves2 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf2, 2);
            for (int i = 0; i < Savegame.sv.moves2.GetLength(0); i++) {
                InputField.GetComponent<Inputbuttons>().button_list[i].image.color = ColorIndeed(Savegame.sv.moves2[i, 1]);
                Btnplay.GetComponent<Button_play>().func[1].input_arr[i].color = Savegame.sv.moves2[i, 1];
                Btnplay.GetComponent<Button_play>().func[1].input_arr[i].direct = Savegame.sv.moves2[i, 0];
                if (Savegame.sv.moves2[i, 0] != 0) {
                    if (Savegame.sv.moves2[i, 0] < 4) {
                        int n = 6;
                        for (int a = 1; a < 4; a++) {
                            n += 1;
                            if (n == 9)
                                n -= 3;;
                            if (Savegame.sv.moves2[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                    else if (Savegame.sv.moves2[i, 0] > 6) {
                        int n = 0;
                        for (int a = 7; a < 12; a++) {
                            n += 1;
                            if (Savegame.sv.moves2[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                }
            }
        }
        if (Savegame.sv.movesf3 != null && Savegame.sv.movesf3.Length > 0) {
            Savegame.sv.moves3 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf3, 2);
            for (int i = 0; i < Savegame.sv.moves3.GetLength(0); i++) {
                InputField.GetComponent<Inputbuttons>().button_list[i].image.color = ColorIndeed(Savegame.sv.moves3[i, 1]);
                Btnplay.GetComponent<Button_play>().func[2].input_arr[i].color = Savegame.sv.moves3[i, 1];
                Btnplay.GetComponent<Button_play>().func[3].input_arr[i].direct = Savegame.sv.moves3[i, 0];
                if (Savegame.sv.moves3[i, 0] != 0) {
                    if (Savegame.sv.moves3[i, 0] < 4) {
                        int n = 6;
                        for (int a = 1; a < 4; a++) {
                            n += 1;
                            if (n == 9)
                                n -= 3;;
                            if (Savegame.sv.moves3[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                    else if (Savegame.sv.moves3[i, 0] > 6) {
                        int n = 0;
                        for (int a = 7; a < 12; a++) {
                            n += 1;
                            if (Savegame.sv.moves3[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                }
            }
        }
        if (Savegame.sv.movesf4 != null && Savegame.sv.movesf4.Length > 0) {
            Savegame.sv.moves4 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf4, 2);
            for (int i = 0; i < Savegame.sv.moves4.GetLength(0); i++) {
                InputField.GetComponent<Inputbuttons>().button_list[i].image.color = ColorIndeed(Savegame.sv.moves4[i, 1]);
                Btnplay.GetComponent<Button_play>().func[3].input_arr[i].color = Savegame.sv.moves4[i, 1];
                Btnplay.GetComponent<Button_play>().func[3].input_arr[i].direct = Savegame.sv.moves4[i, 0];
                if (Savegame.sv.moves4[i, 0] != 0) {
                    if (Savegame.sv.moves4[i, 0] < 4) {
                        int n = 6;
                        for (int a = 1; a < 4; a++) {
                            n += 1;
                            if (n == 9)
                                n -= 3;;
                            if (Savegame.sv.moves4[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                    else if (Savegame.sv.moves4[i, 0] > 6) {
                        int n = 0;
                        for (int a = 7; a < 12; a++) {
                            n += 1;
                            if (Savegame.sv.moves4[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                }
            } 
        }
        if (Savegame.sv.movesf5 != null && Savegame.sv.movesf5.Length > 0) {
            Savegame.sv.moves5 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf5, 2);
            for (int i = 0; i < Savegame.sv.moves5.GetLength(0); i++) {
                InputField.GetComponent<Inputbuttons>().button_list[i].image.color = ColorIndeed(Savegame.sv.moves5[i, 1]);
                Btnplay.GetComponent<Button_play>().func[4].input_arr[i].color = Savegame.sv.moves5[i, 1];
                Btnplay.GetComponent<Button_play>().func[4].input_arr[i].direct = Savegame.sv.moves5[i, 0];
                if (Savegame.sv.moves5[i, 0] != 0) {
                    if (Savegame.sv.moves5[i, 0] < 4) {
                        int n = 6;
                        for (int a = 1; a < 4; a++) {
                            n += 1;
                            if (n == 9)
                                n -= 3;;
                            if (Savegame.sv.moves5[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                    else if (Savegame.sv.moves5[i, 0] > 6) {
                        int n = 0;
                        for (int a = 7; a < 12; a++) {
                            n += 1;
                            if (Savegame.sv.moves5[i, 0] == a)
                                InputField.GetComponent<Inputbuttons>().button_list[i].image.sprite = s1[n];
                        }
                    }
                }
            }
        }
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
            AddAction(ref moves, arr[a, 0]);
        }
    }

    public void CancelButton() {
        btn = InputField.GetComponent<Inputbuttons>().button;
        fun = InputField.GetComponent<Inputbuttons>().func;
        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[0];
        Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = 0;
        InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.7333333f, 0.7843138f, 0.8784314f, 1f);
        Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = 0;
        if (fun == 0) {
            for (int j = 0; j < 2; j++)
                Savegame.sv.moves1[btn, j] = 0;
        }
        else if (fun == 1) {
            for (int j = 0; j < 2; j++)
                Savegame.sv.moves1[btn, j] = 0;
        }
        else if (fun == 2) {
            for (int j = 0; j < 2; j++)
                Savegame.sv.moves1[btn, j] = 0;
        }
        else if (fun == 3) {
            for (int j = 0; j < 2; j++)
                Savegame.sv.moves1[btn, j] = 0;
        }
        else if (fun == 4) {
            for (int j = 0; j < 2; j++)
                Savegame.sv.moves1[btn, j] = 0;
        }
    }

    public void HideButtons() {
        Content.gameObject.SetActive(false);
        RectTransform panelka = Panel.GetComponent<RectTransform>();
        ScrollRect scroll = Panel.GetComponent<ScrollRect>();
        RectTransform pict = ContentPrefab.GetComponent<RectTransform>();
        panelka.localPosition = new Vector3(0, -398.6f, 0);
        panelka.sizeDelta = new Vector2(57.5f, 17.1f);
        scroll.content = pict;
        button_frame.gameObject.SetActive(true);
        if (start_frame.x != 0 && start_frame.y != 0)
            button_frame.transform.position = start_frame;
        else
            start_frame = button_frame.transform.position;
    }

    public void FrameTranslate(Transform nextchild, Transform currentchild) {
        Vector3 currentPos = button_frame.transform.position;
        float speed = robot.fade_speed;
        button_frame.transform.position = nextchild.transform.position;
        nextchild.transform.localScale = new Vector3(0.22F, 0.75F, 0);
        button_frame.transform.parent = nextchild.transform;
        currentchild.transform.localScale = new Vector3(0.18F, 0.52F, 0);
    }
    
    public void Hint() {
        btn = InputField.GetComponent<Inputbuttons>().button;
        fun = InputField.GetComponent<Inputbuttons>().func;
        if (fun == 0) {
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = movesf1[btn, 0];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = movesf1[btn, 1];
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = ColorIndeed(movesf1[btn, 1]);
            if (movesf1[btn, 0] < 4) {
                int n = 6;
                for (int a = 1; a < 4; a++) {
                    n += 1;
                    if (n == 9)
                        n -= 3;;
                    if (movesf1[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
            else if (movesf1[btn, 0] > 6) {
                int n = 0;
                for (int a = 7; a < 12; a++) {
                    n += 1;
                    if (movesf1[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
        }
        else if (fun == 1) {
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = movesf2[btn, 0];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = movesf2[btn, 1];
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = ColorIndeed(movesf2[btn, 1]);
            if (movesf2[btn, 0] < 4) {
                int n = 6;
                for (int a = 1; a < 4; a++) {
                    n += 1;
                    if (n == 9)
                        n -= 3;;
                    if (movesf2[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
            else if (movesf2[btn, 0] > 6) {
                int n = 0;
                for (int a = 7; a < 12; a++) {
                    n += 1;
                    if (movesf2[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
        }
        else if (fun == 2) {
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = movesf3[btn, 0];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = movesf3[btn, 1];
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = ColorIndeed(movesf3[btn, 1]);
            if (movesf3[btn, 0] < 4) {
                int n = 6;
                for (int a = 1; a < 4; a++) {
                    n += 1;
                    if (n == 9)
                        n -= 3;;
                    if (movesf3[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
            else if (movesf3[btn, 0] > 6) {
                int n = 0;
                for (int a = 7; a < 12; a++) {
                    n += 1;
                    if (movesf3[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
        }
        if (fun == 3) {
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = movesf4[btn, 0];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = movesf4[btn, 1];
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = ColorIndeed(movesf4[btn, 1]);
            if (movesf4[btn, 0] < 4) {
                int n = 6;
                for (int a = 1; a < 4; a++) {
                    n += 1;
                    if (n == 9)
                        n -= 3;;
                    if (movesf4[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
            else if (movesf4[btn, 0] > 6) {
                int n = 0;
                for (int a = 7; a < 12; a++) {
                    n += 1;
                    if (movesf4[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
        }
        if (fun == 4) {
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = movesf5[btn, 0];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = movesf5[btn, 1];
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = ColorIndeed(movesf5[btn, 1]);
            if (movesf5[btn, 0] < 4) {
                int n = 6;
                for (int a = 1; a < 4; a++) {
                    n += 1;
                    if (n == 9)
                        n -= 3;;
                    if (movesf5[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
            else if (movesf5[btn, 0] > 6) {
                int n = 0;
                for (int a = 7; a < 12; a++) {
                    n += 1;
                    if (movesf5[btn, 0] == a)
                        InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[n];
                }
            }
        }
    }

    public void DestroyPrefab(int num = 0) {
        fade = true;
        Transform child = ContentPrefab.transform.GetChild(num);
        if (ContentPrefab.transform.childCount > 1) {
            Transform children = ContentPrefab.transform.GetChild(1);
            FrameTranslate(children, child);
        }
        else {
            button_frame.transform.parent = Panel.transform;
            destroy = false;
        }
        StartCoroutine(UnvisiblePrefab(child));
    }
 
    public void ReturnAll(bool stop = false) {
        StopAllCoroutines();
        if (button_frame.transform.parent != Panel.transform)
            button_frame.transform.parent = Panel.transform;
        button_frame.transform.parent = Panel.transform;
        foreach (Transform child in ContentPrefab.transform)
            GameObject.Destroy(child.gameObject);
        Content.gameObject.SetActive(true);
        Btnplay.GetComponent<Button_play>().ReturnBtn();
        RectTransform panelka = Panel.GetComponent<RectTransform>();
        ScrollRect scroll = Panel.GetComponent<ScrollRect>();
        RectTransform pict = Content.GetComponent<RectTransform>();
        panelka.localPosition = new Vector3(-29, -477.525f, 0);
        panelka.sizeDelta = new Vector2(0, -24.35027f);
        scroll.content = pict;
        button_frame.gameObject.SetActive(false);
        if (!stop) {
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
        }
    }



    public IEnumerator UnvisiblePrefab(Transform child) {
        Image background;
        background = child.GetComponent<Image>();
        Color color = background.color;
        float speed = robot.fade_speed; 
        if (speed != 0) {
            for (float f = 0.95f; f >= 0; f -= 0.05f) {
                speed = robot.fade_speed;
                color.a = f;
                background.color = color;
                if (speed != 0)
                    yield return new WaitForSeconds(speed);
                else {
                    break;
                }
            }
        }
        GameObject.Destroy(child.gameObject);
        fade = false;
        yield break;
    }

    public static Color ColorIndeed(int color) {
        if (color != 0) {
            if (color == 1)
                return new Color(0.2763439F, 0.6294675F, 0.8490566F, 1F); 
            else if (color == 2)
                return new Color(0.3551086F, 0.7169812F,0.3980731F, 1F);
            else 
                return new Color(0.8490566F, 0.2763439F, 0.2763439F, 1F);
        }
        else
            return new Color(0.7333333f, 0.7843138f, 0.8784314f, 1f);
    }

    public static Color ColorGetting(int color) {
        if (color != 0) {
            if (color == 1)
                return new Color(0.2763439F, 0.6294675F, 0.8490566F, 0F); 
            else if (color == 2)
                return new Color(0.3551086F, 0.7169812F,0.3980731F, 0F);
            else 
                return new Color(0.8490566F, 0.2763439F, 0.2763439F, 0F);
        }
        else
            return new Color(0.7333333f, 0.7843138f, 0.8784314f, 0f);
    }

    public GameObject PrefabGetting(int move) {
            if (move == 1)
                return top;
            else if (move == 2)
                return right;
            else if (move == 3)
                return left;
            else if (move == 7)
                return f1;
            else if (move == 8)
                return f2;
            else if (move == 9)
                return f3;
            else if (move == 10)
                return f4;
            else
                return f5;
    }

    public IEnumerator CreatePrefab(int col, int moven, int check, int num, bool del) {
        GameObject create = PrefabGetting(moven);
        GameObject newChild = GameObject.Instantiate(create) as GameObject;
        if (newChild.transform.GetSiblingIndex() == 1)
            new_speed = true;
        newChild.transform.parent = ContentPrefab.transform;
        if (newChild.transform.GetSiblingIndex() == 0)
            newChild.transform.localScale = new Vector3(0.22F, 0.75F, 0);
        else
            newChild.transform.localScale = new Vector3(0.18F, 0.52F, 0);
        Image background;
        background = newChild.GetComponent<Image>();
        Color color = ColorGetting(col);
        if (check >= 1)
            ForFunc(num);
        float speed = robot.fade_speed;
        if (speed != 0) {
            for (float f = 0f; f <= 1; f += 0.05f) {
                if (new_speed && f == 0.05f)
                    new_speed = false;
                speed = robot.fade_speed;
                color.a = f;
                background.color = color;
                if (speed != 0)
                    yield return new WaitForSeconds(speed);
                else {
                    color.a = 1;
                    background.color = color;
                    yield break;
                }
            }
        }
        else {
            color.a = 1;
            background.color = color;
            if (new_speed)
                new_speed = false;
        }
        yield break;
    }

    public void ForFunc(int que) {
        Transform fath = ContentPrefab.transform;
        int childs = fath.childCount;
        fath.GetChild(childs-1).GetComponent<RectTransform>().SetSiblingIndex(que);
    }

    public void Red_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.8490566F, 0.2763439F, 0.2763439F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Red;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 1] = 3;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 1] = 3;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 1] = 3;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 1] = 3;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 1] = 3;
    }
    public void Blue_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.2763439F, 0.6294675F, 0.8490566F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Blue;
            if (fun == 0)
                Savegame.sv.moves1[btn, 1] = 1;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 1] = 1;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 1] = 1;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 1] = 1;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 1] = 1;
    }
    public void Green_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.color = new Color(0.3551086F, 0.7169812F,0.3980731F, 1F);
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].color = (int) Input_Class.Colors.Green;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 1] = 2;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 1] = 2;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 1] = 2;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 1] = 2;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 1] = 2;
    }
    public void f1_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[1];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f1;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 7;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 7;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 7;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 7;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 7;
    }
    public void f2_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[2];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f2;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 8;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 8;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 8;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 8;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 8;
    }
    public void f3_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[3];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f3;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 9;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 9;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 9;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 9;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 9;
    }
    public void f4_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[4];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f4;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 10;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 10;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 10;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 10;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 10;
    }

    public void f5_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[5];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f5;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 11;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 11;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 11;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 11;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 11;
    }
    public void Top_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[7];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.forward;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 1;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 1;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 1;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 1;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 1;
    }
    public void Left_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[6];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.left;if (fun == 1) 
            if (fun == 0)
                Savegame.sv.moves1[btn, 0] = 3;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 3;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 3;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 3;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 3;
    }
    public void Right_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[8];
            fun = InputField.GetComponent<Inputbuttons>().func;
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.right;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 2;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 2;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 2;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 2;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 2;
    }
}
