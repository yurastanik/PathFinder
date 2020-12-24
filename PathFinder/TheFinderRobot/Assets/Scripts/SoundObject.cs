using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour {

    private AudioSource source = null;

    private bool isActive = false;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    private void Update() {
        if (isActive) {
            if (!source.isPlaying)
                Destroy(gameObject);
        }
    }

    public void Play(ref AudioClip clip, float volume) {
        source.clip = clip;
        source.volume = volume;
        source.Play();
        isActive = true;
    }

}
