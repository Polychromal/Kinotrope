using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class GameController : MonoBehaviour
{
    public PlayerController playerController;
    public Energy energy;
    public DeathCamera deathCamera;

    public float playerSpeed;
    public float playerSpeedDecimal;
    private float maxMagnitude = 450f;
    public TimeSpan currentLapTime = new TimeSpan();

    public AudioSource musicAudioSource;
    public AudioSource playerAudioSource;
    public AudioClip music;

    public bool playerLocked { get; private set; } = true;
    public bool raceHasStarted { get; private set; } = false;
    public bool playerDead = false;


    public int lapCounter
    {
        get
        {
            return lapTimes.Count + 1;
        }

    }

    public TimeSpan fastestLap
    {
        get
        {
            return lapTimes.Max<TimeSpan>();
        }
    }

    public void StartRace()
    {
        musicAudioSource.Play();    
        raceHasStarted = true;
        playerLocked = false;
    }

    public List<TimeSpan> lapTimes = new List<TimeSpan>();

    public void UpdatePlayerSpeed(float playerVelocityMagnitude)
    {
        
        playerSpeedDecimal = playerVelocityMagnitude / maxMagnitude;
        playerSpeedDecimal = Mathf.Clamp(playerSpeedDecimal, 0, 1);
        playerSpeed = Mathf.Round(playerVelocityMagnitude);
    }

    public void FinishedLap(TimeSpan lapTime)
    {
        lapTimes.Add(lapTime);
    }

    public void PlayerDeath()
    {

        playerDead = true;
        playerLocked = true;
        playerController.trailRenderer.enabled = false;
        playerController.ShipExplosion();
        deathCamera.DeathCamActive = true;
        energy.healthLow = false;
        

       
    }
    private void Update()
    {
        float targetPitch = 0f;

        if (playerDead == true)
        {
            musicAudioSource.pitch = Mathf.Lerp(musicAudioSource.pitch, targetPitch, 0.3f * Time.deltaTime);
            playerAudioSource.pitch = Mathf.Lerp(playerAudioSource.pitch, targetPitch, 0.3f * Time.deltaTime);
        }
    }
}
