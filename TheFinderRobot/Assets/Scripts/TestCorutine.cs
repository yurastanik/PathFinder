using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCorutine : MonoBehaviour
{
    int[,] card = new int[,] {{ 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 }, {2, 2, 0, 0, 0}, { 1, 2, 3, 0, 0 }, { 0, 0, 0, 0, 0 }};
    int[,] moves = new int[,] {{1, 0}, {2, 2}, {1, 0}, {2, 2}, {1, 0}, {2, 2}, {1, 0}};
    List<int> startPoint = new List<int>() {3, 0};
    bool isUp;
    public GameObject plate;
    string currentPosition = "down";
    // Start is called before the first frame update
    void Start() {
        Moving();
    }

    // Update is called once per frame
    void Update() {  
        
    }

    void Moving() {
        //for (int dimension = 1; dimension <= moves.Rank; dimension++)
        for (int i = 0; i <= moves.GetUpperBound(0); i++) {
            print( "x: " + startPoint[0] + " y: " + startPoint[1]);
            if (moves[i, 0] == 1) {
                if (moves[i, 1] == 0) {
                    FaceDirection(currentPosition);
                }
                else if (moves[i, 1] == card[startPoint[0], startPoint[1]]) {
                    FaceDirection(currentPosition);
                }                
            }
            else if (moves[i, 0] == 2)  {
                if (moves[i, 1] == 0) {
                    ChangePos(currentPosition, 2);
                    }
                else if (moves[i, 1] == card[startPoint[0], startPoint[1]]) {
                    ChangePos(currentPosition, 2);
                } 
                }
            else if (moves[i, 0] == 3)  {
                if (moves[i, 1] == 0) {
                    ChangePos(currentPosition, 3);
                }
                else if (moves[i, 1] == card[startPoint[0], startPoint[1]]) {
                    ChangePos(currentPosition, 3);
                }                              
            }        
        }
    }

    public void ChangePos(string Position, int Move) {
        if (Move == 2) {
            if (Position == "down") {
                currentPosition = "left";
            }
            if (Position == "up") {
                currentPosition = "right";
            }
            if (Position == "left") {
                currentPosition = "up";
            }
            if (Position == "right") {
                currentPosition = "down";
            }
        }
        if (Move == 3) {
            if (Position == "down") {
                currentPosition = "right";
            }
            if (Position == "up") {
                currentPosition = "left";
            }
            if (Position == "left") {
                currentPosition = "down";
            }
            if (Position == "right") {
                currentPosition = "up";
            }
        }
    }

    public void FaceDirection(string Position) {
        if (Position == "down") {
            StartCoroutine(MoveDown());
            if (card[startPoint[0], startPoint[1]] == 0) {
                Debug.Log("Game Over!");
            }
        }
        if (Position == "up") {
            StartCoroutine(MoveUp());
            if (card[startPoint[0], startPoint[1]] == 0) {
                Debug.Log("Game Over!");
            }
        }
        if (Position == "left") {
            StartCoroutine(MoveLeft());         
            if (card[startPoint[0], startPoint[1]] == 0) {
                Debug.Log("Game Over!");
            }
        }
        if (Position == "right") {
            StartCoroutine(MoveRight());
            if (card[startPoint[0], startPoint[1]] == 0) {
                Debug.Log("Game Over!");
            }
        }
    }
    IEnumerator MoveLeft() {
        startPoint[0] -= 1;
        transform.Translate(-2, 0, 0);
        yield return new WaitForSeconds(2f);
    }
    IEnumerator MoveRight() {
        startPoint[0] += 1;
        transform.Translate(2, 0, 0);
        yield return new WaitForSeconds(2f);
    }
    IEnumerator MoveUp() {
        startPoint[1] -= 1;
        transform.Translate(0, 2, 0);
        yield return new WaitForSeconds(2f);
    }
    IEnumerator MoveDown() {
        startPoint[1] += 1;
        transform.Translate(0, -2, 0);
        yield return new WaitForSeconds(2f);
    }
}     
            
