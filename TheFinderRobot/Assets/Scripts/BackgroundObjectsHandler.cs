using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundObjectsHandler : MonoBehaviour {

    [SerializeField]
    private GameObject[] prefabs;

    [SerializeField]
    private int maxObjectsOnScene;

    private void Start() {
        for (int i = 0; i < maxObjectsOnScene; i++) {
            int randPrefab = Random.Range(0, prefabs.Length);
            GameObject newObject = GameObject.Instantiate(prefabs[randPrefab]);

            newObject.transform.parent = this.transform;
            SetObjectCorrectStartPosition(newObject);
            StartCoroutine(CheckForLost(newObject));
        }
    }

    private void Update() {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            BackgroundObjectHandler handler = GetHandler(child.gameObject);
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(child.position);

            bool isVisible = screenPoint.z > 0 && screenPoint.x > -0.1f
                && screenPoint.x < 1.1f && screenPoint.y > -0.1f && screenPoint.y < 1.1f;

            bool isNotHidden = handler.GetNotHidden();

            if (isVisible == false && isNotHidden == true)
                RestartObject(child.gameObject);
            else if (isVisible == true && isNotHidden == false)
                handler.SetNotHidden(true);
        }
    }

    private void RestartObject(GameObject objectFromList) {
        BackgroundObjectHandler handler = GetHandler(objectFromList);

        handler.SetRandomParams();
        SetObjectCorrectStartPosition(objectFromList);
        StartCoroutine(CheckForLost(objectFromList));
    }

    private void SetObjectCorrectStartPosition(GameObject newObject) {
        BackgroundObjectHandler handler = GetHandler(newObject);
        Vector3 direction = handler.GetMoveDirection();
        int x, z;

        if (direction.x == 0 &&
            direction.z == 0) {
                handler.SetMoveDirection(new Vector3(2, 0, 1));
        }

        if (direction.x < 0) { // Right
            x = Random.Range(20, 25);

            if (direction.z < 0) // Top
                z = Random.Range(5, 10);
            else if (direction.z > 0) // Down
                z = Random.Range(-35, -30);
            else // Mid
                z = Random.Range(-30, 0);
        } else if (direction.x > 0) { // Left
            x = Random.Range(-10, -5);

            if (direction.z < 0) // Top
                z = Random.Range(5, 10);
            else if (direction.z > 0) // Down
                z = Random.Range(-35, -30);
            else // Mid
                z = Random.Range(-30, 0);
        } else { // Mid
            x = Random.Range(0, 20);

            if (direction.z < 0) // Top
                z = Random.Range(5, 10);
            else // Down
                z = Random.Range(-35, -30);
        }

        newObject.transform.position = new Vector3(x, Random.Range(-10, -5), z);
    }

    private BackgroundObjectHandler GetHandler(GameObject forObject) {
        return forObject.GetComponentInChildren<BackgroundObjectHandler>();
    }

    private IEnumerator CheckForLost(GameObject objectFromList) {
        BackgroundObjectHandler handler = GetHandler(objectFromList);

        yield return new WaitForSeconds(5f);
        if (!handler.GetNotHidden())
            RestartObject(objectFromList);
    }

}
