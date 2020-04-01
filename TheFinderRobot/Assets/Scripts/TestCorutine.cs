using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCorutine : MonoBehaviour {

    private void Start() {
        StartCoroutine(Pause());
    }

    private void Move() {
        transform.position += new Vector3(0, 0, 2);
    }

    IEnumerator Pause() {
        while (true) {
            Move();
            yield return new WaitForSeconds(2f);
        }
    }

}
