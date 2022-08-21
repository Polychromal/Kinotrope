using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Slider energySlider;
    public Energy energy;
    public GameObject fillColour;

    public Color healthBar;
    public Color normalHealth = new Color32(170, 255, 0, 255);
    public Color damageFlash = new Color32(255, 255, 255, 255);
    public Color lowHealth = new Color32(255, 0, 0, 255);


    public float healthLerp;

    private void Start()
    {
        healthBar = normalHealth;
    }

    private void Update()
    {
        fillColour.GetComponent<Image>().color = healthBar;
        healthLerp = energy.currentEnergy / 100;
        healthBar = Color.Lerp(lowHealth, normalHealth, healthLerp);
        
    }

    public void UpdateEnergy(float energy)
    {
        energySlider.value = energy;


    }

    public void UpdateMaxEnergy(float energy)
    {
        energySlider.maxValue = energy;
        energySlider.value = energy;
    }

    public void HealthFlash()
    {
        StartCoroutine(Flash());

        IEnumerator Flash()
        {
            fillColour.GetComponent<Image>().color = damageFlash;
            yield return new WaitForSeconds(0.1f);
            fillColour.GetComponent<Image>().color = healthBar;
        }
    }
}
