using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour {

    private MeshRenderer meshRenderer;

    private float speed = 0.005f;
    private float HSV;

    private void Start() {
        int x = (int)transform.position.x;
        meshRenderer = GetComponent<MeshRenderer>();
        HSV = (x / 2) * 0.1f;
    }

    private void Update() {
        meshRenderer.material.color = Color.HSVToRGB(HSV, 1, 1);
        HSV -= speed;
        if (HSV < 0)
            HSV = 1;
    }
}
