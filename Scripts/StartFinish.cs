using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartFinish : MonoBehaviour
{
    public GameController gameController;
    public PlayerController playerController;
    public Transform player;
    public StartFinish startFinish;
    public TimeSpan currentLapTime;
    public Double lapStart;
    public AudioSource audioSource;
    public AudioClip countDownClip;

    public TMP_Text countdownText;

    public int countDown;
    public bool countDownInProgress = false;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        countDownInProgress = true;
        yield return new WaitForSeconds(3);

        audioSource.PlayOneShot(countDownClip);
        countdownText.text = "3";

        yield return new WaitForSeconds(1);
        countdownText.text = "2";

        yield return new WaitForSeconds(1);
        countdownText.text = "1";

        yield return new WaitForSeconds(1);
        countdownText.text = "GO";
        countDownInProgress = false;
        lapStart = Time.time;
        gameController.StartRace();

        yield return new WaitForSeconds(1);
        countdownText.text = "";



        //while (countDown > 0)
        //{
        //    yield return new WaitForSeconds(1);
        //    countDown--;
        //}

      
    }

    private void Update()
    {
        double currentTime = Time.time;

        if (gameController.raceHasStarted == true)
        {
            currentLapTime = TimeSpan.FromSeconds(lapStart - currentTime);
            gameController.currentLapTime = currentLapTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameController.raceHasStarted == true)
        {
            gameController.FinishedLap(currentLapTime);
            lapStart = Time.time;
        }
    }

    
}