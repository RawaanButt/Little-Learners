using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PubNubAPI;
using System;

public class Commsinfo
{
    public string target;
    public string command;
}
public class GalaxyShooterMultiplayerManager : MonoBehaviour
{
    public spawnManager spawnScript;
    public Text ScoreText;

    private int last20Score, last50Score;
    PubNub pubnub;
    string roomName;

    void Start()
    {
        roomName = PlayerPrefs.GetString("roomName");
        print("Last Room: " + roomName);
        last20Score = 0;
        last50Score = 0;

        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.SubscribeKey = "sub-c-ca80b9a0-66f1-11ec-bc76-5a1a0086ddff";
        pnConfiguration.PublishKey = "pub-c-d97bcd93-9fa7-436b-862c-9827d36bc9c0";
        pnConfiguration.SecretKey = "sec-c-YzE5ODk0YzctOWY0MS00Yzc5LWIxZTctNmFkNjNkZDhlODk5";
        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UUID = UnityEngine.Random.Range(0, 99999999f).ToString();

        pubnub = new PubNub(pnConfiguration);

        pubnub.Subscribe()
            .Channels(new List<string>() { roomName + ".GalaxyShooter" })
            .WithPresence()
            .Execute();

        //Add all power-up methods
        pubnub.SubscribeCallback += OnReceivedMessage;
    }
    private void Update()
    {
        int score = 0; 
        int.TryParse(ScoreText.text.Split(' ')[1], out score);

        if (score <= 0)
        {
            last50Score = 0;
            last20Score = 0;
            return;
        }

        if (score - last20Score >= 20)
        {
            print("Got 20 POints!");
            last20Score = score;
            OnAdd10PointsForSpecialChild();
        }
        if (score - last50Score >= 50)
        {
            last50Score = score;
            OnStartAnimationForSpecialChild();
        }
    }

    public void OnReceivedMessage(object sender, EventArgs e)
    {
        print("RECEIVED MESSAGE!!");
        SubscribeEventEventArgs se = e as SubscribeEventEventArgs;
        if (se.MessageResult != null)
        {
            string msg = se.MessageResult.Payload as String;
            print("Message: " + msg);
            if (msg == "IncreaseFireRate")
            {
                IncreasedFireRate();
            }
            else if (msg == "IncreaseMoveSpeed")
            {
                IncreasedMovementSpeed();
            }
            else if (msg == "Shield")
            {
                Shield();
            }
            else if (msg == "NewEnemy")
            {
                SpawnNewEnemy();
            }
            else if (msg == "Astroid")
            {
                SpawnAStriod();
            }
            else if (msg == "DecreaseMoveSpeed")
            {
                DecreaseMovementSpeed();
            }
        }
    }

    //'On' = LocalEvents
    #region Received Power-Ups/Downs
    public void IncreasedFireRate()
    {
        spawnScript.SpawnPowerUpAtShipPos(0);
    }
    public void IncreasedMovementSpeed()
    {
        spawnScript.SpawnPowerUpAtShipPos(1);
    }
    public void Shield()
    {
        spawnScript.SpawnPowerUpAtShipPos(2);
    }
    public void SpawnNewEnemy()
    {
        spawnScript.SpawnRandEnemyNow();
    }
    public void SpawnAStriod()
    {
        spawnScript.SpawnAstriod();
    }
    public void DecreaseMovementSpeed()
    {
        PlayerScript player = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        player.speedbost = false;
        player.powerup();
    }
    //Happens when get 20 more points
    public void OnAdd10PointsForSpecialChild() => SendToServer("Gain10Points");
    //Happens when get 50 more points
    public void OnStartAnimationForSpecialChild() => SendToServer("StarAndAnimation");

    #endregion
    void SendToServer(string _command)
    {
        pubnub.Publish()
            .Channel("comms")
            .Message(JsonUtility.ToJson(new Commsinfo() {target = roomName + ".TinyHands", command = _command }))
            .Async((result, status) => print("Place Holder"));
    }
    public void OnEndGame()
    {
        SceneManager.LoadScene("Mian Menu");
        Destroy(gameObject);
    }
}
