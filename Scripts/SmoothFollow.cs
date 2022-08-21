using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    Vector3 targetLocation;
    public float damping;
    public float lerp;

    // Update is called once per frame
    void FixedUpdate()
    {
     
        {
            targetLocation = target.position;

            transform.position = Vector3.Lerp(transform.position, target.position, lerp);
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, damping * Time.deltaTime);
        }
            
    }
}