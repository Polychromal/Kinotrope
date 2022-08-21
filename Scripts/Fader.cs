using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public StudioLogo studioLogo;
    public AudioSource audioSource;
    public AudioClip studioClip;
    public float waitAfterLogo;

    IEnumerator Countdown()
    {
        //waitBerofeLogo
        yield return new WaitForSeconds(3);

        audioSource.PlayOneShot(studioClip);

        studioLogo.FadeIn();

        yield return new WaitForSeconds(3);

        studioLogo.FadeOut();

        yield return new WaitForSeconds(3);

    }
}