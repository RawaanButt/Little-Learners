using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{

    // public GameObject BlackImage;
    public GameObject resumeBtn;
    public GameObject newBtn;
    public GameObject loadingText;
    private DatabaseReference reference;
    private int session;
    public bool status;

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                // Debug.Log (pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                Status myObject = JsonUtility.FromJson<Status>(webRequest.downloadHandler.text);
                status = myObject.status;
                // Debug.Log (myObject);
                // Debug.Log (myObject.status);
            }
        }
    }
    IEnumerator GetRequest2(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                Session myObject2 = JsonUtility.FromJson<Session>(webRequest.downloadHandler.text);
                session = myObject2.session;

                // Debug.Log(myObject2);
                // Debug.Log (myObject.status);
            }
        }
    }
    public IEnumerator Start()
    {
        string username = File.ReadAllText(Application.dataPath + "/user.txt");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        String gameT = "";
        if (sceneName == "game2l1" || sceneName == "game2l1Multiplayer")
            gameT = "sorting";
        else if (sceneName == "game3l1" || sceneName == "game3l1Multiplayer")
            gameT = "select/" + "classification";
        else if (sceneName == "game4l1" || sceneName == "game4l1Multiplayer")
            gameT = "select/" + "selection";
        else if (sceneName == "game5l1" || sceneName == "game5l1Multiplayer")
            gameT = "select/" + "adjective";
        else if (sceneName == "game6l1" || sceneName == "game6l1Multiplayer")
            gameT = "select/" + "emotions";
        else if (sceneName == "game7l1" || sceneName == "game7l1Multiplayer")
            gameT = "select/" + "association";
        else if (sceneName == "New" || sceneName == "NewMultiplayer")
            gameT = "select/" + "matching";

        string game = gameT == "sorting" ? gameT : gameT.Split('/')[1];
        yield return StartCoroutine(GetRequest("https://evening-fortress-14821.herokuapp.com/" + gameT + "/" + username + "/check"));
        yield return StartCoroutine(GetRequest2("https://evening-fortress-14821.herokuapp.com/analytics/session/" + username + "/" + game));

        if (status)
        {
            Debug.Log("DTA");
            loadingText.SetActive(false);
            resumeBtn.SetActive(true);
            newBtn.SetActive(true);
        }
        else
        {
            DisableImage();
        }
    }
    public void EnableImage()
    {
        loadingText.SetActive(true);
        resumeBtn.SetActive(false);
        newBtn.SetActive(false);

        callWith(true);
        // Debug.Log("second");

        // BlackImage.SetActive(false);
    }

    public void DisableImage()
    {
        callWith(false);
    }

    public void callWith(bool resume)
    {
        if (!resume)
        {
            session++;
        }
        string username = File.ReadAllText(Application.dataPath + "/user.txt");

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "game2l1" || sceneName == "game2l1Multiplayer")
        {
            if (GameObject.Find("LevelManager1"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager1");
                SortingS1 playerScript = thePlayer.GetComponent<SortingS1>();
                playerScript.onStarting(resume, session);

            }
            if (GameObject.Find("LevelManager"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager");
                SortingS1 playerScript = thePlayer.GetComponent<SortingS1>();
                playerScript.onStarting(resume, session);

            }
            if (GameObject.Find("LevelManager2"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager2");
                SortingS2 playerScript = thePlayer.GetComponent<SortingS2>();
                playerScript.onStarting(resume, session);

            }

        }
        else if (sceneName == "game3l1" || sceneName == "game3l1Multiplayer")
        {
            Debug.Log("Cal,,");
            if (GameObject.Find("LevelManager"))
            {
                Debug.Log("Cal,,2");

                GameObject thePlayer = GameObject.Find("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.onStarting(resume, session);
            }
            if (GameObject.Find("LevelManager1"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager1");
                game3l2 playerScript = thePlayer.GetComponent<game3l2>();
                playerScript.onStarting(resume, session);
            }

        }
        else if (sceneName == "game4l1" || sceneName == "game4l1Multiplayer")
        {
            if (GameObject.Find("LevelManager"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.onStarting(resume, session);

            }
            if (GameObject.Find("LevelManager1"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager1");
                game3l2 playerScript = thePlayer.GetComponent<game3l2>();
                playerScript.onStarting(resume, session);

            }
        }
        else if (sceneName == "game5l1" || sceneName == "game5l1Multiplayer")
        {
            if (GameObject.Find("LevelManager"))
            {
                Debug.Log("onstart");
                GameObject thePlayer = GameObject.Find("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.onStarting(resume, session);

            }
            if (GameObject.Find("LevelManager1"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager1");
                game3l2 playerScript = thePlayer.GetComponent<game3l2>();
                playerScript.onStarting(resume, session);

            }
        }
        else if (sceneName == "game6l1" || sceneName == "game6l1Multiplayer")
        {
            if (GameObject.Find("LevelManager"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager");
                Game6l1 playerScript = thePlayer.GetComponent<Game6l1>();
                playerScript.onStarting(resume, session);


            }
            if (GameObject.Find("LevelManager1"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager1");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.onStarting(resume, session);

            }
            if (GameObject.Find("LevelManager2"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager2");
                game6l3 playerScript = thePlayer.GetComponent<game6l3>();
                playerScript.onStarting(resume, session);

            }
        }
        else if (sceneName == "game7l1" || sceneName == "game7l1Multiplayer")
        {
            if (GameObject.Find("LevelManager"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager");
                Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                playerScript.onStarting(resume, session);
            }
            if (GameObject.Find("LevelManager1"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager1");
                game7l2 playerScript = thePlayer.GetComponent<game7l2>();
                playerScript.onStarting(resume, session);
            }
            if (GameObject.Find("LevelManager2"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager2");
                game7l3 playerScript = thePlayer.GetComponent<game7l3>();
                playerScript.onStarting(resume, session);
            }
        }
        else if (sceneName == "New" || sceneName == "NewMultiplayer")
        {

            if (GameObject.Find("LevelManager"))
            {

                GameObject thePlayer = GameObject.Find("LevelManager");
                ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer>();
                playerScript.onStarting(resume, session);

            }
            if (GameObject.Find("Levelmanager"))
            {

                // GameObject thePlayer = GameObject.Find("Levelmanager");
                // LevelManager playerScript = thePlayer.GetComponent<LevelManager>();
                // playerScript.onStarting(resume);

            }
            if (GameObject.Find("levelManager"))
            {
                GameObject thePlayer = GameObject.Find("levelManager");
                ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer>();
                playerScript.onStarting(resume, session);

            }
            if (GameObject.Find("Levelmanager1"))
            {
                // GameObject thePlayer = GameObject.Find("Levelmanager1");
                // LevelManager playerScript = thePlayer.GetComponent<LevelManager>();
                // playerScript.onStarting(resume);

            }
            if (GameObject.Find("LevelManager1"))
            {
                GameObject thePlayer = GameObject.Find("LevelManager1");
                ObjectRenderer1 playerScript = thePlayer.GetComponent<ObjectRenderer1>();
                playerScript.onStarting(resume, session);

            }
        }

    }

    public void Sorting()
    {
        SceneManager.LoadScene("Sorting");
    }
}

[Serializable]
class Status
{
    public bool status;

}

[Serializable]
class Session
{
    public int session;

}