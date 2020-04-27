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

    private int[,] movesf1;
    private int[,] movesf2;
    private int[,] movesf3;
    private int[,] movesf4;
    private int[,] movesf5;
    private int[,] movesf6;
    private int[] colors;
    private int[] moves = new int[] {1, 2, 3, 7};
    private GameObject create;
    private MapLoader loader;
    private int btn;
    private int fun;
    public Sprite[] s1;
    public bool fade = false;
    public List<int> func_num = new List<int>();


    public void Awake() {
        s1 = Resources.LoadAll<Sprite>("Sprites/Button/input_button");
        ButtonLoad(true);
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
        moves = new int[] {1, 2, 3, 7}; 
        func_num = new List<int>();
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
        foreach (int i in moves) {
            button_list[i].gameObject.SetActive(true);
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
            if (arr[a, 0] > 3 && arr[a, 0] != 7) 
                AddAction(ref moves, arr[a, 0]);
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
    }

    public IEnumerator DestroyPrefab(int num = 0) {
        Transform child = ContentPrefab.transform.GetChild(num);
        StartCoroutine(UnvisiblePrefab(child));
        // yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
        // yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
        // GameObject.Destroy(child.gameObject);
        yield break;
    }

    public void ReturnAll() {
        foreach (Transform child in ContentPrefab.transform)
            GameObject.Destroy(child.gameObject);
        Content.gameObject.SetActive(true);
        RectTransform panelka = Panel.GetComponent<RectTransform>();
        ScrollRect scroll = Panel.GetComponent<ScrollRect>();
        RectTransform pict = Content.GetComponent<RectTransform>();
        panelka.localPosition = new Vector3(-29, -477.525f, 0);
        panelka.sizeDelta = new Vector2(0, -24.35027f);
        scroll.content = pict;
    }

    public IEnumerator UnvisiblePrefab(Transform child) {
        fade = true;
        Debug.Log("start " + child.name);
        Image background;
        background = child.GetComponent<Image>();
        Color color = background.color;
        for (float f = 0.95f; f >= 0; f -= 0.05f) {
            color.a = f;
            background.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        Debug.Log("end " + child.name);
        GameObject.Destroy(child.gameObject);
        fade = false;
        yield break;
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

    public IEnumerator CreatePrefab(int col, int moven, int check, int num) {
        GameObject create = PrefabGetting(moven);
        GameObject newChild = GameObject.Instantiate(create) as GameObject;
        newChild.transform.parent = ContentPrefab.transform;
        newChild.transform.localScale = new Vector3(0.18F, 0.52F, 0);
        Image background;
        background = newChild.GetComponent<Image>();
        Color color = ColorGetting(col);
        if (check >= 1)
            ForFunc(num);
        for (float f = 0.05f; f <= 1; f += 0.05f) {
            color.a = f;
            background.color = color;
            yield return new WaitForSeconds(0.05f);
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
    public void f1_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[1];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f1;
    }
    public void f2_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[2];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f2;
    }
    public void f3_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[3];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f3;
    }
    public void f4_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[4];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f4;
    }

    public void f5_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[5];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.f5;
    }
    public void Top_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[7];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.forward;
    }
    public void Left_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            fun = InputField.GetComponent<Inputbuttons>().func;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[6];
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.left;
    }
    public void Right_button() {
            btn = InputField.GetComponent<Inputbuttons>().button;
            InputField.GetComponent<Inputbuttons>().button_list[btn].image.sprite = s1[8];
            fun = InputField.GetComponent<Inputbuttons>().func;
            Btnplay.GetComponent<Button_play>().func[fun].input_arr[btn].direct = (int) Input_Class.Directs.right;
    }
}
