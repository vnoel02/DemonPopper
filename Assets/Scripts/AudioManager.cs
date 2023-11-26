using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // AudioManager Singleton
     private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

     void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    public AudioClip backgroundMusic;
//    private AudioSource audioSource;
    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic();
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    void PlayBackgroundMusic()
    // void PlayBackgroundMusic(AudioClip music)
    {
        // if (source == null) {
        //     source = gameObject.AddComponent<AudioSource>();
        // }
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();

        // source.clip = music;
        // source.loop = true;
        // source.Play();
    }

    // void setVolume(float volume) {
    //     if (source == null) {
    //         source.volume = volume;
    //     }
    // }

   
}
