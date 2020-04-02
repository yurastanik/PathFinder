﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCorutine : MonoBehaviour {

    private int[,] card = new int[,] {  { 0, 0, 0, 0, 0 },
                                        { 0, 0, 0, 0, 0 },
                                        { 2, 2, 0, 0, 0 },
                                        { 1, 2, 3, 0, 0 },
                                        { 0, 0, 0, 0, 0 } };

    private int[,] moves = new int[,] { {1, 0},
                                        {2, 2},
                                        {1, 0},
                                        {2, 2},
                                        {1, 0},
                                        {2, 2},
                                        {1, 0} };

    // List<int> startPoint = new List<int>() {3, 0};

    private Transform directionArrow;

    private Vector2Int startPos;
    private string currentDirection = "down";

    bool isActive = true;
    int i = 0;

    private void Start() {
        directionArrow = GameObject.Find("RotationPivot").transform;
        startPos = new Vector2Int(3, 0);
        StartCoroutine(MovesHandler());
    }

    private void SetCameraPosition(Vector3Int newPos) {
        Camera mainCamera = Camera.main;
        mainCamera.transform.position += newPos;
    }

    private void CheckMoveType() {
        Debug.Log("i: " + i + "  x: " + startPos.x + "  y: " + startPos.y);
        if (moves[i, 0] == 1) {
            if (moves[i, 1] == 0)
                FaceDirection(currentDirection);
            else if (moves[i, 1] == card[startPos.x, startPos.y]) {
                FaceDirection(currentDirection);
            }
        }
        else if (moves[i, 0] == 2) {
            if (moves[i, 1] == 0)
                ChangeDirection(currentDirection, 2);
            else if (moves[i, 1] == card[startPos.x, startPos.y]) {
                ChangeDirection(currentDirection, 2);
            }
        }
        else if (moves[i, 0] == 3) {
            if (moves[i, 1] == 0)
                ChangeDirection(currentDirection, 3);
            else if (moves[i, 1] == card[startPos.x, startPos.y]) {
                ChangeDirection(currentDirection, 3);
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

    private void GameOver() {
        Debug.Log("Game Over!");
        isActive = false;
    }

    private void SetPosition(Vector2Int newPos) {
        startPos.x += newPos.x;
        startPos.y += newPos.y;
        SetCameraPosition(new Vector3Int(newPos.x * 2, 10, -(newPos.y * 2) - 10));
        transform.Translate(newPos.x * 2, 0, -(newPos.y * 2));
    }

    private IEnumerator MovesHandler() {
        for (i = 0; i <= moves.GetUpperBound(0); i++) {
            if (isActive)
                CheckMoveType();
            yield return new WaitForSeconds(0.5f);
        }
    }

    // private IEnumerator MoveLeft() {
    //     startPos.x -= 1;
    //     transform.Translate(-2, 0, 0);
    //     yield return new WaitForSeconds(2f);
    //     Moving();
    // }

    // private IEnumerator MoveRight() {
    //     startPos.x += 1;
    //     transform.Translate(2, 0, 0);
    //     yield return new WaitForSeconds(2f);
    //     Moving();
    // }

    // private IEnumerator MoveUp() {
    //     startPos.y -= 1;
    //     transform.Translate(0, 0, 2);
    //     yield return new WaitForSeconds(2f);
    //     Moving();
    // }

    // private IEnumerator MoveDown() {
    //     startPos.y += 1;
    //     transform.Translate(0, 0, -2);
    //     yield return new WaitForSeconds(2f);
    //     Moving();
    // }
}
