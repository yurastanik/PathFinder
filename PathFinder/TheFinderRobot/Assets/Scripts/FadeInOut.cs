using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour {

    private Image img;

    private const float pauseTime = 0.01f;
    private const float alphaDecrement = 0.01f;

    private void Awake() {
        img = gameObject.GetComponent<Image>();
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut() {
        while (img.color.a > 0) {
            img.color = new Color(0, 0, 0, img.color.a - alphaDecrement);
            yield return new WaitForSeconds(pauseTime);
        }
        //StartCoroutine(FadeIn("Game"));
    }

    public IEnumerator FadeIn(string sceneName) {
        img.raycastTarget = true;
        while (img.color.a < 1) {
            img.color = new Color(0, 0, 0, img.color.a + alphaDecrement);
            yield return new WaitForSeconds(pauseTime);
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

}
