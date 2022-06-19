using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PubNubAPI;
//using Game6l1;

public class TinyHandsMultiplayerManager : MonoBehaviour
{
    private Text scoreText;
    private int last10Score;

    PubNub pubnub;
    string roomName;

    private void Start()
    {
        SceneManager.LoadScene("Game Selection");
        StartAsSpecialChild();
    }

    public void StartAsSpecialChild()
    {
        last10Score = 0;

        roomName = PlayerPrefs.GetString("roomName");

        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.SubscribeKey = "sub-c-ca80b9a0-66f1-11ec-bc76-5a1a0086ddff";
        pnConfiguration.PublishKey = "pub-c-d97bcd93-9fa7-436b-862c-9827d36bc9c0";
        pnConfiguration.SecretKey = "sec-c-YzE5ODk0YzctOWY0MS00Yzc5LWIxZTctNmFkNjNkZDhlODk5";
        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UUID = UnityEngine.Random.Range(0, 99999999f).ToString();

        pubnub = new PubNub(pnConfiguration);

        print("Last Room: " + PlayerPrefs.GetString("roomName"));

        pubnub.Subscribe()
            .Channels(new List<string>() { roomName + ".TinyHands" })
            .WithPresence()
            .Execute();

        //Add all power-up methods
        pubnub.SubscribeCallback += OnReceivedMessage;
    }

    private void Update()
    {
        string sName = SceneManager.GetActiveScene().name;

        print("Checkin: " + sName);

        if (sName == "game2l1Multiplayer")
        {
            GameObject sort = GameObject.FindWithTag("SortingS1");
            if (sort != null)
            {
                SortingS1 scoreScript = sort.GetComponent<SortingS1>();
                int score = scoreScript.scorevar;
                
                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
            
        }
        else if (sName == "game3l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            if (sort != null)
            {
                int score = sort.scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
            else
            {
                int score = FindObjectOfType<game3l2>().scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
            
        }
        else if (sName == "game4l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            if (sort != null)
            {
                int score = sort.scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
            else
            {
                int score = FindObjectOfType<game3l2>().scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
        }
        else if (sName == "game5l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            if (sort != null)
            {
                int score = sort.scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
            else
            {
                int score = FindObjectOfType<game3l2>().scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
        }
        else if (sName == "game6l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            Game6l1 sort2 = FindObjectOfType<Game6l1>();
           // Game6l1 emotions = new Game6l1();

            if (sort != null)
            {
                int score = sort.scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                    OnLevelComplete();
                }
            }
            else if (sort2 != null)
            {
                int score = sort2.scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                    OnLevelComplete();
                }
            }
            else
            {
                int score = FindObjectOfType<game6l3>().scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
           /* if(emotions.onclickright)
            {
                OnSkipped();
            }*/
            
        }
        else if (sName == "game7l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            game7l2 sort2 = FindObjectOfType<game7l2>();
            if (sort != null)
            {
                int score = sort.scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
            else if (sort2 != null)
            {
                int score = sort2.scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
            else
            {
                int score = FindObjectOfType<game7l3>().scorevar;

                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
        }
        else if (sName == "NewMultiplayer")
        {
            GameObject scoreText = GameObject.Find("ScoreText");
            if (scoreText != null)
            {
                int score = int.Parse(scoreText.GetComponent<Text>().text);
                if (score <= 0) last10Score = 0;

                if (score - last10Score > 10)
                {
                    last10Score = score;
                    OnGained10Points();
                }
            }
        }
    }

    public void OnReceivedMessage(object sender, EventArgs e)
    {
        print("RECEIVED MESSAGE!!!");
        SubscribeEventEventArgs se = e as SubscribeEventEventArgs;
        if (se.MessageResult != null)
        {
            string msg = se.MessageResult.Payload as String;
            print("Message : " + msg);
            if (msg == "Gain10Points")
            {
                Add10Points();
            }
            else if (msg == "StarAndAnimation")
            {
                StarAndAnimation();
            }
        }
    }

    #region Power-Ups
    void Add10Points()
    {
        string sName = SceneManager.GetActiveScene().name;

        if (sName == "game2l1Multiplayer")
        {
            GameObject sort = GameObject.FindWithTag("SortingS1");
            if (sort != null)
            {
                SortingS1 scoreScript = sort.GetComponent<SortingS1>();

                scoreScript.scorevar += 10;
            }
        }
        else if (sName == "game3l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            if (sort != null)
            {
                sort.scorevar += 10;
            }
            else
            {
                FindObjectOfType<game3l2>().scorevar += 10;
            }
        }
        else if (sName == "game4l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            if (sort != null)
            {
                sort.scorevar += 10;
            }
            else
            {
                FindObjectOfType<game3l2>().scorevar += 10;
            }
        }
        else if (sName == "game5l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            if (sort != null)
            {
                sort.scorevar += 10;
            }
            else
            {
                FindObjectOfType<game3l2>().scorevar += 10;
            }
        }
        else if (sName == "game6l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            Game6l1 sort2 = FindObjectOfType<Game6l1>();
            if (sort != null)
            {
                sort.scorevar += 10;
            }
            else if (sort2 != null)
            {
                sort2.scorevar += 10;
            }
            else
            {
                FindObjectOfType<game6l3>().scorevar += 10;
            }
        }
        else if (sName == "game7l1Multiplayer")
        {
            Game3L1 sort = FindObjectOfType<Game3L1>();
            game7l2 sort2 = FindObjectOfType<game7l2>();
            if (sort != null)
            {
                sort.scorevar += 10;
            }
            else if (sort2 != null)
            {
                sort2.scorevar += 10;
            }
            else
            {
                FindObjectOfType<game7l3>().scorevar += 10;
            }
        }
        else if (sName == "NewMultiplayer")
        {
            ObjectRenderer[] o = FindObjectsOfType<ObjectRenderer>();
            LevelManager[] l = FindObjectsOfType<LevelManager>();

            foreach (ObjectRenderer ob in o) ob.scorevar += 10;
            foreach (LevelManager ob in l) ob.scorevar += 10;
        }
    }
    void StarAndAnimation()
    {
        //Anim.SetTrigger("StarAnimation");
        print("START STAR AND ANIMANATION BECAUSE GALAXY SHOOTER GOT +50 POINTS!!");
    }
    #endregion

    //'On' = Local Events
    #region Recieved Power-Ups and Power-Downs
    public void OnGained10Points()  {SendToServer("IncreaseFireRate"); print("Gained 10 Points Checkin"); }
    public void OnQuickAnswer() => SendToServer("IncreaseMoveSpeed");
    public void OnLevelComplete() => SendToServer("Shield");
    //public void OnGameComplete() => SendToServer("");
    public void OnIncorrectAnswer() => SendToServer("Astroid");
    public void OnLongAnswer() => SendToServer("DecreaseMoveSpeed");
    public void OnSkipped() => SendToServer("NewEnemy");
    #endregion

    void SendToServer(string _command)
    {
        Debug.LogError("Sending to server: " + _command);
        pubnub.Publish()
            .Channel("comms")
            .Message(JsonUtility.ToJson(new Commsinfo() { target = roomName + ".GalaxyShooter", command = _command }))
            .Async((result, status) => Debug.LogWarning("Place Holder"));
    }

    public void EndGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mian Menu");
        Destroy(gameObject);
    }
}
