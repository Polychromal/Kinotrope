using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFlame : MonoBehaviour
{

    public PlayerInputActions playerInputActions;
    public GameController gameController;
    public PlayerController playerController;

    public Transform mainFlame;
    public Transform leftFlame;
    public Transform rightFlame;
    public float accelerationLerp;
    public float steeringInput;

    public Vector3 noSmallFlame = new Vector3(0, 0, 0);
    public Vector3 noBigFlame = new Vector3(0, 0, 0);
    public Vector3 bigFlame = new Vector3(2, 1, 4);
    public Vector3 smallFlame = new Vector3(1f, 1f, 4f);

    public bool isPaused = false;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        //Acceleration
        playerInputActions.Player.Accelerating.performed += context => accelerationLerp = context.ReadValue<float>();
        playerInputActions.Player.Accelerating.canceled += context => accelerationLerp = 0;

        //Steering
        playerInputActions.Player.Steering.performed += context => steeringInput = context.ReadValue<Vector2>().x;
        playerInputActions.Player.Steering.canceled += context => steeringInput = 0;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    void Update()
    {
        if (!isPaused)
        {
            mainFlame.localScale = Vector3.Lerp(noBigFlame, bigFlame, accelerationLerp * gameController.playerSpeedDecimal);
            leftFlame.localScale = Vector3.Lerp(noSmallFlame, smallFlame, accelerationLerp * gameController.playerSpeedDecimal);
            rightFlame.localScale = Vector3.Lerp(noSmallFlame, smallFlame, accelerationLerp * gameController.playerSpeedDecimal);
        }
        
    }
}
