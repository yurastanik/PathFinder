using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;

public class Robot_move : MonoBehaviour {

    private int[,] card;
    private int[,] moves;
    private int[,] targets;

    private Animator anim;
    private Transform directionArrow;
    private Vector2Int startPos;
    private string currentDirection;
    private MapLoader loader;

    bool repeat = false;
    bool isActive = false;
    int i = 0;
    int movenum = 0;
    int winnum = 0;
    int currentMap = 1;

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
        transform.position = new Vector3(startPos.y * 2, 0, startPos.x * -2);
        directionArrow = GameObject.Find("RotationPivot").transform;
        DirectAtStart();
    }

    private void Start() {
        anim = GetComponent<Animator>();
        Level();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.S) && !isActive) {
            isActive = true;
            StartCoroutine(MovesHandler());
        }
    }

    private void DirectAtStart() {
        if (currentDirection == "up") 
            directionArrow.rotation = Quaternion.Euler(0, 180, 0);
        else if (currentDirection == "right")
            directionArrow.rotation = Quaternion.Euler(0, -90, 0);
        else if (currentDirection == "left")
            directionArrow.rotation = Quaternion.Euler(0, 90, 0);
        else 
            directionArrow.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void CheckMoveType() {
        if (moves[movenum, 0] == 1) {
            if (moves[movenum, 1] == 0) {
                FaceDirection(currentDirection);
                CheckTarget();
            }
            else if (moves[movenum, 1] == card[startPos.x, startPos.y]) {
                FaceDirection(currentDirection);
                CheckTarget();
            }
        }
        else if (moves[movenum, 0] == 2) {
            if (moves[movenum, 1] == 0) {
                ChangeDirection(currentDirection, 2);
            }
            else if (moves[movenum, 1] == card[startPos.x, startPos.y]) {
                ChangeDirection(currentDirection, 2);
            }
        }
        else if (moves[movenum, 0] == 3) {
            if (moves[movenum, 1] == 0) {
                ChangeDirection(currentDirection, 3);
            }
            else if (moves[movenum, 1] == card[startPos.x, startPos.y]) {
                ChangeDirection(currentDirection, 3);
            }
        }
        else if (moves[movenum, 0] == 4){
            card[startPos.x, startPos.y] = moves[movenum, 1];
            loader.OnMapUpdate(card);
        }
        else if (moves[movenum, 0] == 5) {
            if (moves[movenum, 1] == 0){
                repeat = true;
                isActive = false;
                StartCoroutine(MovesHandler());
            }
            else if (moves[movenum, 1] == card[startPos.x, startPos.y]) {
                repeat = true;
                isActive = false;
                StartCoroutine(MovesHandler());
            }
        }
    }

    private void ChangeDirection(string direction, int move) {
        if (move == 2) {
            directionArrow.rotation *= Quaternion.Euler(0, 90, 0);
            anim.Play("Right", 0);
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
        if (move == 3) {
            directionArrow.rotation *= Quaternion.Euler(0, -90, 0);
            anim.Play("Left", 0);
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
        if (direction == "up") {
            SetPosition(new Vector2Int(0, -1));
            if (card[startPos.x, startPos.y] == 0)
                GameOver();
        }
        if (direction == "left") {
            SetPosition(new Vector2Int(-1, 0));
            if (card[startPos.x, startPos.y] == 0)
                GameOver();
        }
        if (direction == "right") {
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
            Debug.Log("You WIN!!!");
            winnum = 0;
            currentMap += 1;
            isActive = false;
            loader.MapNext(currentMap);
            Level();
        }
        else
            winnum = 0;
    }

    private void GameOver() {
        Debug.Log("Game Over!");
        isActive = false;
    }

    private void SetPosition(Vector2Int newPos) {
        // transform.Translate(newPos.x * 2, 0, -(newPos.y * 2));
        anim.Play("Run", 0);
        startPos.x += newPos.y;
        startPos.y += newPos.x;
    }
 
    private IEnumerator MovesHandler() {
        for (i = 0; i <= moves.GetUpperBound(0); i++) {
            // if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stay")) {
                if (isActive) {
                    CheckMoveType();
                    movenum += 1;
                }
                else if (!isActive && repeat) {
                    i = -1;
                    movenum = -1;
                    isActive = true;
                    repeat = false;
                    yield return new WaitForSeconds(1f);
                    yield break;
                }
                else {
                    i = 0;
                    movenum = 0;
                    yield break;
                }
                yield return new WaitForSeconds(1f);
            // }
       }
    }
}
