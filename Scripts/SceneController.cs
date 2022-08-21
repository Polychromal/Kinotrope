using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(sceneName: "Title Screen");
    }

    public void LoadModeSelect()
    {
        SceneManager.LoadScene(sceneName : "Mode Select");
    }

    public void LoadPlanetSelect()
    {
        SceneManager.LoadScene(sceneName: "Planet Select");
    }

    public void LoadCharacterSelect()
    {
        SceneManager.LoadScene(sceneName: "Character Select");
    }

    public void LoadRamparts()
    {
        SceneManager.LoadScene(sceneName: "Ramparts");
    }

    public void LoadOutworld()
    {
        SceneManager.LoadScene(sceneName: "Outworld");
    }

    public void LoadTheClimb()
    {
        SceneManager.LoadScene(sceneName: "The Climb");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
