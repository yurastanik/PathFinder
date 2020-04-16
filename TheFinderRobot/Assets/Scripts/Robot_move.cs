using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using UnityEngine;

public class Robot_move : MonoBehaviour {

    private int[,] card;
    private int[,] moves;
    private int[,] movesafter = new int[100 , 2];
    private int[,] targets;

    private Animator anim;
    private Transform directionArrow;
    private Vector2Int startPos;
    private string currentDirection;
    private MapLoader loader;
    private Camera camer;
 
    bool repeat = false;
    bool isActive = false;
    int i = 0;
    int movenum = 0;
    int winnum = 0;
    int currentMap = 1;
    int forepeat = 0;
    float timee = 0;

    private int[,] listik;

    private void LoadMap() {
        loader = GameObject.Find("Map").GetComponent<MapLoader>();
        Map loadedMap = loader.GetMap();
        card = MapLoader.OneDToTwoDArray(loadedMap.map, loadedMap.mapWidth);
        moves = MapLoader.OneDToTwoDArray(loadedMap.moves, 2);
        targets = MapLoader.OneDToTwoDArray(loadedMap.targets, 2);
        startPos = loadedMap.startPos;
        currentDirection = loadedMap.direction;
    }

    private void Level() {
        LoadMap();
        AtStart();
        camer = GameObject.Find("Main Camera").GetComponent<Camera>();
        camer.transform.position = new Vector3(startPos.y * 2, card.GetUpperBound(0)-1, -15);
        listik = moves;    
        transform.position = new Vector3(startPos.y * 2, 0, startPos.x * -2);
        directionArrow = GameObject.Find("RotationPivot").transform;
        DirectAtStart();
    }

    private void Start() {
        anim = GetComponent<Animator>();
        Level();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Alpha1))
            anim.speed = 1;
        if (Input.GetKeyUp(KeyCode.Alpha2))
            anim.speed = 2;        
        if (Input.GetKeyUp(KeyCode.Alpha3)) 
            anim.speed = 3;        
        if (Input.GetKeyUp(KeyCode.S) && !isActive) {
            isActive = true;
            StopCoroutine(MovesHandler());
            StartCoroutine(MovesHandler());
        }
    }

    private void DirectAtStart() {
        if (currentDirection == "up") {
            anim.transform.rotation = Quaternion.Euler(0, 180, 0);
            directionArrow.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (currentDirection == "right") {
            anim.transform.rotation = Quaternion.Euler(0, -90, 0);
            directionArrow.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (currentDirection == "left") {
            anim.transform.rotation = Quaternion.Euler(0, 90, 0);
            directionArrow.rotation = Quaternion.Euler(0, 90, 0);
        }
        else {
            anim.transform.rotation = Quaternion.Euler(0, 0, 0);
            directionArrow.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    private void CheckMoveType(int[,] movelist) {
        if (movelist[movenum, 0] == 1) {
            if (movelist[movenum, 1] == 0) {
                FaceDirection(currentDirection);
                CheckTarget();
            }
            else if (movelist[movenum, 1] == card[startPos.x, startPos.y]) {
                FaceDirection(currentDirection);
                CheckTarget();
            }
        }
        else if (movelist[movenum, 0] == 2) {
            if (movelist[movenum, 1] == 0) {
                Debug.Log(card[startPos.x, startPos.y] + " tyt 0");
                ChangeDirection(currentDirection, 2);
            }
            else if (movelist[movenum, 1] == card[startPos.x, startPos.y]) {
                Debug.Log(card[startPos.x, startPos.y]);
                ChangeDirection(currentDirection, 2);
            } 
        } 
        else if (movelist[movenum, 0] == 3) {
            if (movelist[movenum, 1] == 0) {
                ChangeDirection(currentDirection, 3);
            }
            else if (movelist[movenum, 1] == card[startPos.x, startPos.y]) {
                ChangeDirection(currentDirection, 3);                
            }
        }
        else if (movelist[movenum, 0] == 4){
            if (movelist[movenum, 1] != card[startPos.x, startPos.y]) {
                card[startPos.x, startPos.y] = movelist[movenum, 1];
                loader.OnMapUpdate(card);
            }
        }
        else if (movelist[movenum, 0] == 5) {
            if (movelist[movenum, 1] == 0){
                i = -1;
                movenum = -1;
                // repeat = true;
                // isActive = false;
                // StopCoroutine(MovesHandler());
                // StartCoroutine(MovesHandler());
            }
            else if (movelist[movenum, 1] == card[startPos.x, startPos.y]) {
                if (movenum == movelist.GetUpperBound(0)) {
                    i = -1;
                    movenum = -1;
                    // repeat = true;
                    // isActive = false;
                    // StopCoroutine(MovesHandler());
                    // StartCoroutine(MovesHandler());
                }
                else {
                    // repeat = true;
                    // isActive = false;
                    int integer = moves.GetUpperBound(0) - movenum;                    
                    for (int j = 1; j <= integer; j++) {
                        movesafter[forepeat, 0] = moves[movenum+j, 0];
                        movesafter[forepeat, 1] = moves[movenum+j, 1];
                        forepeat += 1;                    
                    }
                    i = -1;
                    movenum = -1;
                    // StopCoroutine(MovesHandler());
                    // StartCoroutine(MovesHandler());
                }
            }
            else if (movesafter.Length > 1) {
                // repeat = true;
                // isActive = false;
                int integer = moves.GetUpperBound(0) - movenum;
                for (int j = 1; j <= integer; j++) {
                        movesafter[forepeat, 0] = moves[movenum+j, 0];
                        movesafter[forepeat, 1] = moves[movenum+j, 1];
                        forepeat += 1;
                }
                listik = movesafter;
                i = -1;
                movenum = -1;
                // StopCoroutine(MovesHandler());
                // StartCoroutine(MovesHandler());
            }
        }
    }

    private void ChangeDirection(string direction, int move) {
        Animka(move);
        if (move == 2) {
            directionArrow.rotation *= Quaternion.Euler(0, 90, 0);            
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
            directionArrow.rotation *= Quaternion.Euler(0, -90, 0);
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
        movesafter = new int[100, 2];
        repeat = false;
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

    // private float SpeedOfAnimation(int movenumber) {
    //     var name = IntToMove(movenumber);
    //     if (name == "Run") {
    //         return 1/(anim.speed)+0.05f;
    //     }
    //     else if (name == "Right" || name == "Left") {
    //     }
    //     else if (name == "Scratch") {
    //         return 1f;
    //     }
    //     return 0f;
    // }



 
    private IEnumerator MovesHandler() {
        for (i = 0; i <= listik.GetUpperBound(0); i++) {            
            if (isActive) {
                Debug.Log("s");
                var name = "Other";
                if (listik[movenum, 1] == card[startPos.x, startPos.y] || listik[movenum, 1] == 0 || listik[movenum, 0] == 4)
                    name = IntToMove(listik[movenum, 0], false);
                CheckMoveType(listik);
                movenum += 1;
                if (name != "Other" || name != "Scratch") {
                    Debug.Log(name);
                    yield return new WaitForSeconds(0.12f);
                    yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName(name));
                    yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
                }
                else if (name == "Scratch") {
                    if (listik[movenum-1, 1] == card[startPos.x, startPos.y])
                        yield return null;
                    yield return new WaitForSeconds(0.15f);
                }
                else 
                    yield return new WaitForSeconds(0);;
            }
            // else if (!isActive && repeat) {
            //     i = -1;
            //     movenum = -1;
            //     isActive = true;
            //     repeat = false;
            //     yield break;
            // }
            else {
                yield return new WaitForSeconds(1.1f);
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
        }
    }    
}
