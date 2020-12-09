using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipIntro : MonoBehaviour {

    void Start() {
        StartCoroutine(Pause());
    }

    private IEnumerator Pause() {
        yield return new WaitForSeconds(5);
        StartCoroutine(GameObject.FindGameObjectWithTag("FadeInFadeOut").GetComponent<FadeInOut>().FadeIn("Menu"));

    }

}
