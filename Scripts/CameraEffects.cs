using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public GameController gamecontroller;
    public PlayerController playerController;
    public Transform mainCamera;
    public float minFieldOfView;
    public float maxFieldOfView;
    public float boostedMaxFieldOfView;
    public float currentMaxFieldOfView;
    public float lerp;

    // Update is called once per frame

    private void Update()
    {
        if(playerController.energyBoosting == true)
        {
            currentMaxFieldOfView = Mathf.Lerp(maxFieldOfView, boostedMaxFieldOfView, Time.deltaTime);
        }
        else
        {
            currentMaxFieldOfView = Mathf.Lerp(currentMaxFieldOfView, maxFieldOfView, Time.deltaTime);
        }

        Camera.main.fieldOfView = minFieldOfView + (currentMaxFieldOfView - minFieldOfView) * gamecontroller.playerSpeedDecimal;
    }


}
