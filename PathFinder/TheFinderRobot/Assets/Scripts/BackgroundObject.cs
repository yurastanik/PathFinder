using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObject : MonoBehaviour {

    private bool readyToDie = false;
    private float speed = 0.1f;

    private int destination;
    private bool rightDir;

    public void SetupDestination(int destination, bool rightDir) {
        this.destination = destination;
        this.rightDir = rightDir;
        speed = Random.Range(0.5f, 1.5f);
        StartCoroutine(StepTimer());
    }

    private IEnumerator StepTimer() {
        while (true) {
            if (rightDir) {
                Debug.Log(transform.localPosition.x + " < " + destination);
                if (transform.localPosition.x < destination) {
                    transform.localPosition = new Vector3(
                        transform.localPosition.x + speed,
                        transform.localPosition.y,
                        transform.localPosition.z
                    );
                }
                else
                    break;
            } else {
                if (transform.localPosition.x > destination) {
                    transform.localPosition = new Vector3(
                        transform.localPosition.x - speed,
                        transform.localPosition.y,
                        transform.localPosition.z
                    );
                }
                else
                    break;
            }

            yield return new WaitForSeconds(0.01f);
        }

        readyToDie = true;
    }

    public bool IsReady() { return readyToDie; }

}
