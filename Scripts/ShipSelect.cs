using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelect : MonoBehaviour
{
    //public Transform character;
    public Transform ship;

    // Update is called once per frame
    private void Update()
    {
        ship.rotation = Quaternion.Euler(0, 0.5f, 0) * ship.rotation;
    }
}
