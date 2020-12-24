using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour {

    private Image img;

    private const float pauseTime = 0.001f;
    private const float alphaDecrement = 0.02f;

    bool inAccess = false;

    private void Awake() {
        img = gameObject.GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut() {
        YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();

        while (img.color.a > 0) {
            img.color = new Color(0, 0, 0, img.color.a - alphaDecrement);

            for (float duration = pauseTime; duration > 0; duration -= Time.fixedDeltaTime)
                yield return waitForFixedUpdate;
        }
        inAccess = true;
    }

    public IEnumerator FadeIn(string sceneName) {
        if (inAccess) {
            YieldInstruction waitForFixedUpdate = new WaitForFixedUpdate();

            img.raycastTarget = true;
            while (img.color.a < 1) {
                img.color = new Color(0, 0, 0, img.color.a + alphaDecrement);

                for (float duration = pauseTime; duration > 0; duration -= Time.fixedDeltaTime)
                    yield return waitForFixedUpdate;
            }
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

}
