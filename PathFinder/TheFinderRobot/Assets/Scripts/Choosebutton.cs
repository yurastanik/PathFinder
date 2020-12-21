using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Choosebutton : MonoBehaviour
{
    [SerializeField] private Button_play Btnplay;
    [SerializeField] private Inputbuttons InputField;
    [SerializeField] private MapLoader loader;
    [SerializeField] private Robot_move robot;


    [SerializeField] private GameObject top;
    [SerializeField] private GameObject blue_scratch;
    [SerializeField] private GameObject green_scratch;
    [SerializeField] private GameObject red_scratch;
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject f1;
    [SerializeField] private GameObject f2;
    [SerializeField] private GameObject f3;
    [SerializeField] private GameObject f4;
    [SerializeField] private GameObject f5;

    [SerializeField] private GameObject Panel;
    [SerializeField] public Inputbuttons input;
    [SerializeField] private Button[] button_list = new Button[15];
    [SerializeField] private Image Content;
    [SerializeField] public Image ContentPrefab;
    [SerializeField] private GameObject Scrol;
    [SerializeField] public Image button_frame;
    [SerializeField] private GameObject hintmenu;
    [SerializeField] private Image hint_count;
    [SerializeField] public Image hint;

    private int[,] movesf1;
    private bool neww = true;
    public bool new_speed = false;
    private bool destroy = true;
    private int[,] movesf2;
    private int[,] movesf3;
    private int[,] movesf4;
    private int[,] movesf5;
    private int[,] movesf6;
    private int[] colors;
    private int[] moves = new int[0];
    private GameObject create;
    private int btn;
    private int fun;
    public Sprite[] s1;
    public bool fade = false;
    public bool fade_last = false;
    public List<int> func_num = new List<int>();
    Vector3 start_frame;
    Vector2 sizeDelt;
    Vector2 localPos;
    Vector2 anxorMax;


    public void Awake() {
        s1 = Resources.LoadAll<Sprite>("Sprites/Button/input_button");
        ButtonLoad(true);




#if UNITY_EDITOR
        if (Savegame.sv.movesf1 != null && Savegame.sv.movesf1.Length > 0) {
            Debug.Log("SSSSSSTART GAME");
            Debug.Log(Savegame.sv.movesf1[0]);
        }
#endif
    }

    private void UpDatetion() { 
        for (int i = 0; i < Btnplay.func.Count; i++){
            int buttons = Btnplay.func[i].input_arr.Count;
            for (int a = 0; a < buttons; a++){
                Color tmp = InputField.button_list[a].transform.GetChild(0).GetComponent<Image>().color;
                tmp = new Color(tmp.r, tmp.g, tmp.b, 0F);
                InputField.button_list[a].image.color = new Color(0.7333333f, 0.7843138f, 0.8784314f, 1f);
                Btnplay.func[i].input_arr[a].direct = 0;
                Btnplay.func[i].input_arr[a].color = 0;
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
        Btnplay.ReStart();
        if (Savegame.sv.movesf1 != null && Savegame.sv.movesf1.Length > 0) {
            Savegame.sv.moves1 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf1, 2);
            for (int i = 0; i < Savegame.sv.moves1.GetLength(0); i++) {
                InputField.button_list[i].image.color = ColorIndeed(Savegame.sv.moves1[i, 1]);
                Btnplay.func[0].input_arr[i].color = Savegame.sv.moves1[i, 1];
                Btnplay.func[0].input_arr[i].direct = Savegame.sv.moves1[i, 0];
                if (Savegame.sv.moves1[i, 0] != 0)
                    action_chose(Savegame.sv.moves1[i, 0]-1, i);
            }
        }
        if (Savegame.sv.movesf2 != null && Savegame.sv.movesf2.Length > 0) {
            Savegame.sv.moves2 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf2, 2);
            for (int i = 0; i < Savegame.sv.moves2.GetLength(0); i++) {
                Btnplay.func[1].input_arr[i].color = Savegame.sv.moves2[i, 1];
                Btnplay.func[1].input_arr[i].direct = Savegame.sv.moves2[i, 0];
            }
        }
        if (Savegame.sv.movesf3 != null && Savegame.sv.movesf3.Length > 0) {
            Savegame.sv.moves3 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf3, 2);
            for (int i = 0; i < Savegame.sv.moves3.GetLength(0); i++) {
                Btnplay.func[2].input_arr[i].color = Savegame.sv.moves3[i, 1];
                Btnplay.func[2].input_arr[i].direct = Savegame.sv.moves3[i, 0];
            }
        }
        if (Savegame.sv.movesf4 != null && Savegame.sv.movesf4.Length > 0) {
            Savegame.sv.moves4 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf4, 2);
            for (int i = 0; i < Savegame.sv.moves4.GetLength(0); i++) {
                Btnplay.func[3].input_arr[i].color = Savegame.sv.moves4[i, 1];
                Btnplay.func[3].input_arr[i].direct = Savegame.sv.moves4[i, 0];
            } 
        }
        if (Savegame.sv.movesf5 != null && Savegame.sv.movesf5.Length > 0) {
            Savegame.sv.moves5 = MapLoader.OneDToTwoDArray(Savegame.sv.movesf5, 2);
            for (int i = 0; i < Savegame.sv.moves5.GetLength(0); i++) {
                Btnplay.func[4].input_arr[i].color = Savegame.sv.moves5[i, 1];
                Btnplay.func[4].input_arr[i].direct = Savegame.sv.moves5[i, 0];
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
        if (Inputbuttons.move_btn == true) {
            btn = InputField.button;
            fun = InputField.func;
            Color tmp = InputField.button_list[btn].transform.GetChild(0).GetComponent<Image>().color;
            InputField.button_list[btn].transform.GetChild(0).GetComponent<Image>().color = new Color(tmp.r, tmp.g, tmp.b, 0F);
            Btnplay.func[fun].input_arr[btn].direct = 0;
            InputField.button_list[btn].image.color = new Color(0.7333333f, 0.7843138f, 0.8784314f, 1f);
            Btnplay.func[fun].input_arr[btn].color = 0;
            if (fun == 0) {
                for (int j = 0; j < 2; j++)
                    Savegame.sv.moves1[btn, j] = 0;
            }
            else if (fun == 1) {
                for (int j = 0; j < 2; j++)
                    Savegame.sv.moves2[btn, j] = 0;
            }
            else if (fun == 2) {
                for (int j = 0; j < 2; j++)
                    Savegame.sv.moves3[btn, j] = 0;
            }
            else if (fun == 3) {
                for (int j = 0; j < 2; j++)
                    Savegame.sv.moves4[btn, j] = 0;
            }
            else if (fun == 4) {
                for (int j = 0; j < 2; j++)
                    Savegame.sv.moves5[btn, j] = 0;
            }
        }
    }

    public void HideButtons() {
        Content.gameObject.SetActive(false);
        RectTransform panelka = Panel.GetComponent<RectTransform>();
        ScrollRect scroll = Panel.GetComponent<ScrollRect>();
        RectTransform pict = ContentPrefab.GetComponent<RectTransform>();
        sizeDelt = panelka.sizeDelta;
        localPos = panelka.localPosition;
        anxorMax = panelka.anchorMax;
        panelka.localPosition = new Vector3(0, -398.6f, 0);
        panelka.anchorMax = new Vector2(1f, anxorMax.y);
        panelka.sizeDelta = new Vector2(0f, 17.1f);
        panelka.offsetMin = new Vector2(0f, panelka.offsetMin.y);
        panelka.offsetMax = new Vector2(65f, panelka.offsetMax.y);
        scroll.content = pict;
        button_frame.gameObject.SetActive(true);
        if (start_frame.x != 0 && start_frame.y != 0)
            button_frame.transform.position = start_frame;
        else
            start_frame = button_frame.transform.position;
    }

    public Vector3 scale_for_move(int move) {
        if (move == 0)
            return new Vector3(0.5F, 0.8F, 1F);
        else if (move == 1 || move == 2)
            return new Vector3(0.65F, 0.8F, 1F);
        return new Vector3(0.9F, 0.8F, 1F);
    }

    public void FrameTranslate(Transform nextchild, Transform currentchild) {
        Vector3 currentPos = button_frame.transform.position;
        float speed = robot.fade_speed;
        button_frame.transform.position = nextchild.transform.position;
        nextchild.transform.localScale = new Vector3(0.22F, 0.75F, 0);
        button_frame.transform.SetParent(nextchild.transform);
        currentchild.transform.localScale = new Vector3(0.18F, 0.52F, 0);
        ContentPrefab.GetComponent<HorizontalLayoutGroup>().spacing = 24;
        ContentPrefab.GetComponent<HorizontalLayoutGroup>().spacing = 25;
    }
    

    // public void GetMoreHint() {
    //     if (Advertisement.IsReady("rewardedVideo")) {
    //         Advertisement.Show("rewardedVideo");
    //         Savegame.sv.hint++;
    //         hint_count.GetComponentInChildren<Text>().text = Savegame.sv.hint + "        left";
    //     }
    // }

    private void Move(Transform from, Transform to, int col, int n, int btn, int[,] lt) {
        Inputbuttons.move_btn = false;
        StartCoroutine(_Move(from, to, col, n, btn, lt));
    }

    IEnumerator _Move(Transform from, Transform to, int col, int n, int btn, int[,] lt) {
        //yield return new WaitWhile(() => neww);
        neww = false;
        Vector2 original = from.position;
        from.gameObject.SetActive(true);
        Image hint_background = hint.GetComponent<Image>();
        Image child = hint.transform.GetChild(0).GetComponent<Image>();
        child.sprite = s1[n];
        child.transform.localScale = scale_for_move(n);
        hint_background.color = ColorIndeed(lt[btn, 1]);
        Color color = hint_background.color;
        float timer = 0.0f;
        float overTime = ColorIndeed(col).a;
        float y = 1;
        if (overTime < 1)
            y = 2.22f;
        while (timer < overTime) {
            float step = Vector2.Distance(original, to.position) * (Time.deltaTime);
            from.position = Vector2.MoveTowards(from.position, to.position, step);
            color.a = timer;
            hint_background.color = color;
            child.color = new Color(child.color.r, child.color.g, child.color.b, timer*y);
            timer += Time.deltaTime/y;
            yield return null;
        }
        action_chose(n);
        InputField.button_list[btn].image.color = ColorIndeed(lt[btn, 1]);
        from.gameObject.SetActive(false);
        color.a = 0;
        hint_background.color = color;
        child.color = new Color(child.color.r, child.color.g, child.color.b, 0);
        from.transform.position = original;
        Inputbuttons.move_btn = true;
        if (btn+1 < Btnplay.func[fun].input_arr.Count) {
            if (btn == 0)
                input.secondbutton();
            else if (btn == 1)
                input.threebutton();
            else if (btn == 2)
                input.fourbutton();
            else if (btn == 3)
                input.fivebutton();
            else if (btn == 4)
                input.sixbutton();
            else if (btn == 5)
                input.sevenbutton();
            else if (btn == 6)
                input.eightbutton();
        }
        neww = true;
    }

    public void Hint() {
        if (Savegame.sv.hint != 0) {
            Savegame.sv.hint--;
            Time.timeScale = 1;
            hintmenu.gameObject.SetActive(false);
            btn = InputField.button;
            fun = InputField.func;
            if (fun == 0) {
                Btnplay.func[fun].input_arr[btn].direct = movesf1[btn, 0];
                Btnplay.func[fun].input_arr[btn].color = movesf1[btn, 1];
                Move(hint.transform, InputField.button_list[btn].transform, movesf1[btn, 1], movesf1[btn, 0]-1, btn, movesf1);
            }
            else if (fun == 1) {
                Btnplay.func[fun].input_arr[btn].direct = movesf2[btn, 0];
                Btnplay.func[fun].input_arr[btn].color = movesf2[btn, 1];
                Move(hint.transform, InputField.button_list[btn].transform, movesf2[btn, 1], movesf2[btn, 0]-1, btn, movesf2);
            }
            else if (fun == 2) {
                Btnplay.func[fun].input_arr[btn].direct = movesf3[btn, 0];
                Btnplay.func[fun].input_arr[btn].color = movesf3[btn, 1];
                Move(hint.transform, InputField.button_list[btn].transform, movesf3[btn, 1], movesf3[btn, 0]-1, btn, movesf3);
            }
            if (fun == 3) {
                Btnplay.func[fun].input_arr[btn].direct = movesf4[btn, 0];
                Btnplay.func[fun].input_arr[btn].color = movesf4[btn, 1];
                Move(hint.transform, InputField.button_list[btn].transform, movesf4[btn, 1], movesf4[btn, 0]-1, btn, movesf4);
            }
            if (fun == 4) {
                Btnplay.func[fun].input_arr[btn].direct = movesf5[btn, 0];
                Btnplay.func[fun].input_arr[btn].color = movesf5[btn, 1];
                Move(hint.transform, InputField.button_list[btn].transform, movesf5[btn, 1], movesf5[btn, 0]-1, btn, movesf5);
            }
        }
    }

    public void DestroyLastPrefab() {
        fade_last = true;
        Transform child = ContentPrefab.transform.GetChild(ContentPrefab.transform.childCount-1);
        GameObject.Destroy(child.gameObject);
        fade_last = false;
    }

    public void DestroyPrefab(int num = 0) {
        if (!fade) {
            fade = true;
            Transform child = ContentPrefab.transform.GetChild(num);
            if (ContentPrefab.transform.childCount > 1) {
                Transform children = ContentPrefab.transform.GetChild(1);
                FrameTranslate(children, child);
            }
            else {
                button_frame.transform.SetParent(Panel.transform);
                destroy = false;
            }
            StartCoroutine(UnvisiblePrefab(child));
        }
    }
 
    public void ReturnAll(bool stop = false) {
        StopAllCoroutines();
        if (button_frame.transform.parent != Panel.transform)
            button_frame.transform.SetParent(Panel.transform);
        button_frame.transform.SetParent(Panel.transform);
        foreach (Transform child in ContentPrefab.transform)
            GameObject.Destroy(child.gameObject);
        Content.gameObject.SetActive(true);
        Btnplay.ReturnBtn();
        RectTransform panelka = Panel.GetComponent<RectTransform>();
        ScrollRect scroll = Panel.GetComponent<ScrollRect>();
        RectTransform pict = Content.GetComponent<RectTransform>();
        panelka.anchorMax = anxorMax;
        panelka.sizeDelta = sizeDelt;
        panelka.localPosition = localPos;
        scroll.content = pict;
        button_frame.gameObject.SetActive(false);
        fade = false;
        fade_last = false;
        new_speed = false;
        destroy = true;
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
        Image child_obj = child.GetChild(0).GetComponent<Image>();
        Color color_child = child_obj.color;
        float alf = color.a;
        float alfa = color_child.a - alf;
        float speed = robot.fade_speed;
        if (speed != 0) {
            for (float f = alf-alf/20; f >= 0; f -= alf/20) {
                speed = robot.fade_speed;
                color.a = f;
                alfa -= alf/20;
                color_child.a = f+alfa;
                background.color = color;
                child_obj.color = color_child;
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
                return new Color(0F, 0.2509804F, 1F, 0.45F);
            else if (color == 2)
                return new Color(0F, 1F, 0.1098039F, 0.45F);
            else 
                return new Color(1F, 0F, 0F, 0.45F);
        }
        else
            return new Color(0.7333333f, 0.7843138f, 0.8784314f, 1f);
    }

    public static Color ColorGetting(int color) {
        if (color != 0) {
            if (color == 1)
                return new Color(0F, 0.2509804F, 1F, 0F);
            else if (color == 2)
                return new Color(0F, 1F, 0.1098039F, 0F);
            else 
                return new Color(1F, 0F, 0F, 0F);
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
            else if (move == 4)
                return blue_scratch;
            else if (move == 5)
                return green_scratch;
            else if (move == 6)
                return red_scratch;
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

    public IEnumerator CreatePrefab(int col, int moven, int num) {
        GameObject create = PrefabGetting(moven);
        GameObject newChild = GameObject.Instantiate(create) as GameObject;
        newChild.transform.SetParent(ContentPrefab.transform);
        Image child_obj = newChild.transform.GetChild(0).GetComponent<Image>();
        Color color_child = child_obj.color;
        if (newChild.transform.GetSiblingIndex() == 0)
            newChild.transform.localScale = new Vector3(0.22F, 0.75F, 0);
        else
            newChild.transform.localScale = new Vector3(0.18F, 0.52F, 0);
        Image background;
        background = newChild.GetComponent<Image>();
        Color color = ColorGetting(col);
        float a = ColorIndeed(col).a;
        float alfa = (color_child.a - a)/20;
        ForFunc(num);
        float speed = robot.fade_speed;
        if (speed != 0) {
            for (float f = 0f; f <= a; f += a/20) {
                if (new_speed && f > 0)
                    new_speed = false;
                speed = robot.fade_speed;
                color.a = f;
                color_child.a = f+alfa;
                background.color = color;                
                child_obj.color = color_child;
                if (speed != 0)
                    yield return new WaitForSeconds(speed);
                else {
                    color.a = a;
                    background.color = color;
                    yield break;
                }
            }
        }
        else {
            color.a = a;
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

    public void action_chose(int move, int but_num = -1) {
        Image chill;
        if (but_num == -1)
            chill = InputField.button_list[btn].transform.GetChild(0).gameObject.GetComponent<Image>();
        else
            chill = InputField.button_list[but_num].transform.GetChild(0).gameObject.GetComponent<Image>();
        chill.sprite = s1[move];
        chill.transform.localScale = scale_for_move(move);
        chill.color = new Color(chill.color.r, chill.color.g, chill.color.b, 1F);        
    }

    public void Red_button() {
            btn = InputField.button;
            fun = InputField.func;
            InputField.button_list[btn].image.color = new Color(1F, 0F, 0F, 0.45F);
            Btnplay.func[fun].input_arr[btn].color = (int) Input_Class.Colors.Red;
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
            btn = InputField.button;
            fun = InputField.func;
            InputField.button_list[btn]. image.color = new Color(0F, 0.2509804F, 1F, 0.45F);
            Btnplay.func[fun].input_arr[btn].color = (int) Input_Class.Colors.Blue;
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
            btn = InputField.button;
            fun = InputField.func;
            InputField.button_list[btn].image.color = new Color(0F, 1F, 0.1098039F, 0.45F);
            Btnplay.func[fun].input_arr[btn].color = (int) Input_Class.Colors.Green;
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(6);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f1;
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(7);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f2;
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(8);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f3;
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(9);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f4;
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(10);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f5;
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(0);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.forward;
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(2);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.left; 
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
            btn = InputField.button;
            fun = InputField.func;
            action_chose(1);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.right;
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
    public void blue_scatch() {
            btn = InputField.button;
            fun = InputField.func;
            action_chose(3);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.scatch_blue;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 4;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 4;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 4;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 4;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 4;
    }

    public void green_scatch() {
            btn = InputField.button;
            fun = InputField.func;
            action_chose(4);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.scatch_green;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 5;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 5;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 5;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 5;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 5;
    }

    public void red_scatch() {
            btn = InputField.button;
            fun = InputField.func;
            action_chose(5);
            Btnplay.func[fun].input_arr[btn].direct = (int) Input_Class.Directs.scatch_red;
            if (fun == 0) 
                Savegame.sv.moves1[btn, 0] = 6;
            else if (fun == 1)
                Savegame.sv.moves2[btn, 0] = 6;
            else if (fun == 2)
                Savegame.sv.moves3[btn, 0] = 6;
            else if (fun == 3)
                Savegame.sv.moves4[btn, 0] = 6;
            else if (fun == 4)
                Savegame.sv.moves5[btn, 0] = 6;
    }
}
