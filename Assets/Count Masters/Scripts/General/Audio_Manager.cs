using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum AudioType { Music, Sound }

public class Audio_Manager : MonoBehaviour
{
    public Sound[] sounds;
    public bool soundMute = false, musicMute = false;

    #region singleton
    public static Audio_Manager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        // DontDestroyOnLoad(this);

        SquadDetection.OnRunnersAdded += PlayRunnersAddedSound;
        Runner.OnRunnerDied += PlayRunnerDiedSound;

        ParticleControl.OnCoinsReleasedAction += PlayCoinsReleaseSound;
        ParticleControl.OnCoinAddedAction += PlayCoinSound;
    }

    private void OnDestroy()
    {
        SquadDetection.OnRunnersAdded -= PlayRunnersAddedSound;
        Runner.OnRunnerDied -= PlayRunnerDiedSound;

        ParticleControl.OnCoinsReleasedAction -= PlayCoinsReleaseSound;
        ParticleControl.OnCoinAddedAction -= PlayCoinSound;
    }

    #endregion

    void Start()
    {

        foreach (Sound s in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = s.Clip;
            source.volume = s.volume;
            source.pitch = s.pitch;
            source.loop = s.loop;

            s.Source = source;

            if (s.playOnStart)
                s.Source.Play();
        }
    }

    public void Play(string soundName, bool varyPitch = false)
    {
        
        Sound s = Array.Find(sounds, so => so.name == soundName);

        if (s == null)
        {
            // Debug.LogError("Sound with name " + soundName + " doesn't exist!");
            return;
        }

        if (s.audioType == AudioType.Music && !musicMute)
        {
            s.Source.Play();
        }
        else if (s.audioType == AudioType.Sound && !soundMute)
        {
            if(!s.Source.isPlaying)
            {
                if(varyPitch)
                    s.Source.pitch = UnityEngine.Random.Range(1f, 1.1f);
                s.Source.Play();
            }
        }

    }

    public void Stop(string soundName)
    {

        Sound s = Array.Find(sounds, so => so.name == soundName);

        if (s == null)
        {
            Debug.LogError("Sound with name " + soundName + " doesn't exist!");
            return;
        }

        s.Source.Stop();

    }

    private void PlayRunnersAddedSound(int none)
    {
        if (soundMute)
            return;

        Sound s = Array.Find(sounds, so => so.name == "Runners Added");
        if(s == null)
            return;
        
        s.Source.Play();
    }

    private void PlayRunnerDiedSound(Runner runner)
    {
        PlayRunnerDiedSound();
    }

    private void PlayRunnerDiedSound()
    {
        //if(!soundMute)
        Play("Runner Died", true);
    }

    private void PlayCoinsReleaseSound()
    {
        Play("CoinBag");
    }

    private void PlayCoinSound()
    {
        Play("Coin", true);
    }
}

    [Serializable]
    public class Sound
    {
        public string name;
        public AudioType audioType;
        public AudioClip Clip;


        [HideInInspector]
        public AudioSource Source;

        public float volume = 1;
        public float pitch = 1;

        public bool loop = false;
        public bool playOnStart = false;

    }






