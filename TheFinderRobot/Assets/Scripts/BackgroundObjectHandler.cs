using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectHandler : MonoBehaviour {

    private Transform objectModel;

    private Vector3 moveDirection;
    private Quaternion rotation;

    private float maxMovSpd = 2f;
    private float maxRotSpd = 0.5f;

    private bool isNotHidden = false;
    // private bool isVisible = false;

    // VISIBLE FUNCTIONS
    public bool GetNotHidden() {
        return isNotHidden;
    }
    public void SetNotHidden(bool hide) {
        isNotHidden = hide;
    }
    // -----------------

    private void Start() {
        objectModel = GetComponentInChildren<Transform>();
        SetRandomParams();
    }

    public void SetRandomParams() {
        isNotHidden = false;

        moveDirection.x = Random.Range(-maxMovSpd, maxMovSpd);
        moveDirection.z = Random.Range(-maxMovSpd, maxMovSpd);

        Vector3 rand = new Vector3(
            Random.Range(-maxRotSpd, maxRotSpd),
            Random.Range(-maxRotSpd, maxRotSpd),
            Random.Range(-maxRotSpd, maxRotSpd)
        );

        rotation = Quaternion.Euler(rand);
    }

    private void Update() {
        objectModel.rotation *= rotation;
        transform.position += moveDirection * Time.deltaTime;
    }

    public Vector3 GetMoveDirection() {
        return moveDirection;
    }
    public void SetMoveDirection(Vector3 newDirection) {
        moveDirection = newDirection;
    }
}
