using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{

    public GameController gameController;
    public AudioSource audioSource;

    public float audioPitch;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.pitch = audioPitch;
        audioPitch = 0.1f * (gameController.playerSpeed / 20);
    }
}
