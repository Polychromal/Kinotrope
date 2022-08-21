using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public Rigidbody player;
    public GameController gameController;
    public PlayerController playerController;

    public AudioSource audioSource;
    public AudioClip alarm;

    public bool healthLow = false;
    public bool takingDamageOverTime;

    public float maxEnergy = 100;
    public float currentEnergy;

    public EnergyBar energyBar;


    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.UpdateMaxEnergy(maxEnergy);
    }

    private void Update()
    {

        if (currentEnergy <= 30 && healthLow == false)
        {
            healthLow = true;
            LowHealthWarning();
        }

        if (currentEnergy > 30)
        {
            healthLow = false;
            LowHealthWarning();
        }

        if (currentEnergy <= 10)
        {
            audioSource.pitch = 2f;
        }
        else if (currentEnergy <= 20 && currentEnergy > 10)
        {
            audioSource.pitch = 1.5f;
        }
        else if (currentEnergy <= 30 && currentEnergy > 20)
        {
            audioSource.pitch = 1f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Level")
        {
            TakeDamage(1);
        }

        if (collision.gameObject.tag == "OutOfBounds")
        {
            TakeDamage(100);
        }

    }

    public void TakeDamage(float damage)
    {
        currentEnergy -= damage;
        energyBar.UpdateEnergy(currentEnergy);

        energyBar.HealthFlash();

        if (currentEnergy <= 0)
        {
            gameController.PlayerDeath();
        }
    }

    public void StartDamageOverTime()
    {
        takingDamageOverTime = true;

        StartCoroutine(Tick());
        IEnumerator Tick()
        {
            while(takingDamageOverTime == true)
            {
                TakeDamage(1.0f);
                yield return new WaitForSeconds(0.5f);
            }

        }
    }

    public void StopDamageOverTime()
    {
        takingDamageOverTime = false;
    }

    public void HealDamage(float damage)
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += damage;
        }

        if (currentEnergy > 30)
        {
            healthLow = false;
        }

        energyBar.UpdateEnergy(currentEnergy);
    }

    public void LowHealthWarning()
    {
        if (healthLow == true)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
