using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxySpin : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(Time.deltaTime * 0, 0, speed, Space.Self);
    }
}