using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SettingScripts : MonoBehaviour
{

    Text percentageText;
    public float speed = 10f;
    public GameObject image;
    public GameObject image11;
    public GameObject Panel;
    public GameObject Panel1;
    public GameObject Panel2;

    public bool Pnl;

    public void OpenPanel()
    {
        if (Panel != null)
        {

            Panel.SetActive(true);
            image11.SetActive(true);

        }
    }

    public void ClosePanel()
    {
        if (Panel != null)
        {

            Panel.SetActive(false);
            image11.SetActive(false);

            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            if (sceneName == "game2l1")
            {

                if (GameObject.Find("LevelManager"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    SortingS1 playerScript = thePlayer.GetComponent<SortingS1>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("LevelManager1"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    SortingS1 playerScript = thePlayer.GetComponent<SortingS1>();
                    playerScript.restartTimer();

                }
                else
                {
                    GameObject thePlayer = GameObject.Find("LevelManager2");
                    SortingS2 playerScript = thePlayer.GetComponent<SortingS2>();
                    playerScript.restartTimer();

                }

            }
            else if (sceneName == "game3l1")
            {
                if (GameObject.Find("LevelManager"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("LevelManager1"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    game3l2 playerScript = thePlayer.GetComponent<game3l2>();
                    playerScript.restartTimer();

                }

            }
            else if (sceneName == "game4l1")
            {
                if (GameObject.Find("LevelManager"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("LevelManager1"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    game3l2 playerScript = thePlayer.GetComponent<game3l2>();
                    playerScript.restartTimer();

                }
            }
            else if (sceneName == "game5l1")
            {
                if (GameObject.Find("LevelManager"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("LevelManager1"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    game3l2 playerScript = thePlayer.GetComponent<game3l2>();
                    playerScript.restartTimer();

                }
            }
            else if (sceneName == "game6l1")
            {
                if (GameObject.Find("LevelManager"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    Game6l1 playerScript = thePlayer.GetComponent<Game6l1>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("LevelManager1"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                    playerScript.restartTimer();

                }
                else
                {
                    GameObject thePlayer = GameObject.Find("LevelManager2");
                    game6l3 playerScript = thePlayer.GetComponent<game6l3>();
                    playerScript.restartTimer();

                }
            }
            else if (sceneName == "game7l1")
            {
                if (GameObject.Find("LevelManager"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    Game3L1 playerScript = thePlayer.GetComponent<Game3L1>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("LevelManager1"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    game7l2 playerScript = thePlayer.GetComponent<game7l2>();
                    playerScript.restartTimer();

                }
                else
                {
                    GameObject thePlayer = GameObject.Find("LevelManager2");
                    game7l3 playerScript = thePlayer.GetComponent<game7l3>();
                    playerScript.restartTimer();

                }
            }
            else if (sceneName == "New")
            {
                if (GameObject.Find("LevelManager"))
                {
                    GameObject thePlayer = GameObject.Find("LevelManager");
                    ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("Levelmanager"))
                {
                    GameObject thePlayer = GameObject.Find("Levelmanager");
                    LevelManager playerScript = thePlayer.GetComponent<LevelManager>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("levelManager"))
                {
                    GameObject thePlayer = GameObject.Find("levelManager");
                    ObjectRenderer playerScript = thePlayer.GetComponent<ObjectRenderer>();
                    playerScript.restartTimer();

                }
                else if (GameObject.Find("Levelmanager1"))
                {
                    GameObject thePlayer = GameObject.Find("Levelmanager1");
                    LevelManager playerScript = thePlayer.GetComponent<LevelManager>();
                    playerScript.restartTimer();

                }
                else
                {
                    GameObject thePlayer = GameObject.Find("LevelManager1");
                    ObjectRenderer1 playerScript = thePlayer.GetComponent<ObjectRenderer1>();
                    playerScript.restartTimer();

                }
            }

        }
    }
    int tap;
    public void OnPointerClick()
    {
        tap++;
        StartCoroutine(DoubleTapInterval());

    }

    IEnumerator DoubleTapInterval()
    {

        yield return new WaitForSeconds(0.3f);

        if (tap == 1)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            if (sceneName == "Game Selection" || sceneName == "MultiplayerRooms")
            {
                SceneManager.LoadScene("Mian Menu");
            }
            else if (sceneName == "Mian Menu")
            {
                if (Panel1 != null)
                {
                    Panel1.SetActive(true);
                }

            }
            else if (sceneName == "login screen" || sceneName == "Registraion")
            {
                if (Panel2 != null)
                {
                    Panel2.SetActive(true);
                }
                //Application.Quit();
            }
            else if(sceneName== "Game Selection Multiplayer")
            {
                SceneManager.LoadScene("Mian Menu");
            }
            else
            {
                SceneManager.LoadScene("Game Selection");
            }
            tap = 0;

        }
        else if (tap > 1)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;
            if (sceneName == "Mian Menu" || sceneName == "login screen" || sceneName == "Registraion")
            {
                // return;
            }
            else
            {
                if (Panel1 != null)
                {
                    Panel1.SetActive(true);
                }
            }

            tap = 0;

        }
    }
    public void exitgame()
    {
        print("Game will now exit in Built EXE");
        Application.Quit();
        // Panel2.SetActive (false);

    }
    public void cancelexit()
    {
        if (Panel2 != null)
        {

            Panel2.SetActive(false);
        }
    }

    public void exittologin()
    {
        SceneManager.LoadScene("login screen");
        Panel1.SetActive(false);
    }
    public void cancel()
    {
        if (Panel1 != null)
        {

            Panel1.SetActive(false);
        }
    }

    public void Hide()
    {

        if (image != null)
        {
            image.SetActive(false);
        }

    }

    void Start()
    {
        percentageText = GetComponent<Text>();

    }

    public void textUpdate(float value)
    {
        Debug.Log(value);
        value = value - 250;
        percentageText.text = Mathf.RoundToInt(value) + "%";
    }
    public void loginscreen()
    {
        SceneManager.LoadScene("login screen");
    }

    private void Update()
    {

    }
}