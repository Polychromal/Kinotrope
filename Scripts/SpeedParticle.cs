using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedParticle : MonoBehaviour
{
    public GameController gameController;
    public ParticleSystem speedParticle;

    public Color fast;
    public Color slow;


    private void Update()
    {
        speedParticle.startColor = Color.Lerp(slow, fast, gameController.playerSpeedDecimal - 0.3f);
    }
}
