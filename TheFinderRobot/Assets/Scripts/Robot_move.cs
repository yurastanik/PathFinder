using System.Collections;
using System;
using System.IO;
using UnityEngine;

public class Robot_move : MonoBehaviour {

    private int[,] card;
    private int[,] movesf1;
    private int[,] movesf2;
    private int[,] movesf3;
    private int[,] movesf4;
    private int[,] movesf5;
    private int[,] movesf6;
    private int[,] targets;
    private int[,] quene = new int[0,0];
    private int[,] listik;

    private Animator anim;
    private Transform directionArrow;
    private Vector2Int startPos;
    private string currentDirection;
    private MapLoader loader;
    private Camera camer;
    private Vector3 positionToRun;
    private Quaternion currentPosLR;

    bool isActive = false;
    bool isPause = false;
    bool switchLst = false;

    int i = 0;
    int movenum = 0;
    int winnum = 0;
    int currentMap = 1;
    int forepeat = 0;
    private int moveslen;


    private void LoadMap() {
        loader = GameObject.Find("Map").GetComponent<MapLoader>();
        Map loadedMap = loader.GetMap();
        
        card = MapLoader.OneDToTwoDArray(loadedMap.map, loadedMap.mapWidth);
        movesf1 = MapLoader.OneDToTwoDArray(loadedMap.movesf1, 2);
        if (loadedMap.movesf2 != null) 
            movesf2 = MapLoader.OneDToTwoDArray(loadedMap.movesf2, 2);
        if (loadedMap.movesf3 != null) 
            movesf3 = MapLoader.OneDToTwoDArray(loadedMap.movesf3, 2);
        if (loadedMap.movesf4 != null) 
            movesf4 = MapLoader.OneDToTwoDArray(loadedMap.movesf4, 2);
        if (loadedMap.movesf5 != null) 
            movesf5 = MapLoader.OneDToTwoDArray(loadedMap.movesf5, 2);
        if (loadedMap.movesf6 != null)
            movesf6 = MapLoader.OneDToTwoDArray(loadedMap.movesf6, 2);
        targets = MapLoader.OneDToTwoDArray(loadedMap.targets, 2);
        startPos = loadedMap.startPos;
        currentDirection = loadedMap.direction;
        moveslen = movesf1.GetLength(0);
    }

    private void Level() {
        LoadMap();
        AtStart();
        camer = GameObject.Find("Main Camera").GetComponent<Camera>();
        camer.transform.position = new Vector3(startPos.y * 2, card.GetUpperBound(0)-1, -15);   
        transform.position = new Vector3(startPos.y * 2, 0, startPos.x * -2); 
        DirectAtStart();
    }

    private void Start() {
        anim = GetComponent<Animator>();        
        Level();
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
        if (Input.GetKeyUp(KeyCode.S) && !isActive) { 
            isActive = true;
            StopCoroutine(MovesHandlerF1());
            StartCoroutine(MovesHandlerF1());
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
        else if (arr[movenum, 0] == 12) {
            if (arr[movenum, 1] == 0 || (arr[movenum, 1] == card[startPos.x, startPos.y] && (movenum == arr.GetLength(0)-1 || arr == quene))){
                if (arr == quene) {
                    SizeReArray(ref arr, movenum);
                }
                switchLst = true;
                movenum = -1;
                StartCoroutine(MovesHandlerF6());
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
                StartCoroutine(MovesHandlerF6());
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
                GameOver();
        }
        else if (direction == "up") {
            SetPosition(new Vector2Int(0, -1));
            if (card[startPos.x, startPos.y] == 0)
                GameOver();
        }
        else if (direction == "left") {
            SetPosition(new Vector2Int(-1, 0));
            if (card[startPos.x, startPos.y] == 0)
                GameOver();
        }
        else {
            SetPosition(new Vector2Int(1, 0));
            if (card[startPos.x, startPos.y] == 0)
                GameOver();
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

    private void GameOver() {
        Debug.Log("You Lose!!!");
        isActive = false;
        StopAllCoroutines();
        AtStart();        
    }

    private void SetPosition(Vector2Int newPos) {
        Animka(1);
        startPos.x += newPos.y;
        startPos.y += newPos.x;
    }

    private void AtStart() {
        listik = movesf1;
        i = 0;
        movenum = 0;
        winnum = 0;
        forepeat = 0;
    }

    private void NextMap() {   
        isActive = false;
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
                var name = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (quene[movenum, 1] == card[startPos.x, startPos.y] || quene[movenum, 1] == 0 || quene[movenum, 0] == 4) {
                    name = IntToMove(quene[movenum, 0], false);
                    if (name == "Left" || name == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref quene);
                movenum += 1;
                if (name != "Other" && name != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    if (name == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (name == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (name == "Scratch") {
                    yield return null;
                }
                if (name == "Other") {
                    yield return null;
                }
            }
            else {
                yield return new WaitForSeconds(0.5f);
                i = 0;
                try{
                    loader.MapNext(currentMap);                   
                }
                catch(FileNotFoundException e) {
                    Debug.Log(e);
                }
                Level();
                yield break;
            }
            if (switchLst) {
                switchLst = false;
                yield break;
            }
        }
    }

    private IEnumerator MovesHandlerF1() {
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i < movesf1.GetLength(0); i++) {
            Debug.Log(i + " - " + movenum);
            if (isActive) {
                var name = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf1[movenum, 1] == card[startPos.x, startPos.y] || movesf1[movenum, 1] == 0 || movesf1[movenum, 0] == 4) {
                    name = IntToMove(movesf1[movenum, 0], false);
                    if (name == "Left" || name == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf1);
                movenum += 1;
                if (name != "Other" && name != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    if (name == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (name == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (name == "Scratch") {
                    yield return null;
                }
                if (name == "Other") {
                    yield return null;
                }
            }
            else {
                yield return new WaitForSeconds(0.5f);
                i = 0;
                try{
                    loader.MapNext(currentMap);                   
                }
                catch(FileNotFoundException e) {
                    Debug.Log(e);
                }
                Level();
                yield break;
            }
            if (switchLst) {
                switchLst = false;
                yield break;
            }    
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
    }

    private IEnumerator MovesHandlerF2() {
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i < movesf2.GetLength(0); i++) {
            if (isActive) {
                var name = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf2[movenum, 1] == card[startPos.x, startPos.y] || movesf2[movenum, 1] == 0 || movesf2[movenum, 0] == 4) {
                    name = IntToMove(movesf2[movenum, 0], false);
                    if (name == "Left" || name == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf2);
                movenum += 1;
                if (name != "Other" && name != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    if (name == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (name == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (name == "Scratch") {
                    yield return null;
                }
                if (name == "Other") {
                    yield return null;
                }
            }
            else {
                yield return new WaitForSeconds(0.5f);
                i = 0;
                try{
                    loader.MapNext(currentMap);                   
                }
                catch(FileNotFoundException e) {
                    Debug.Log(e);
                }
                Level();
                yield break;
            }
            if (switchLst) {
                switchLst = false;
                yield break;
            }    
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
    }

    private IEnumerator MovesHandlerF3() {
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i < movesf3.GetLength(0); i++) {
            if (isActive) {
                var name = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf3[movenum, 1] == card[startPos.x, startPos.y] || movesf3[movenum, 1] == 0 || movesf3[movenum, 0] == 4) {
                    name = IntToMove(movesf3[movenum, 0], false);
                    if (name == "Left" || name == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf3);
                movenum += 1;
                if (name != "Other" && name != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    if (name == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (name == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (name == "Scratch") {
                    yield return null;
                }
                if (name == "Other") {
                    yield return null;
                }
            }
            else {
                yield return new WaitForSeconds(0.5f);
                i = 0;
                try{
                    loader.MapNext(currentMap);                   
                }
                catch(FileNotFoundException e) {
                    Debug.Log(e);
                }
                Level();
                yield break;
            }
            if (switchLst) {
                switchLst = false;
                yield break;
            }    
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
    }

    private IEnumerator MovesHandlerF4() {
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i < movesf4.GetLength(0); i++) {
            if (isActive) {
                var name = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf4[movenum, 1] == card[startPos.x, startPos.y] || movesf4[movenum, 1] == 0 || movesf4[movenum, 0] == 4) {
                    name = IntToMove(movesf4[movenum, 0], false);
                    if (name == "Left" || name == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf4);
                movenum += 1;
                if (name != "Other" && name != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    if (name == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (name == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (name == "Scratch") {
                    yield return null;
                }
                if (name == "Other") {
                    yield return null;
                }
            }
            else {
                yield return new WaitForSeconds(0.5f);
                i = 0;
                try{
                    loader.MapNext(currentMap);                   
                }
                catch(FileNotFoundException e) {
                    Debug.Log(e);
                }
                Level();
                yield break;
            }
            if (switchLst) {
                switchLst = false;
                yield break;
            }    
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
    }

    private IEnumerator MovesHandlerF5() {
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i < movesf5.GetLength(0); i++) {
            if (isActive) {
                var name = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf5[movenum, 1] == card[startPos.x, startPos.y] || movesf5[movenum, 1] == 0 || movesf5[movenum, 0] == 4) {
                    name = IntToMove(movesf5[movenum, 0], false);
                    if (name == "Left" || name == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf5);
                movenum += 1;
                if (name != "Other" && name != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    if (name == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (name == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (name == "Scratch") {
                    yield return null;
                }
                if (name == "Other") {
                    yield return null;
                }
            }
            else {
                yield return new WaitForSeconds(0.5f);
                i = 0;
                try{
                    loader.MapNext(currentMap);                   
                }
                catch(FileNotFoundException e) {
                    Debug.Log(e);
                }
                Level();
                yield break;
            }
            if (switchLst) {
                switchLst = false;
                yield break;
            }    
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
    }

    private IEnumerator MovesHandlerF6() {
        yield return new WaitWhile(() => switchLst);
        for (i = 0; i < movesf6.GetLength(0); i++) {
            if (isActive) {
                var name = "Other";
                Vector3 currentPosRun = anim.transform.position;
                if (movesf6[movenum, 1] == card[startPos.x, startPos.y] || movesf6[movenum, 1] == 0 || movesf6[movenum, 0] == 4) {
                    name = IntToMove(movesf6[movenum, 0], false);
                    if (name == "Left" || name == "Right") 
                        currentPosLR = Quaternion.Euler(anim.transform.rotation.eulerAngles);
                    else {
                        var newpos = LeftRight(currentDirection);
                        positionToRun = new Vector3(currentPosRun.x + newpos.x, currentPosRun.y, currentPosRun.z + newpos.y);
                    }
                } 
                CheckMoveType(ref movesf6);
                movenum += 1;
                if (name != "Other" && name != "Scratch") {
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    if (name == "Left") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, -90, 0);
                    else if (name == "Right") 
                        anim.transform.rotation = currentPosLR * Quaternion.Euler(0, 90, 0);
                    else
                        anim.transform.position = Vector3.MoveTowards(currentPosRun, positionToRun, 2);
                }
                if (name == "Scratch") {
                    yield return null;
                }
                if (name == "Other") {
                    yield return null;
                }
            }
            else {
                yield return new WaitForSeconds(0.5f);
                i = 0;
                try{
                    loader.MapNext(currentMap);                   
                }
                catch(FileNotFoundException e) {
                    Debug.Log(e);
                }
                Level();
                yield break;
            }
            if (switchLst) {
                switchLst = false;
                yield break;
            }    
        }
        if (quene.GetLength(0) >= 1) {
            movenum = 0;
            StartCoroutine(MovesHandlerQuene());
            yield break;
        }
    }
}