using System.Collections;
using System;
using System.IO;
using UnityEngine;

public class Robot_move : MonoBehaviour {
    [SerializeField] private Button_play input;
    [SerializeField] private Inputbuttons func;
    [SerializeField] private Choosebutton button;
    private int[,] card;
    private int[,] movesf1;
    private int[,] targets;
    private Vector2Int startPos;
    private string currentDirection;
    private Animator anim;
    [SerializeField] private MapLoader loader;
    private Vector3 positionToRun;
    private Quaternion currentPosLR;
    private Vector3 firstPos;
    bool isPause = false;
    string movename;

    public float fade_speed = 0.03f;
    public bool readyToStart = true;
    bool isActive = false;
    bool isDel = false;
    bool switchLst = false;
    bool looser = false;
    bool winner = false;
    bool trans = false;
    bool isDestroy = false;
    float n = -2;
    int i = 0;
    int t = 0;
    int movenum = 0;
    int winnum = 0;
    int currentMap = Savegame.sv.mapNum;
    private int[,] allarrays;
    private int[,] movesf2;
    private int[,] movesf3;
    private int[,] movesf4;
    private int[,] movesf5;
    private int[,] quene = new int[0,0];
    private int[,] targeti;


    private void LoadMap(bool lose = false) {
        Map loadedMap = loader.GetMap();
        card = MapLoader.OneDToTwoDArray(loadedMap.map, loadedMap.mapWidth);
        targeti = MapLoader.OneDToTwoDArray(loadedMap.targets, 2);
        targets = targeti;
        startPos = loadedMap.startPos;
        currentDirection = loadedMap.direction;
        n = -2;
        if (loadedMap.mapSize.y != 0)
            n *= loadedMap.mapSize.y;
        if (lose)
            loader.OnMapUpdate(card, targeti);
    }

    // Check Ground Function For SprayParticles.cs
    public bool CheckPosition() {
        if (card[startPos.x, startPos.y] != 0)
            return true;
        return false;
    }

    private void Level(bool loser = false) {
        LoadMap(loser);
        AtStart();
        DirectAtStart();
        transform.position = new Vector3(startPos.y * 2, 0, (float)Math.Floor(startPos.x * n));
    }

    public void MovesInit(int[,] moves1, int[,] moves2, int[,] moves3, int[,] moves4, int[,] moves5) {
        if (readyToStart && !isActive){
            trans = false;
            button.HideButtons();
            func.gameObject.SetActive(false);
            movesf1 = moves1;
            movesf2 = moves2;
            movesf3 = moves3;
            movesf4 = moves4;
            movesf5 = moves5;
            isActive = true;
            readyToStart = false;
            allarrays = movesf1;
            StartCoroutine(MovesHandler());
        }
    }

    public void StopGame() {
        Time.timeScale = 1;
        isPause = false;
        StopAllCoroutines();
        button.ReturnAll(true);
        func.gameObject.SetActive(true);
        anim.Play("Stay", 0);
        Level(true);
        trans = true;
    }

    public void PauseGame() {
        if (!readyToStart) {
            if (!isPause) {
                Time.timeScale = 0;
                isPause = true;
            }
            else {
                Time.timeScale = 1;
                isPause = false;
            }
        }
    }

    public void Start() {
        anim = GetComponent<Animator>();
        tot_speed();
        Level();
    }

    public void tot_speed() {
        anim.speed = Savegame.sv.speed;
        if (anim.speed == 2)
            fade_speed = 0.002f;
        else if (anim.speed == 4)
            fade_speed = 0;
        else
            fade_speed = 0;
    }

    private void FixedUpdate() {
        if (trans) {
            t += 1;
            if (t==2) {
                trans = false;
                t = 0;
            }
        }
    }

    private void Update() {
        if (trans) {
            transform.position = new Vector3(startPos.y * 2, 0, (float)Math.Floor(startPos.x * n));
            DirectAtStart();
        }
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
                allarrays = movesf1;
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (quene.GetLength(0) <= 100) {
                    int integer = (arr.GetLength(0) - movenum)-1;
                    ResizeArray(ref quene, integer);
                    for (int j = 0; j < integer; j++) {
                        quene[j, 0] = arr[movenum+(j+1), 0];
                        quene[j, 1] = arr[movenum+(j+1), 1];
                    }
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf1;
            }
        }
        else if (arr[movenum, 0] == 8) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf2; 
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (quene.GetLength(0) <= 100) {
                    int integer = (arr.GetLength(0) - movenum)-1;
                    ResizeArray(ref quene, integer);
                    for (int j = 0; j < integer; j++) {
                        quene[j, 0] = arr[movenum+(j+1), 0];
                        quene[j, 1] = arr[movenum+(j+1), 1];
                    }
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf2; 
            }
        }
        else if (arr[movenum, 0] == 9) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf3;
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (quene.GetLength(0) <= 100) {
                    int integer = (arr.GetLength(0) - movenum)-1;
                    ResizeArray(ref quene, integer);
                    for (int j = 0; j < integer; j++) {
                        quene[j, 0] = arr[movenum+(j+1), 0];
                        quene[j, 1] = arr[movenum+(j+1), 1];
                    }
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf3; 
            }
        }
        else if (arr[movenum, 0] == 10) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf4;
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (quene.GetLength(0) <= 100) {
                    int integer = (arr.GetLength(0) - movenum)-1;
                    ResizeArray(ref quene, integer);
                    for (int j = 0; j < integer; j++) {
                        quene[j, 0] = arr[movenum+(j+1), 0];
                        quene[j, 1] = arr[movenum+(j+1), 1];
                    }
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf4;
            }
        }
        else if (arr[movenum, 0] == 11) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf5; 
            }
            else if (arr[movenum, 1] == card[startPos.x, startPos.y]) {
                if (quene.GetLength(0) <= 100) {
                    int integer = (arr.GetLength(0) - movenum)-1;
                    ResizeArray(ref quene, integer);
                    for (int j = 0; j < integer; j++) {
                        quene[j, 0] = arr[movenum+(j+1), 0];
                        quene[j, 1] = arr[movenum+(j+1), 1];
                    }
                }
                switchLst = true;
                movenum = -1;
                allarrays = movesf5;
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
            if (card[startPos.x, startPos.y] == 0) {
                isActive = false;
                looser = true;
            }
        }
        else if (direction == "up") {
            SetPosition(new Vector2Int(0, -1));
            if (card[startPos.x, startPos.y] == 0) {
                isActive = false;
                looser = true;
            }
        }
        else if (direction == "left") {
            SetPosition(new Vector2Int(-1, 0));
            if (card[startPos.x, startPos.y] == 0) {
                isActive = false;
                looser = true;
            }
        }
        else {
            SetPosition(new Vector2Int(1, 0));
            if (card[startPos.x, startPos.y] == 0) {
                isActive = false;
                looser = true;
            }
        }
    }

    private void CheckTarget() {
        for (int target = 0; target <= targets.GetUpperBound(0); target++) {
            if (startPos.x == targets[target, 0] && startPos.y == targets[target, 1]) {
                Array.Clear(targets, target*2, 2);
#if UNITY_EDITOR
                Debug.Log("x - " + startPos.x + "; y - " + startPos.y);
#endif
                GameWinner();
            }
        }
    }

    private int[,] PointDes(int[,] points) {
        int nu = 0;
        for (int x = 0; x < points.GetLength(0); x++) {
            if (points[x, 0] == 0 && points[x, 1] == 0) {
                nu += 1;
            }
        }
        int[,] new_targ = new int[points.GetLength(0)-nu, 2];
        int n = 0;
        for (int i = 0; i < points.GetLength(0); i++) {
            if (points[i, 0] != 0 || points[i, 1] != 0) {
                new_targ[i-n, 0] = points[i, 0];
                new_targ[i-n, 1] = points[i, 1];
            }
            else {
                n += 1;
            }
        }
        return new_targ;
    }
 
    private void GameWinner() {
        foreach (int win in targets) {
            if (win == 0)
                winnum += 1;
        }
        if (winnum == targets.Length) {
#if UNITY_EDITOR
            Debug.Log("You Win!!!");
#endif
            NextMap();
        }
        else {
            winnum = 0;
            isDestroy = true;
        }
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
#if UNITY_EDITOR
        Debug.Log(currentMap);
#endif
        if (currentMap <= 0)
            currentMap -= 2;
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
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, 0, currentPosRun.z + (newpos.y * (n/-2)));
                    }
                }
                CheckMoveType(ref quene);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") {
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                        anim.transform.position = new Vector3(currentPosRun.x, 0, currentPosRun.z);
                    }
                    else if (movename == "Right") {
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                        anim.transform.position = new Vector3(currentPosRun.x, 0, currentPosRun.z);
                    }
                    else {
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 3);
                    }
                }
                else if (movename == "Scratch") 
                    yield return new WaitForSeconds(0.1f);
                if (switchLst)
                    isDel = true;
                else {
                    yield return new WaitWhile(() => button.new_speed);
                    button.DestroyPrefab();
                    yield return new WaitWhile(() => button.fade);
                }
                if (winner) {
                    loader.OnMapUpdate(null, PointDes(targets));
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
#if UNITY_EDITOR
                        Debug.Log(e);
#endif
                    }
                    Level();
                    yield break;
                }
                else if (looser) {
                    anim.Play("Fall");
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));                    
                    Level(true);
                    button.ReturnAll(true);
                    func.gameObject.SetActive(true);
                    yield break;
                }
                else if (isDestroy) {
                    loader.OnMapUpdate(null, PointDes(targets));
                    isDestroy = false;
                }
            }            
            if (switchLst) {
                switchLst = false;
                StartCoroutine(MovesHandler());
                yield break;
            }
        }
        yield return new WaitForSeconds(0.7f);
        Level(true);
        button.ReturnAll(true);
        func.gameObject.SetActive(true);
        yield break;
    }   

    private IEnumerator MovesHandler() {
        yield return new WaitWhile(() => switchLst);
        for (int a = 0; a < allarrays.GetLength(0); a++) {
            StartCoroutine(button.CreatePrefab(allarrays[a, 1], allarrays[a, 0], a+1));
            if (button.ContentPrefab.transform.childCount > (100 + allarrays.GetLength(0))) {
                button.DestroyLastPrefab();
                yield return new WaitWhile(() => button.fade_last);
            }
        }
        for (i = 0; i < allarrays.GetLength(0); i++) {
            if (isActive) {
                if (isDel) {
                    yield return new WaitWhile(() => button.new_speed);
                    button.DestroyPrefab();
                    yield return new WaitWhile(() => button.fade);
                    isDel = false;
                }
                movename = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (allarrays[movenum, 1] == card[startPos.x, startPos.y] || allarrays[movenum, 1] == 0 || allarrays[movenum, 0] == 4) {
                    movename = IntToMove(allarrays[movenum, 0], false);
                    if (movename == "Left" || movename == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, 0, currentPosRun.z + (newpos.y * (n/-2)));
                    }
                } 
                CheckMoveType(ref allarrays);
                movenum += 1;
                if (movename != "Other" && movename != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(movename));
                    if (movename == "Left") {
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                        anim.transform.position = new Vector3(currentPosRun.x, 0, currentPosRun.z);
                    }
                    else if (movename == "Right") {
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                        anim.transform.position = new Vector3(currentPosRun.x, 0, currentPosRun.z);
                    }
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 3);
                }
                else if (movename == "Scratch") 
                    yield return new WaitForSeconds(0.1f);
                if (switchLst)
                    isDel = true;
                else {
                    yield return new WaitWhile(() => button.new_speed);
                    button.DestroyPrefab();
                    yield return new WaitWhile(() => button.fade);
                }
                if (winner) {
                    loader.OnMapUpdate(null, PointDes(targets));
                    yield return new WaitForSeconds(0.5f);
                    i = 0;
                    try {
                        loader.MapNext(currentMap);
                        button.ReturnAll();
                        func.gameObject.SetActive(true);
                        button.ButtonLoad(false);     
                        func.FuncLoad(false);
                        input.ReStart();              
                    }
                    catch(FileNotFoundException) {
                    }
                    Level();
                    yield break;
                }
                else if (looser) {
                        anim.Play("Fall");
                        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Fall"));
                        Level(true);
                        button.ReturnAll(true);
                        func.gameObject.SetActive(true);
                        yield break;                
                }
                else if (isDestroy) {
                    loader.OnMapUpdate(null, PointDes(targets));
                    isDestroy = false;
                }
            }
            if (switchLst) {
                switchLst = false;
                StartCoroutine(MovesHandler());
                yield break;
            } 
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
        else {
            yield return new WaitForSeconds(0.7f);
            Level(true);
            button.ReturnAll(true);
            func.gameObject.SetActive(true);
            yield break;
        }
    }
} 
