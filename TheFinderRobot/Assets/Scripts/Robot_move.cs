using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Robot_move : MonoBehaviour {
                                    //  { {0, 0, 0, 0, 0, 0, 0, 0},
                                    //    {0, 0, 0, 0, 0, 0, 2, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 2, 3, 3, 3, 3, 1, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 0, 0, 0, 0, 0, 3, 0},
                                    //    {0, 0, 0, 0, 0, 0, 2, 0},
                                    //    {0, 0, 0, 0, 0, 0, 0, 0} };

    //private List <Input_Class> input_arr;

    private int[,] card;
    private int[,] moves;
    private int[,] targets;

    // List<int> startPoint = new List<int>() {3, 0};

    private Transform directionArrow;
    private Vector2Int startPos;
    private string currentDirection = "down";

    bool isActive = true;
    int i = 0;
    int movenum = 0;
    int winnum = 0;

    private void LoadMap() {
        MapLoader loader = GameObject.Find("Map").GetComponent<MapLoader>();
        Map loadedMap = loader.GetMap();

        card = MapLoader.OneDToTwoDArray(loadedMap.map, loadedMap.mapWidth);
        moves = MapLoader.OneDToTwoDArray(loadedMap.moves, 2);
        targets = MapLoader.OneDToTwoDArray(loadedMap.targets, 2);
        startPos = loadedMap.startPos;
        currentDirection = loadedMap.direction;
    }

    private void Start() {
        LoadMap();
        // input_arr = starting.GetComponent<Button_play>().input_arr;
        // Debug.Log(input_arr);
        transform.position = new Vector3Int(startPos.y *2, 0, startPos.x * -2);
        directionArrow = GameObject.Find("RotationPivot").transform;
        StartCoroutine(MovesHandler());
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
        }
        else if (moves[movenum, 0] == 5) {
            if (moves[movenum, 1] == 0){
                isActive = false;                
                StartCoroutine(MovesHandler());
            }
            else if (moves[movenum, 1] == card[startPos.x, startPos.y]) {
                isActive = false;                
                StartCoroutine(MovesHandler());
            }
        }
    }

    private void ChangeDirection(string direction, int move) {
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
        if (move == 3) {
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
                Debug.Log("Picked star " + target);
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
            isActive = false;
        }
        else
            winnum = 0;
    }

    private void GameOver() {
        Debug.Log("Game Over!");
        Debug.Log(startPos.x + " " + startPos.y);
        isActive = false;
    }

    private void SetPosition(Vector2Int newPos) {
        startPos.x += newPos.y;
        startPos.y += newPos.x;
        transform.Translate(newPos.x * 2, 0, -(newPos.y * 2));
    }

    private IEnumerator MovesHandler() {
        for (i = 0; i <= moves.GetUpperBound(0); i++) {
            if (isActive) {
                CheckMoveType();
                movenum += 1;
                yield return new WaitForSeconds(0.2f);
            }
            else {
                i = -1;
                movenum = -1; 
                isActive = true;
                yield break;
            }
       }
    }
}
