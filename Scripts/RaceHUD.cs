using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceHUD : MonoBehaviour
{
    public GameController gameController;
    public TMP_Text currentTime;
    public TMP_Text currentSpeed;
    public TMP_Text fastestLap;
    public TMP_Text lapCounter;

    public TMP_Text[] lapTrackerSlots;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        int lapsCompleted = gameController.lapTimes.Count;

        currentTime.text = string.Format(@"{0:mm\:ss\:ff}", gameController.currentLapTime);
        currentSpeed.text = string.Format("{0:0}", gameController.playerSpeed * 2.237);
        lapCounter.text = string.Format(@"{0:0}", gameController.lapCounter);

        if (lapsCompleted > 0)
        {
            fastestLap.text = string.Format(@"{0:mm\:ss\:ff}", gameController.fastestLap);
        }

        for (int loopCounter = 0; loopCounter < lapTrackerSlots.Length; loopCounter++)
        {
            int lapTimeIndex = lapsCompleted - 1 - loopCounter;
            if (lapTimeIndex >= 0 && lapsCompleted > 0)
            {
                lapTrackerSlots[loopCounter].text = string.Format(@"{0:mm\:ss\:ff}", gameController.lapTimes[lapTimeIndex]);
            }
        }   
    }
}
