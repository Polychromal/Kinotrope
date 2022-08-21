using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Rigidbody player;
    public GameController gameController;
    public DeathCamera deathCamera;
    public Transform followTarget;
    public Transform vertigoTarget;
    public float cameraLerp;
    public float cameraAdjust;

    private void LateUpdate()
    {
        if(deathCamera.DeathCamActive == false)
        {
            var velocityFollow = Quaternion.LookRotation(player.velocity, player.transform.up);
            var lookFollow = Quaternion.LookRotation(player.transform.forward, player.transform.up);

            cameraLerp = gameController.playerSpeedDecimal / 4;

            transform.position = Vector3.Lerp(followTarget.position, vertigoTarget.position, gameController.playerSpeedDecimal);
            transform.rotation = Quaternion.Slerp(lookFollow, velocityFollow, cameraLerp) * Quaternion.Euler(cameraAdjust, 0, 0);
        }
    }
}
