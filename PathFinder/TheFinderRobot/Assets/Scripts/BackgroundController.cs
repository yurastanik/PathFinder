using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour {

    [SerializeField]
    private GameObject[] prefabs;
    [SerializeField]
    private uint maxObjects;

    private List<BackgroundObject> onSceneObjects;

    void Start() {
        onSceneObjects = new List<BackgroundObject>();
    }

    void Update() {
        if (onSceneObjects.Count < maxObjects) {
            // if (Random.Range(1, 2) == 1) {
                CreateNewObject();
            // }
        }
        for (int i = 0; i < onSceneObjects.Count; i++) {
            if (onSceneObjects[i].IsReady() == true) {
                GameObject.Destroy(onSceneObjects[i].gameObject);
                onSceneObjects.Remove(onSceneObjects[i]);
                i--;
            }
        }
    }

    void CreateNewObject() {
        GameObject newObj = GameObject.Instantiate(
            prefabs[Random.Range(0, prefabs.Length)],
            transform
        );

        //                              \/ newOBbj
        BackgroundObject objClassRef = newObj.GetComponent<BackgroundObject>();

        int xOffset = Random.Range(-50, 50);
        int y = Random.Range(-500, 500);
        float scale = Random.Range(5f, 20f);

        newObj.transform.localScale = new Vector3(scale, scale, 1);

        if (Random.Range(0, 2) == 0) { // LEFT SIDE
            newObj.transform.localPosition = new Vector3(-900 + xOffset, y, newObj.transform.position.z);
            objClassRef.SetupDestination(750, true);
        } else { // RIGHT SIDE
            newObj.transform.localPosition = new Vector3(900 + xOffset, y, newObj.transform.position.z);
            objClassRef.SetupDestination(-750, false);
        }

        onSceneObjects.Add(objClassRef);
    }

}
