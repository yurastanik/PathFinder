using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> musicArray = new List<AudioClip>();
    private AudioSource source;

    private void Awake() {
        source = gameObject.GetComponent<AudioSource>();

        if (GameObject.FindGameObjectsWithTag("Music").Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }

    public void ChangeVolume(float volume) {
        source.volume = volume;
    }

    private void Update() {
        if (!source.isPlaying)
            ChangeMusic();
    }

    public void ChangeMusic() {
        source.loop = false;
        source.clip = musicArray[Random.Range(0, musicArray.Count)];
        source.Play();
    }

}
