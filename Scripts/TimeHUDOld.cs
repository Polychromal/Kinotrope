using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHUD : MonoBehaviour
{

    public StartFinish startFinish;
    public GameController gameController;
    public string currentLapTime;
    public int trackTargetLaps;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void OnGUI()
    {

        GUI.skin.label.fontSize = 100;

        GUI.Label(new Rect(Screen.width / 2 - 300, 90, 800, 200), currentLapTime);

        GUI.skin.label.fontSize = 80;

        if (trackTargetLaps > 0)
        {
            GUI.Label(new Rect(Screen.width -160, 20, 500, 500), string.Format("LAP {0}/{1}", gameController.lapTimes.Count + 1, trackTargetLaps));
        }
        else
        {
            GUI.Label(new Rect(Screen.width - 350, 50, 500, 500), string.Format("LAP {0}", gameController.lapTimes.Count + 1));
        }


        GUI.skin.label.fontSize = 40;
        
        GUI.Box(new Rect(Screen.width -350, 150, 300, 800), "Lap Times");
        
        //currentLapTime = string.Format(@"{0:mm\:ss\:ff}", gameController.currentLapTime);


        for (int i = 0; i < gameController.lapTimes.Count; i++)
        {
            GUI.Label(new Rect(Screen.width - 325, 200 + (50 * i), 400, 1200), string.Format(@"{0:mm\:ss\:ff}", gameController.lapTimes[i]));
        }
    }

}