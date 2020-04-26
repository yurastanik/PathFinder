using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireframeRotation : MonoBehaviour {

    float HSV = 0f;

    private void Update() {
        transform.Rotate(new Vector3(-0.5f, 0, 0));
        transform.Rotate(new Vector3(0, 1f, 0));

        GetComponentsInChildren<MeshRenderer>()[1].material.color
            = Color.HSVToRGB(HSV, 1, 1);

        HSV += 0.001f;
        if (HSV > 1)
            HSV = 0;
    }

}
