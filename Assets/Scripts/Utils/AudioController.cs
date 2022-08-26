using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController audioInstance;
    bool muted = false;

    public Sound[] sounds;

    public string currentAwakeSong;

    private void Awake() {
        if (audioInstance == null) {
            audioInstance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = s.playOnAwake;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

        }
    }

    private void Start() {
        Play(currentAwakeSong);
    }

    public void ChangeCurrentAwakeSong(string nameSong) {
        Stop(currentAwakeSong);
        currentAwakeSong = nameSong;
        Play(currentAwakeSong);
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        //print(s.name);
        if(s==null) {
            Debug.LogError($"audio clip with this {name} dont exist");
            return;
        }
        s.source.Play();
    }

    private void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogError($"audio clip with this {name} dont exist");
            return;
        }
        s.source.Stop();
    }
    public void SetGlobalVolume() {
        print(AudioListener.volume);
        if (muted) {
            AudioListener.volume = 1;
            muted = false;
        } else {
            AudioListener.volume = 0;
            muted = true;
        }
    }
}
