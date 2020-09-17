using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayParticle : MonoBehaviour {

    [SerializeField] private GameObject mapObj;
    [SerializeField] private GameObject sprayPrefab;
    [SerializeField] private Robot_move rbMove;

    private void Update() {
        if (rbMove.CheckPosition()) {
            Instantiate(
                sprayPrefab,
                new Vector3(transform.position.x, 0, transform.position.z),
                Quaternion.Euler(0, 0, 0),
                mapObj.transform
            );
        }
        this.enabled = false;
    }

}
