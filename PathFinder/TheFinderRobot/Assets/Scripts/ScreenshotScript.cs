using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotScript : MonoBehaviour {

#if UNITY_EDITOR
    [SerializeField] private bool hideInterface;
    [SerializeField] private bool hideClouds;
    [SerializeField] private int scale;
    [SerializeField] private float cameraOffset;
    [SerializeField] private float cameraFOVScale;

    [SerializeField] private GameObject[] objectsToHide;
    [SerializeField] private GameObject clouds;

    private Camera cam;

    void Start() {
        cam = Camera.main;
    }

    void FixedUpdate() {
        if (Input.GetKeyDown(KeyCode.F11)) {
            if (cam.orthographic) {
                cam.transform.position += Vector3.up * cameraOffset;
                cam.orthographicSize += cameraFOVScale;
            }
            else
                cam.fieldOfView += cameraFOVScale;

            if (hideClouds) {
                foreach (var cl in clouds.GetComponentsInChildren<SpriteRenderer>())
                    cl.color = new Color(cl.color.r, cl.color.g, cl.color.b, 0f);
            }

            if (hideInterface) {
                foreach (GameObject go in objectsToHide) {
                    CanvasGroup cg = go.AddComponent<CanvasGroup>();
                    cg.alpha = 0f;
                }
            }

            StartCoroutine(Pause());
        }
    }

    private IEnumerator Pause() {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("ScreenShot_" + Time.frameCount + ".png");
        ScreenCapture.CaptureScreenshot(
            "ScreenShot_" + Time.frameCount + ".png",
            scale
        );

        yield return new WaitForSeconds(0.5f);

        if (cam.orthographic) {
            cam.transform.position -= Vector3.up * cameraOffset;
            cam.orthographicSize -= cameraFOVScale;
        }
        else
            cam.fieldOfView -= cameraFOVScale;

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
#endif

}
