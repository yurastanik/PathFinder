using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamHack : MonoBehaviour {

    // Button 'I'
    private bool hideInterface = false;

    // Button 'C'
    private bool hideClouds = false;

    // Numpad
    private float cameraOffsetX = 0;
    private float cameraOffsetY = 0;
    private float cameraOffsetZ = 0;
    private Vector3 camPos;

    // Buttons +/-
    private float cameraFOVScale = 1f;
    private float orthoSize;
    private float camFOV;

    [SerializeField]
    private GameObject[] objectsToHide;
    [SerializeField]
    private GameObject clouds;

    // Button F10
    private bool enable = false;

    private Camera cam;

    void Start() {
        cam = Camera.main;

        camPos = cam.transform.position;
        orthoSize = cam.orthographicSize;
        camFOV = cam.fieldOfView;
    }

    void TurnON() {
        if (hideClouds) {
            foreach (var cl in clouds.GetComponentsInChildren<SpriteRenderer>())
                cl.color = new Color(cl.color.r, cl.color.g, cl.color.b, 0f);
        }

        if (hideInterface) {
            foreach (GameObject go in objectsToHide) {
                CanvasGroup cg;

                if (!(go.TryGetComponent<CanvasGroup>(out cg)))
                    cg = go.AddComponent<CanvasGroup>();
                cg.alpha = 0f;
            }
        }
    }

    void TurnOFF() {
        cam.transform.position = camPos;
        cam.orthographicSize = orthoSize;
        cam.fieldOfView = camFOV;

        if (hideClouds) {
            foreach (var cl in clouds.GetComponentsInChildren<SpriteRenderer>())
                cl.color = new Color(cl.color.r, cl.color.g, cl.color.b, 1f);
        }

        if (hideInterface) {
            foreach (GameObject go in objectsToHide) {
                Destroy(go.GetComponents<CanvasGroup>()[
                    go.GetComponents<CanvasGroup>().Length - 1
                ]);
            }
        }
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.F9)) {
            if (enable) {
                enable = false;
                TurnOFF();
                Debug.Log("CamHack is disabled!");
            }
            else {
                enable = true;
                Debug.Log("CamHack is enabled!");
            }
        }

        if (enable) {
            // CAMERA
            cam.transform.position = camPos + new Vector3(cameraOffsetX, cameraOffsetY, cameraOffsetZ);
            cam.orthographicSize = orthoSize + cameraFOVScale;
            cam.fieldOfView = camFOV + cameraFOVScale;

            if (Input.GetKeyDown(KeyCode.I))
                hideInterface = hideInterface ? false : true;

            if (Input.GetKeyDown(KeyCode.C))
                hideClouds = hideClouds ? false : true;

            if (Input.GetKeyDown(KeyCode.Keypad8))
                cameraOffsetY += 0.1f;
            if (Input.GetKeyDown(KeyCode.Keypad2))
                cameraOffsetY -= 0.1f;
            if (Input.GetKeyDown(KeyCode.Keypad4))
                cameraOffsetX -= 0.1f;
            if (Input.GetKeyDown(KeyCode.Keypad6))
                cameraOffsetX += 0.1f;
            if (Input.GetKeyDown(KeyCode.Keypad9))
                cameraOffsetZ += 0.1f;
            if (Input.GetKeyDown(KeyCode.Keypad3))
                cameraOffsetZ -= 0.1f;

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
                cameraFOVScale += 0.1f;
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
                cameraFOVScale -= 0.1f;

            TurnON();
        }
    }

}
