using MultiplayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testingUI : MonoBehaviour
{
    public InputField input;
    public GameObject platform,button, textinput,exit;

    // Start is called before the first frame update
    PlayerName name;
    void Start()
    {
        exit.SetActive(false);

        button.SetActive(true);
        textinput.SetActive(true);
        platform.SetActive(false);
        name = new PlayerName();
    }
    public void Setname()
    {
        name.SetPlayerName(input.text);
    }
    // Update is called once per frame
    public void SetPlatform()
    {
        exit.SetActive(true);
        button.SetActive(false);
        textinput.SetActive(false);
        platform.SetActive(true);
    }
    public void ExitRoom()
    {
        exit.SetActive(false);
        button.SetActive(true);
        textinput.SetActive(true);
        platform.SetActive(false);
    }
}
