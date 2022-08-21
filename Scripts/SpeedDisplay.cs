using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDisplay : MonoBehaviour

{
    public GameController gameController;
    public Font font;
    public string speed;

    void OnGUI()
    {
        GUI.skin.font = font;
        GUI.skin.label.fontSize = 80;
        GUI.skin.box.fontSize = 40;
        speed = string.Format("{0:000}", gameController.playerSpeed * 2.237);

        GUI.Box(new Rect(10, Screen.height - 180, 500, 160), "Speed");

        GUI.Label(new Rect(35, Screen.height -130, 600, 200), $"{speed} mph");

    }
}
