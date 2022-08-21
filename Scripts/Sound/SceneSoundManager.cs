using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundManager : MonoBehaviour
{
    public AudioSource galaxyAudio;
    public AudioSource fuironAudio;
    public AudioSource kalasskuAudio;
    //public AudioSource ehChedAudio;
    public AudioSource tonDarAudio;
   

    private void Start()
    {
        galaxyAudio.Play();
        fuironAudio.Play();
        kalasskuAudio.Play();
        //ehChedAudio.Play();
        tonDarAudio.Play();
    }

    

}
