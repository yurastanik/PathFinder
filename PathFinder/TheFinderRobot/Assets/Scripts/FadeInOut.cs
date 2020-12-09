using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour {

    private Image img;

    private const float pauseTime = 0.01f;
    private const float alphaDecrement = 0.01f;

    bool inAccess = false;

    private void Awake() {
        img = gameObject.GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut() {
        while (img.color.a > 0) {
            img.color = new Color(0, 0, 0, img.color.a - alphaDecrement);
            yield return new WaitForSeconds(pauseTime * Time.deltaTime);
        }
        inAccess = true;
    }

    public IEnumerator FadeIn(string sceneName) {
        if (inAccess) {
            img.raycastTarget = true;
            while (img.color.a < 1) {
                img.color = new Color(0, 0, 0, img.color.a + alphaDecrement);
                yield return new WaitForSeconds(pauseTime * Time.deltaTime);
            }
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

}
