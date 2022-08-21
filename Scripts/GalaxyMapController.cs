using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GalaxyMapController : MonoBehaviour
{
    public Camera mainCamera;
    public float speed;
    public float rotationSlerp;

    public MenuItem[] menuItems;
    public TMP_Text titleText;
    public SceneSoundManager sceneSoundManager;

    public void Start()
    {
        SelectPlanet("NoPlanet");
        titleText = GameObject.Find("titleText").GetComponent<TextMeshPro>();
    }

    public void SelectPlanet(string name)
    {
        foreach (MenuItem menuItem in menuItems)
        {
            if (menuItem.name == name)
            {
                mainCamera.transform.SetParent(menuItem.cameraPosition);
                ResetCamera();
                titleText.text = menuItem.titleName;
            }
        }
           
    }



    private void ResetCamera()
    {
        mainCamera.transform.localPosition = new Vector3(0, 0, 0);
        mainCamera.transform.localRotation = new Quaternion(0, 0, 0, 0);
    }
}

