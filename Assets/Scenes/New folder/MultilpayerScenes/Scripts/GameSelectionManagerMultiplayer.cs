using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PubNubAPI;
using System;

public class SelectionInfo
{
    public string name;
    public string mode;
}
public class GameSelectionManagerMultiplayer : MonoBehaviour
{
    public PubNub pubnub;

    public Text waitText;
    public GameObject normalButton, specialButton, chooseUI;
    public GameObject tinyHandsMultManager;
    private GameObject lastChosen;

    private int gameModesSelected;
    private string roomName, currentGameMode;

    private void Start()
    {
        gameModesSelected = 0;
        roomName = PlayerPrefs.GetString("roomName");

        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.SubscribeKey = "sub-c-ca80b9a0-66f1-11ec-bc76-5a1a0086ddff";
        pnConfiguration.PublishKey = "pub-c-d97bcd93-9fa7-436b-862c-9827d36bc9c0";
        pnConfiguration.SecretKey = "sec-c-YzE5ODk0YzctOWY0MS00Yzc5LWIxZTctNmFkNjNkZDhlODk5";
        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UUID = UnityEngine.Random.Range(0, 999999f).ToString();

        pubnub = new PubNub(pnConfiguration);

        pubnub.Subscribe()
            .Channels(new List<string>() {roomName + ".GameSelection" })
            .WithPresence()
            .Execute();

        pubnub.SubscribeCallback += GameModeSelectedByOtherUser;
    }

    public void ButtonClicked(GameObject button)
    {
        lastChosen = button;
        normalButton.SetActive(false);
        specialButton.SetActive(false);

        pubnub.Publish()
            .Channel("game_selection")
            .Message(JsonUtility.ToJson(new SelectionInfo() { name = roomName, mode = (button.name.ToLower().Contains("normal") ? "normal" : "special") }))
            .Async((result, status) => print("sent"));
    }

    void GameModeSelectedByOtherUser(object sender, EventArgs e)
    {
        SubscribeEventEventArgs se = e as SubscribeEventEventArgs;
        if (se.MessageResult != null && se.MessageResult.Channel == roomName + ".GameSelection")
        {
            //Dictionary<string, object> msg = se.MessageResult.Payload as Dictionary<string, object>;

            //GameModeChosen string is in format 'isOpen':'true'/'false'
            string message = se.MessageResult.Payload as String;
            string isOpen = message.Split('#')[0];
            
            if(isOpen == "true")
            {
                string modesSelected = message.Split('#')[1];
                waitText.text = "Waiting for other user to select!";
                gameModesSelected++;
                if (modesSelected == "2")
                {
                    LoadScenes();
                }
            }
            else
            {
                waitText.text = "Other User picked that one!";
                normalButton.SetActive(true);
                specialButton.SetActive(true);
            }
        }
    }

    public void SetCurrentGameMode(string mode)
    {
        currentGameMode = mode;
    }

    void LoadScenes()
    {
        if (currentGameMode == "normal")
        {
            SceneManager.LoadScene("GalaxyShooter");
        }
        else
        {
            chooseUI.SetActive(false);
            Instantiate(tinyHandsMultManager);
            this.enabled = false;
        }
    }
}
