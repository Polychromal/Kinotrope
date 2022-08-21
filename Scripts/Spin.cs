using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.RotateAround(transform.localPosition, transform.forward, Time.deltaTime * speed);
    }
}
