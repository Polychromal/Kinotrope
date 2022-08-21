using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KalasskuEye : MonoBehaviour
{
    public Transform eyeBall;
    public Transform eyeTarget;

    private void Update()
    {
        eyeBall.LookAt(eyeTarget);
    }
}
