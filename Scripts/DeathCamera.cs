using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCamera : MonoBehaviour
{
    public Transform player;
    public Transform DeathCamTarget;

    public bool DeathCamActive = false;

    public void Update()
    {
        if (DeathCamActive == true)
        {
            transform.position = DeathCamTarget.position;
            transform.LookAt(player);
        }

    }
}
