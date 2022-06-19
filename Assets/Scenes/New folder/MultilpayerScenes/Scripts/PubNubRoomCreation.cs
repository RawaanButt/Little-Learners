using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PubNubAPI;
using System;



public class info
{
    public string command;
    public string name;
}

public class PubNubRoomCreation : MonoBehaviour
{
    public  PubNub pubnub;

    public InputField nameInput;

    private string RoomCallbackChannel;
    public GameObject Empty;

    void Start()
    {
        print("Last Room: " + PlayerPrefs.GetString("roomName"));
        PNConfiguration pnConfiguration = new PNConfiguration();
        pnConfiguration.SubscribeKey = "sub-c-ca80b9a0-66f1-11ec-bc76-5a1a0086ddff";
        pnConfiguration.PublishKey = "pub-c-d97bcd93-9fa7-436b-862c-9827d36bc9c0";
        pnConfiguration.SecretKey = "sec-c-YzE5ODk0YzctOWY0MS00Yzc5LWIxZTctNmFkNjNkZDhlODk5";
        pnConfiguration.LogVerbosity = PNLogVerbosity.BODY;
        pnConfiguration.UUID = UnityEngine.Random.Range(0, 999999f).ToString();

        pubnub = new PubNub(pnConfiguration);
        pubnub.Fire()
            .Channel("submit_room")
            .Message(JsonUtility.ToJson(new info()))
            .Async((result, status) => print("Done Firing"));

        pubnub.SubscribeCallback += (sender, e) => { print("callback for join room"); };
    }
    public void CreateRoom()
    {
        
            RoomCallbackChannel = "CreateRoom." + nameInput.text;
            PlayerPrefs.SetString("roomName", nameInput.text);
            pubnub.SubscribeCallback += OnCreateRoomCallback;
            pubnub.Subscribe()
                .Channels(new List<string>() { RoomCallbackChannel })
                .WithPresence()
                .Execute();


            pubnub.Publish()
                .Channel("submit_room")
                .Message(JsonUtility.ToJson(new info { command = "create", name = nameInput.text }))
                .Async((result, status) => print("join room"));

            print("helor");
        
    }

    public void JoinRoom()
    {
        RoomCallbackChannel = "JoinRoom." + nameInput.text;
        pubnub.SubscribeCallback += OnJoinRoomCallback;
        PlayerPrefs.SetString("roomName", nameInput.text);

        pubnub.Subscribe()
            .Channels(new List<string>() { RoomCallbackChannel })
            .WithPresence()
            .Execute();

        print(RoomCallbackChannel);

        pubnub.Publish()
            .Channel("submit_room")
            .Message(JsonUtility.ToJson(new info { command = "join", name = nameInput.text }))
            .Async((result, status) => print("join room"));

        print("joining room");
    }
    private void OnCreateRoomCallback(object sender, EventArgs e)
    {
        print("callback for join room");
        SubscribeEventEventArgs se = e as SubscribeEventEventArgs;

        if (se.MessageResult != null && se.MessageResult.Channel == RoomCallbackChannel)
        {
            //Dictionary<string, object> msg = se.MessageResult.Payload as Dictionary<string, object>;

            string canCreateRoom = se.MessageResult.Payload as String;
            print(canCreateRoom);

            if(canCreateRoom == "false")
            {
                CannotCreateRoom();
            }
            else
            {
                RoomCreated();
            }

            pubnub.SubscribeCallback -= OnCreateRoomCallback;
            pubnub.Unsubscribe().Channels(new List<string>() { RoomCallbackChannel });
        }
    }
    private void OnJoinRoomCallback(object sender, EventArgs e)
    {
        SubscribeEventEventArgs se = e as SubscribeEventEventArgs;

        print("callback for join room");

        if (se.MessageResult != null && se.MessageResult.Channel == RoomCallbackChannel)
        {
            //Dictionary<string, object> msg = se.MessageResult.Payload as Dictionary<string, object>;

            string canJoin = se.MessageResult.Payload as String;
            print(canJoin);

            if (canJoin == "false")
            {
                CannotJoinRoom();
            }
            else
            {
                RoomJoined();
            }

            pubnub.SubscribeCallback -= OnJoinRoomCallback;
            pubnub.Unsubscribe().Channels(new List<string>() { RoomCallbackChannel });
        }
    }
    private void CannotCreateRoom()
    {
        print("unable to create room with that name");
    }
    private void RoomCreated()
    {
        print("room created");
        SceneManager.LoadScene("Game Selection Multiplayer");
    }
    private void CannotJoinRoom()
    {
        print("Unable to join room with that name");
    }
    private void RoomJoined()
    {
        print("room joined");
        SceneManager.LoadScene("Game Selection Multiplayer");
    }
}
