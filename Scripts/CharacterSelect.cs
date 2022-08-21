using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterSelect : MonoBehaviour
{

    public TMP_Text characterName = GameObject.Find("Character Name").GetComponent<TextMeshPro>();


    public GameObject[] characters;
    public int selectedCharacter;

    private void Start()
    {
        Tinkerer();
        //button =
        //initialButtonSelected.Select();
    }

    public void Tinkerer()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = 0;
        characters[selectedCharacter].SetActive(true);
        characterName.text = "The Tinkerer";
    }

    public void Baroness()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = 1;
        characters[selectedCharacter].SetActive(true);
        characterName.text = "The Baroness";
    }

    public void BugThing()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = 2;
        characters[selectedCharacter].SetActive(true);
        characterName.text = "Bug Thing";
    }
}
