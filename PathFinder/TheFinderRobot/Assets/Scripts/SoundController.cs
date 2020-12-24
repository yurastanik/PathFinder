using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

    [SerializeField] private GameObject soundObject = null;

    [SerializeField] private AudioClip click = null;
    [SerializeField] private AudioClip accept = null;
    [SerializeField] private AudioClip discard = null;
    [SerializeField] private AudioClip onPlayerClick = null;
    [SerializeField] private AudioClip levelPage = null;
    [SerializeField] private AudioClip jump1 = null;
    [SerializeField] private AudioClip jump2 = null;
    [SerializeField] private AudioClip jump3 = null;
    [SerializeField] private AudioClip jump4 = null;
    [SerializeField] private AudioClip hint = null;

    private float volume = 0.75f;

    public void SetVolume(float volume) {
        this.volume = volume;
    }

    public void PlayClick() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Click";
        obj.GetComponent<SoundObject>().Play(ref click, volume);
    }

    public void PlayAccept() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Accept";
        obj.GetComponent<SoundObject>().Play(ref accept, volume);
    }

    public void PlayDiscard() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Discard";
        obj.GetComponent<SoundObject>().Play(ref discard, volume);
    }

    public void PlayPlayerClick() {
        foreach (Transform child in GetComponentsInChildren<Transform>()) {
            if (child.name == "Player Click")
                return;
        }

        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Player Click";
        obj.GetComponent<SoundObject>().Play(ref onPlayerClick, volume);
    }

    public void PlayPage() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Page";
        obj.GetComponent<SoundObject>().Play(ref levelPage, volume);
    }

    public void PlayJump1() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Jump 1";
        obj.GetComponent<SoundObject>().Play(ref jump1, volume);
    }

    public void PlayJump2() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Jump 2";
        obj.GetComponent<SoundObject>().Play(ref jump2, volume);
    }

    public void PlayJump3() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Jump 3";
        obj.GetComponent<SoundObject>().Play(ref jump3, volume);
    }

    public void PlayJump4() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Jump 4";
        obj.GetComponent<SoundObject>().Play(ref jump4, volume);
    }

    public void PlayHint() {
        GameObject obj = GameObject.Instantiate(soundObject, gameObject.transform);

        obj.name = "Hint";
        obj.GetComponent<SoundObject>().Play(ref hint, volume);
    }

}
