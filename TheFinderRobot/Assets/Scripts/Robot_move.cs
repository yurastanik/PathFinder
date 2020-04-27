using System.Collections;
using System;
using System.IO;
using UnityEngine;

public class Robot_move : MonoBehaviour {

    [SerializeField] public GameObject buttons;
    [SerializeField] public GameObject funcs;
    [SerializeField] public GameObject inputs;
    Button_play input;
    Inputbuttons func;
    Choosebutton button;
    private int[,] card;
    private int[,] movesf1;
    private int[,] targets;
    private Vector2Int startPos;
    private string currentDirection;
    private Animator anim;    
    private MapLoader loader;    
    private Vector3 positionToRun;
    private Quaternion currentPosLR;
    private Vector3 firstPos;
    bool isPause = false;
    string movename;

    public bool readyToStart = true;
    bool isActive = false;
    bool isDel = false;
    bool switchLst = false;
    bool looser = false;
    bool winner = false;
    int i = 0;
    int movenum = 0;
    int winnum = 0;
    int currentMap = 1;
    private int[,] movesf2;
    private int[,] movesf3;
    private int[,] movesf4;
    private int[,] movesf5;
    private int[,] quene = new int[0,0];


    private void LoadMap() {
        func = funcs.GetComponent<Inputbuttons>();
        button = buttons.GetComponent<Choosebutton>();
        input = inputs.GetComponent<Button_play>();
        loader = GameObject.Find("Map").GetComponent<MapLoader>();
        Map loadedMap = loader.GetMap();
        card = MapLoader.OneDToTwoDArray(loadedMap.map, loadedMap.mapWidth);
        targets = MapLoader.OneDToTwoDArray(loadedMap.targets, 2);
        startPos = loadedMap.startPos;
        currentDirection = loadedMap.direction;
    }

    private void Level() {
        LoadMap();
        AtStart();  
        transform.position = new Vector3(startPos.y * 2, 0, startPos.x * -2);
        DirectAtStart();
    }

    public void MovesInit(int[,] moves1, int[,] moves2, int[,] moves3, int[,] moves4, int[,] moves5) {
        if (readyToStart && !isActive){
            button.HideButtons();
            func.gameObject.SetActive(false);
            movesf1 = moves1;
            movesf2 = moves2;
            movesf3 = moves3;
            movesf4 = moves4;
            movesf5 = moves5;
            isActive = true;
            readyToStart = false;
            StopCoroutine(MovesHandlerF1());
            StartCoroutine(MovesHandlerF1());
        }
    }

    public void Start() {
        ForTest();
        ForTest(15);
        anim = GetComponent<Animator>();
        Level();
    }

    public void ForTest(int i = 12) {
        Debug.Log(i);
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Alpha6))
            anim.speed = 6;
        if (Input.GetKeyUp(KeyCode.P)) {
            if (!isPause) {
                Time.timeScale = 0;
                isPause = true;
            }
            else {
                Time.timeScale = 1;
                isPause = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Alpha7))
            anim.speed = 7;
        if (Input.GetKeyUp(KeyCode.Alpha8))
            anim.speed = 8;
        if (Input.GetKeyUp(KeyCode.Alpha9))
            anim.speed = 9;
        if (Input.GetKeyUp(KeyCode.Alpha1))
            anim.speed = 1;
        if (Input.GetKeyUp(KeyCode.Alpha4))
            anim.speed = 4;
        if (Input.GetKeyUp(KeyCode.Alpha5))
            anim.speed = 5;
        if (Input.GetKeyUp(KeyCode.Alpha2))
            anim.speed = 2;        
        if (Input.GetKeyUp(KeyCode.Alpha3)) 
            anim.speed = 3;
    }

    private static void SizeReArray(ref int[,] arr, int num) {
        int[,] newArray = new int[arr.GetLength(0)-1,2];
        var k = 0;
        for (int m = 0; m < arr.GetLength(0); m++) {
            for (int n = 0; n < arr.GetLength(1); n++) {
                if (m == num)
                    k = 1;              
                else
                    newArray[m-k, n] = arr[m, n];
            }
        }
        arr = newArray;        
    }

    public static void ResizeArray(ref int[,] arr, int newM) {
        int[,] newArray = new int[arr.GetLength(0)+newM,2];
        for (int m = 0; m < arr.GetLength(0); m++) {
            for (int n = 0; n < arr.GetLength(1); n++) {
                newArray[m+newM, n] = arr[m, n];
            }
        }
        arr = newArray;
    }

    private void DirectAtStart() {
        if (currentDirection == "up") 
            anim.transform.rotation = Quaternion.Euler(0, 180, 0);        
        else if (currentDirection == "right") 
            anim.transform.rotation = Quaternion.Euler(0, -90, 0);        
        else if (currentDirection == "left") 
            anim.transform.rotation = Quaternion.Euler(0, 90, 0);
        else 
            anim.transform.rotation = Quaternion.Euler(0, 0, 0);         
    }

    private void CheckMoveType(ref int[,] arr) {
        if (arr[movenum, 0] == 1) {
            if (arr[movenum, 1] == 0 || arr[movenum, 1] == card[startPos.x, startPos.y]) {
                FaceDirection(currentDirection);
                CheckTarget();
                if (arr == quene) {
                    SizeReArray(ref quene, movenum);
                    movenum -= 1;
                    i -= 1;
                }
            }
        }
        else if (arr[movenum, 0] == 2) {
            if (arr[movenum, 1] == 0 || arr[movenum, 1] == card[startPos.x, startPos.y]) {
                ChangeDirection(currentDirection, 2);
                if (arr == quene) {
                    SizeReArray(ref quene, movenum);
                    movenum -= 1;
                    i -= 1;
                }
            }
        } 
        else if (arr[movenum, 0] == 3) {
            if (arr[movenum, 1] == 0 || arr[movenum, 1] == card[startPos.x, startPos.y]) {
                ChangeDirection(currentDirection, 3);
                if (arr == quene) {
                    SizeReArray(ref quene, movenum);
                    movenum -= 1;
                    i -= 1;
                }
            }
        }
        else if (arr[movenum, 0] == 4) {
            if (arr[movenum, 1] == 0 || arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (card[startPos.x, startPos.y] != 1) {
                    card[startPos.x, startPos.y] = 1;
                    loader.OnMapUpdate(card);
                }
                if (arr == quene) {
                    SizeReArray(ref quene, movenum);
                    movenum -= 1;
                    i -= 1;
                }            
            }
        }
        else if (arr[movenum, 0] == 5){
            if (arr[movenum, 1] == 0 || arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (card[startPos.x, startPos.y] != 2) {
                    card[startPos.x, startPos.y] = 2;
                    loader.OnMapUpdate(card);
                }
                if (arr == quene) {
                    SizeReArray(ref quene, movenum);
                    movenum -= 1;
                    i -= 1;
                }
            }
        }
        else if (arr[movenum, 0] == 6){
            if (arr[movenum, 1] == 0 || arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (card[startPos.x, startPos.y] != 3) {
                    card[startPos.x, startPos.y] = 3;
                    loader.OnMapUpdate(card);
                }
                if (arr == quene) {
                    SizeReArray(ref quene, movenum);
                    movenum -= 1;
                    i -= 1;
                }
            }
        }
        else if (arr[movenum, 0] == 7) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF1());                
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                int integer = (arr.GetLength(0) - movenum)-1;
                ResizeArray(ref quene, integer);
                for (int j = 0; j < integer; j++) {                        
                    quene[j, 0] = arr[movenum+(j+1), 0];
                    quene[j, 1] = arr[movenum+(j+1), 1];           
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF1());
            }
        }
        else if (arr[movenum, 0] == 8) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF2());
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                int integer = (arr.GetLength(0) - movenum)-1;
                ResizeArray(ref quene, integer);
                for (int j = 0; j < integer; j++) {                        
                    quene[j, 0] = arr[movenum+(j+1), 0];
                    quene[j, 1] = arr[movenum+(j+1), 1];
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF2());
            }
        }
        else if (arr[movenum, 0] == 9) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF3());
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                int integer = (arr.GetLength(0) - movenum)-1;
                ResizeArray(ref quene, integer);
                for (int j = 0; j < integer; j++) {
                    quene[j, 0] = arr[movenum+(j+1), 0];
                    quene[j, 1] = arr[movenum+(j+1), 1];           
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF3());
            }
        }
        else if (arr[movenum, 0] == 10) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF4());
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                int integer = (arr.GetLength(0) - movenum)-1;
                ResizeArray(ref quene, integer);
                for (int j = 0; j < integer; j++) {                        
                    quene[j, 0] = arr[movenum+(j+1), 0];
                    quene[j, 1] = arr[movenum+(j+1), 1];           
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF4());
            }
        }
        else if (arr[movenum, 0] == 11) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF5());
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                int integer = (arr.GetLength(0) - movenum)-1;
                ResizeArray(ref quene, integer);
                for (int j = 0; j < integer; j++) {                        
                    quene[j, 0] = arr[movenum+(j+1), 0];
                    quene[j, 1] = arr[movenum+(j+1), 1];           
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF5());               
            }
        }
    }

    private void ChangeDirection(string direction, int move) {
        Animka(move);
        if (move == 2) {
            if (direction == "down") {
                currentDirection = "left";
            }
            else if (direction == "up") {
                currentDirection = "right";
            }
            else if (direction == "left") {
                currentDirection = "up";
            }
            else {
                currentDirection = "down";
            }
        }
        else {
            if (direction == "down") {
                currentDirection = "right";
            }
            else if (direction == "up") {
                currentDirection = "left";
            }
            else if (direction == "left") {
                currentDirection = "down";
            }
            else {
                currentDirection = "up";
            }
        }
    }

    private Vector2Int LeftRight(string direction) {
        if (direction == "down")
            return new Vector2Int(0, -2);
        else if (direction == "up")
            return new Vector2Int(0, 2);
        else if (direction == "left")
            return new Vector2Int(-2, 0);
        else
            return new Vector2Int(2, 0);
    }

    private void FaceDirection(string direction) {
        if (direction == "down") {
            SetPosition(new Vector2Int(0, 1));
            if (card[startPos.x, startPos.y] == 0)
                isActive = false;
                looser = true;
        }
        else if (direction == "up") {
            SetPosition(new Vector2Int(0, -1));
            if (card[startPos.x, startPos.y] == 0)
                isActive = false;
                looser = true;
        }
        else if (direction == "left") {
            SetPosition(new Vector2Int(-1, 0));
            if (card[startPos.x, startPos.y] == 0)
                isActive = false;
                looser = true;
        }
        else {
            SetPosition(new Vector2Int(1, 0));
            if (card[startPos.x, startPos.y] == 0)
                isActive = false;
                looser = true;
        }
    }

    private void CheckTarget() {
        for (int target = 0; target <= targets.GetUpperBound(0); target++) {
            if (startPos.x == targets[target, 0] && startPos.y == targets[target, 1]) {
                Debug.Log("Picked star " + (target + 1));
                Array.Clear(targets, target*2, 2);
                GameWinner();
            }
        }
    }
 
    private void GameWinner() {
        foreach (int win in targets) {
            if (win == 0)
                winnum += 1;
        }
        if (winnum == targets.Length) {
            Debug.Log("You Win!!!");
            NextMap();
       }
        else
            winnum = 0;
    }

    private void SetPosition(Vector2Int newPos) {
        Animka(1);
        startPos.x += newPos.y;
        startPos.y += newPos.x;
    }

    private void AtStart() {
        isActive = false;
        readyToStart = true;
        winner = false;
        looser = false;
        switchLst = false;
        i = 0;
        movenum = 0;
        winnum = 0;
        movesf1 = new int[0,0];
        movesf2 = new int[0,0];
        movesf3 = new int[0,0];
        movesf4 = new int[0,0];
        movesf5 = new int[0,0];
        quene = new int[0,0];
    }

    private void NextMap() {   
        isActive = false;
        winner = true;
        currentMap += 1;
    }

    private string IntToMove(int move, bool doit = true) {
        if (doit) {
            if (move == 1) {
                anim.Play("Run", 0);
                return "Run";
            }
            else if (move == 2) {
                anim.Play("Right", 0);
                return "Right";
            }
            else if (move == 3) {
                anim.Play("Left", 0);
                return "Left";            
            }
            else if (move == 4) {
                return "Scratch";
            }
            return "Other";
        }
        else {
            if (move == 1) {
                return "Run";
            }
            else if (move == 2) {
                return "Right";
            }
            else if (move == 3) {
                return "Left";            
            }
            else if (move == 4) {
                return "Scratch";
            }
            return "Other";
        }
    }
 
    private void Animka(int mov) {
        IntToMove(mov);
    }

    private IEnumerator MovesHandlerQuene() {
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i <= quene.GetLength(0); i++) {
            if (isActive) {
                movename = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (quene[movenum, 1] == card[startPos.x, startPos.y] || quene[movenum, 1] == 0 || quene[movenum, 0] == 4) {
                    movename = IntToMove(quene[movenum, 0], false);
                    if (movename == "Left" || movename == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref quene);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (movename == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (movename == "Scratch") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (movename == "Other") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (switchLst)
                    isDel = true;
                else
                    button.DestroyPrefab();
            }
            else {
                if (winner) {
                    yield return new WaitForSeconds(0.5f);
                    i = 0;
                    try{
                        loader.MapNext(currentMap);
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        button.ButtonLoad(false); 
                        func.FuncLoad(false);
                        input.ReStart();
                    }
                    catch(FileNotFoundException e) {
                        Debug.Log(e);
                    }
                    Level();
                    yield break;
                }
                else if (looser) {
                    anim.Play("Fall");
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));                    
                    Level();
                    button.ReturnAll();
                    func.gameObject.SetActive(true);
                    yield break;
                }
            }
            if (switchLst) {
                yield return new WaitForSeconds(0.5f);
                switchLst = false;
                yield break;
            }
        }
        Debug.Log("You Lose!!!");
        yield return new WaitForSeconds(0.7f);
        Level();
        button.ReturnAll();
        func.gameObject.SetActive(true);
        yield break;
    }

    private IEnumerator MovesHandlerF1() {
        yield return new WaitWhile(() => switchLst);
        for (int a = 0; a < movesf1.GetLength(0); a++) {
            StartCoroutine(button.CreatePrefab(movesf1[a, 1], movesf1[a, 0], quene.GetLength(0), a+1));
        }
        for (i = 0; i < movesf1.GetLength(0); i++) {
            if (isActive) {
                if (isDel) {
                    yield return new WaitForSeconds(0.5f);
                    button.DestroyPrefab();
                    isDel = false;
                }
                movename = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf1[movenum, 1] == card[startPos.x, startPos.y] || movesf1[movenum, 1] == 0 || movesf1[movenum, 0] == 4) {
                    movename = IntToMove(movesf1[movenum, 0], false);
                    if (movename == "Left" || movename == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf1);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (movename == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (movename == "Scratch") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (movename == "Other") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (switchLst)
                    isDel = true;
                else
                    button.DestroyPrefab();
            }
            else {
                if (winner) {
                    yield return new WaitForSeconds(0.5f);
                    i = 0;
                    try{
                        loader.MapNext(currentMap);
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        button.ButtonLoad(false);     
                        func.FuncLoad(false);
                        input.ReStart();              
                    }
                    catch(FileNotFoundException e) {
                        Debug.Log(e);
                    }
                    Level();
                    yield break;
                }
                else if (looser) {
                        anim.Play("Fall");
                        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        Level();
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        yield break;                
                }
            }
            if (switchLst) {
                yield return new WaitForSeconds(0.5f);
                switchLst = false;
                yield break;
            }
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
        else {
            Debug.Log("You Lose!!!");
            yield return new WaitForSeconds(0.7f);
            Level();
            button.ReturnAll();
            func.gameObject.SetActive(true);
            yield break;
        }
    }

    private IEnumerator MovesHandlerF2() {
        yield return new WaitWhile(() => switchLst);
        for (int a = 0; a < movesf2.GetLength(0); a++) {
            StartCoroutine(button.CreatePrefab(movesf2[a, 1], movesf2[a, 0], quene.GetLength(0), a));
        }
        for (i = 0; i < movesf2.GetLength(0); i++) {
            if (isActive) {
                if (isDel) {
                    yield return new WaitForSeconds(0.3f);
                    button.DestroyPrefab();
                    isDel = false;
                }
                movename = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf2[movenum, 1] == card[startPos.x, startPos.y] || movesf2[movenum, 1] == 0 || movesf2[movenum, 0] == 4) {
                    movename = IntToMove(movesf2[movenum, 0], false);
                    if (movename == "Left" || movename == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf2);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (movename == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (movename == "Scratch") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (movename == "Other") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (switchLst)
                    isDel = true;
                else
                    button.DestroyPrefab();
            }
            else {
                if (winner) {
                    yield return new WaitForSeconds(0.5f);
                    i = 0;
                    try{
                        loader.MapNext(currentMap); 
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        button.ButtonLoad(false);    
                        func.FuncLoad(false);
                        input.ReStart();               
                    }
                    catch(FileNotFoundException e) {
                        Debug.Log(e);
                    }
                    Level();
                    yield break;
                }
                else if (looser) {
                        anim.Play("Fall");
                        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        Level();
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        yield break;                
                }
            }
            if (switchLst) {
                yield return new WaitForSeconds(0.5f);
                switchLst = false;
                yield break;
            }
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
        else {
            Debug.Log("You Lose!!!");
            yield return new WaitForSeconds(0.7f);
            Level();
            button.ReturnAll();
            func.gameObject.SetActive(true);
            yield break;
        }
    }

    private IEnumerator MovesHandlerF3() {
        yield return new WaitWhile(() => switchLst);
        for (int a = 0; a < movesf3.GetLength(0); a++) {
            StartCoroutine(button.CreatePrefab(movesf3[a, 1], movesf3[a, 0], quene.GetLength(0), a+1));
        }        
        for (i = 0; i < movesf3.GetLength(0); i++) {
            if (isActive) {
                if (isDel) {
                    yield return new WaitForSeconds(1f);
                    button.DestroyPrefab();
                    isDel = false;
                }
                movename = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf3[movenum, 1] == card[startPos.x, startPos.y] || movesf3[movenum, 1] == 0 || movesf3[movenum, 0] == 4) {
                    movename = IntToMove(movesf3[movenum, 0], false);
                    if (movename == "Left" || movename == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf3);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (movename == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (movename == "Scratch") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (movename == "Other") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (switchLst)
                    isDel = true;
                else
                    button.DestroyPrefab();
            }
            else {
                if (winner) {
                    yield return new WaitForSeconds(0.5f);
                    i = 0;
                    try{
                        loader.MapNext(currentMap); 
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        button.ButtonLoad(false);   
                        func.FuncLoad(false); 
                        input.ReStart();                
                    }
                    catch(FileNotFoundException e) {
                        Debug.Log(e);
                    }
                    Level();
                    yield break;
                }
                else if (looser) {
                        anim.Play("Fall");
                        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        Level();
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        yield break;                
                }
            }
            if (switchLst) {
                yield return new WaitForSeconds(0.5f);
                switchLst = false;
                yield break;
            }
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
        else {
            Debug.Log("You Lose!!!");
            yield return new WaitForSeconds(0.7f);
            Level();
            button.ReturnAll();
            func.gameObject.SetActive(true);
            yield break;
        }
    }

    private IEnumerator MovesHandlerF4() {
        yield return new WaitWhile(() => switchLst);
        for (int a = 0; a < movesf4.GetLength(0); a++) {
            StartCoroutine(button.CreatePrefab(movesf4[a, 1], movesf4[a, 0], quene.GetLength(0), a));
        }
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i < movesf4.GetLength(0); i++) {
            if (isActive) {
                if (isDel) {
                    yield return new WaitForSeconds(0.3f);
                    button.DestroyPrefab();
                    isDel = false;
                }
                movename = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf4[movenum, 1] == card[startPos.x, startPos.y] || movesf4[movenum, 1] == 0 || movesf4[movenum, 0] == 4) {
                    movename = IntToMove(movesf4[movenum, 0], false);
                    if (movename == "Left" || movename == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf4);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (movename == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (movename == "Scratch") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (movename == "Other") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (switchLst)
                    isDel = true;
                else
                    button.DestroyPrefab();
            }
            else {
                if (winner) {
                    yield return new WaitForSeconds(0.5f);
                    i = 0;
                    try{
                        loader.MapNext(currentMap);
                        button.ReturnAll();
                        func.gameObject.SetActive(true);  
                        button.ButtonLoad(false);    
                        func.FuncLoad(false);
                        input.ReStart();               
                    }
                    catch(FileNotFoundException e) {
                        Debug.Log(e);
                    }
                    Level();
                    yield break;
                }
                else if (looser) { 
                        anim.Play("Fall");
                        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        Level();
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        yield break;                
                }
            }
            if (switchLst) {
                yield return new WaitForSeconds(0.5f);
                switchLst = false;
                yield break;
            }
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
        else {
            Debug.Log("You Lose!!!");
            yield return new WaitForSeconds(0.7f);
            Level();
            button.ReturnAll();
            func.gameObject.SetActive(true);
            yield break;
        }
    }

    private IEnumerator MovesHandlerF5() {
        yield return new WaitWhile(() => switchLst);
        for (int a = 0; a < movesf5.GetLength(0); a++) {
            StartCoroutine(button.CreatePrefab(movesf5[a, 1], movesf5[a, 0], quene.GetLength(0), a));
        }
        for (i = 0; i < movesf5.GetLength(0); i++) {
            if (isActive) {
                if (isDel) {
                    yield return new WaitForSeconds(0.3f);
                    button.DestroyPrefab();
                    isDel = false;
                }
                movename = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf5[movenum, 1] == card[startPos.x, startPos.y] || movesf5[movenum, 1] == 0 || movesf5[movenum, 0] == 4) {
                    movename = IntToMove(movesf5[movenum, 0], false);
                    if (movename == "Left" || movename == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf5);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (movename == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (movename == "Scratch") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (movename == "Other") {
                    yield return new WaitForSeconds(0.5f);
                }
                if (switchLst)
                    isDel = true;
                else
                    button.DestroyPrefab();
            }
            else {
                if (winner) {
                    yield return new WaitForSeconds(0.5f);
                    i = 0;
                    try{
                        loader.MapNext(currentMap);
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        button.ButtonLoad(false);
                        func.FuncLoad(false);
                        input.ReStart();
                    }
                    catch(FileNotFoundException e) {
                        Debug.Log(e);
                    }
                    Level();
                    yield break;
                }
                else if (looser) {
                        anim.Play("Fall");
                        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        Level();
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        yield break;                
                }
            }
            if (switchLst) {
                yield return new WaitForSeconds(0.5f);
                switchLst = false;
                yield break;
            }
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
        else {
            Debug.Log("You Lose!!!");
            yield return new WaitForSeconds(0.7f);
            Level();
            button.ReturnAll();
            func.gameObject.SetActive(true);
            yield break;
        }
    }
}
