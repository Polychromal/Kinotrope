using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPad : MonoBehaviour
{
    public Energy energy;
    public AudioSource audioSource;
    public AudioClip energyPadSound;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            energy.HealDamage(2);
            PadSound();
        }
        
    }

    private void PadSound()
    {
        //audioSource.PlayOneShot(energyPadSound);
    }
    
}
