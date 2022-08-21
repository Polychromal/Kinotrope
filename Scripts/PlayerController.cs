using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerInputActions playerInputActions;
    
    public GameController gameController;
    public GameObject avatar;
    public Rigidbody playerRigidbody;
    public ShipRaytraceBary shipRaytraceBary;
    public StartFinish startFinish;
    public Energy energy;
    public GameObject energyFill;
    public GameObject player;
    public TrailRenderer trailRenderer;
    public AudioSource worldAudio;
    public AudioSource playerAudio;
    public ShipFlame shipFlame;
    public GameObject pauseMenu;

    public bool paused;
    public float pausePressed;

    public float playerTopSpeed;

    public float playerVelocity;

    public float baseAcceleration;
    public float maxAcceleration;
    public float playerCurrentAcceleration;
    public float accelerationInput;

    public float normalDrag;
    public float brakingDrag;
    public float brakingInput;
    public float deathDrag;

    public bool boosting;
    public bool energyBoosting;
    public float energyBoostFactor;
    public float energyBoostDamage;

    public float torque;
    public float turnSpeed;
    public float velocityTurnSpeed;
    public float steeringInput;

    public float hoverHeight;
    public float hoverDownForce;
    public float hoverUpForce;
    public float hoverDifference;

    public float hoverSmoothing;

    public float gravity;
    public bool grounded = true;

    public bool shipCrahed = false;
    public Vector3 explosionForce;
    public float crashDirection;
    public int crashMinTorque;
    public int crashMaxTorque;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        pauseMenu.SetActive(false);

        //Acceleration
        playerInputActions.Player.Accelerating.performed += context => accelerationInput = context.ReadValue<float>();
        playerInputActions.Player.Accelerating.canceled += context => accelerationInput = 0;

        //Braking
        playerInputActions.Player.Braking.performed += context => brakingInput = context.ReadValue<float>();
        playerInputActions.Player.Braking.canceled += context => brakingInput = 0;

        //Steering
        playerInputActions.Player.Steering.performed += context => steeringInput = context.ReadValue<Vector2>().x;
        playerInputActions.Player.Steering.canceled += context => steeringInput = 0;

        //Energy Boost
        playerInputActions.Player.EnergyBoost.performed += context => energyBoosting = true;
        playerInputActions.Player.EnergyBoost.canceled += context => energyBoosting = false;
        playerInputActions.Player.EnergyBoost.canceled += context => energy.StopDamageOverTime();

        //Pause
        playerInputActions.Player.Pause.performed += context => Pause();

    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void Start()
    {
        boosting = false;
        energyBoosting = false;
        playerCurrentAcceleration = baseAcceleration;
    }

    private void FixedUpdate()
    {
        

        AlignWithTrack();    
        Hover();
        Gravity();

        if (gameController.playerLocked == false)
        {
            Accelerate();        
            Brake();
            Steer();     
        }
    }

    private void Update()
    {
        // Send speed back to game controller
        gameController.UpdatePlayerSpeed(playerRigidbody.velocity.magnitude);

        if (energyBoosting == true && energy.takingDamageOverTime == false)
        {
            EnergyBoost();
        }
        else if (energyBoosting == false && boosting == false)
        {
            playerCurrentAcceleration = baseAcceleration;
        }
    }

    public void Pause()
    {
        paused = !paused;

        if (paused == true)
        {
            Time.timeScale = 0;
            worldAudio.Pause();
            playerAudio.Pause();
            shipFlame.isPaused = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            if(gameController.raceHasStarted == true)
            {
                worldAudio.Play();
            }
            playerAudio.Play();
            shipFlame.isPaused = false;
            pauseMenu.SetActive(false);
        }

    }

    public void Boost(float boostFactor)
    {
        StartCoroutine(BoostCoroutine(boostFactor));
    }

    IEnumerator BoostCoroutine(float boostFactor)
    {
        boosting = true; 
        playerCurrentAcceleration = baseAcceleration * boostFactor;
        yield return new WaitForSeconds(1.5f);
        boosting = false;
        playerCurrentAcceleration = baseAcceleration;
    }

    public void Accelerate()
    {
        if (gameController.playerSpeed < playerTopSpeed)
        {
            playerRigidbody.AddForce(avatar.transform.forward * (playerCurrentAcceleration * accelerationInput));
        }

        playerRigidbody.velocity = player.transform.forward * playerRigidbody.velocity.magnitude;
    }

    private void Brake()
    {
        playerRigidbody.drag = Mathf.Lerp(normalDrag, brakingDrag, brakingInput);
    }

    private void Steer()
    {
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(0, turnSpeed * (1.6f - gameController.playerSpeedDecimal) * steeringInput, 0));


        if (steeringInput !=0)
        {
            avatar.transform.localRotation = Quaternion.Slerp(avatar.transform.localRotation, Quaternion.Euler(0, 5 * steeringInput, 10 * -steeringInput), 2  * Time.deltaTime);
        }
        else
        {
            avatar.transform.localRotation = Quaternion.Slerp(avatar.transform.localRotation, Quaternion.Euler(0, 0, 0), 5 * Time.deltaTime);
        }
       
    }

    private void Hover()
    {
        hoverDifference = hoverHeight - shipRaytraceBary.currentTrackDistance;

        if (shipRaytraceBary.trackDetected == true)
        {
            if (shipRaytraceBary.trackDistance < hoverHeight)
            {
                playerRigidbody.MovePosition(transform.position + (transform.up * hoverDifference));
            }
            else
            {
                playerRigidbody.MovePosition(transform.position + (transform.up * (hoverDifference * 5 * Time.deltaTime)));
            }
            
        }
    }

    private void AlignWithTrack()
    {
        if (grounded == true)
        {
            playerRigidbody.MoveRotation(Quaternion.FromToRotation(transform.up, shipRaytraceBary.trackNormal) * transform.rotation);

        }
     
        
    }

    private void Gravity()
    {
        if (shipRaytraceBary.trackDetected == false)
        {
            playerRigidbody.AddForce(Vector3.up * gravity);
        }
    }

    private void EnergyBoost()
    {
        playerCurrentAcceleration = baseAcceleration * energyBoostFactor;
        energy.StartDamageOverTime();
        
    }





    public void ShipExplosion()
    {
        shipCrahed = true;
        playerRigidbody.drag = deathDrag;
        hoverHeight = 0;
       
        //smoke.Play();
        

        crashDirection = Random.value;

        if (crashDirection <0.5f)
        {
            playerRigidbody.AddTorque(Random.Range(crashMinTorque, crashMaxTorque), Random.Range(crashMinTorque, crashMaxTorque), Random.Range(crashMinTorque, crashMaxTorque));
            
        }
        else
        {
            playerRigidbody.AddTorque(Random.Range(-crashMinTorque, -crashMaxTorque), 0, Random.Range(-crashMinTorque, -crashMaxTorque));
        }

    }

}
