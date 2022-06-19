using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOptionMultiplayer : MonoBehaviour
{


    public void Matching()
    {

        SceneManager.LoadScene("NewMultiplayer");
    }

    public void Sorting()
    {

        SceneManager.LoadScene("game2l1Multiplayer");
    }

    public void Classification()
    {

        SceneManager.LoadScene("game3l1Multiplayer");
    }


    public void Emotions()
    {

        SceneManager.LoadScene("game6l1Multiplayer");
    }


    public void Adjective()
    {

        SceneManager.LoadScene("game5l1Multiplayer");
    }

    public void Selection()
    {

        SceneManager.LoadScene("game4l1Multiplayer");
    }
    public void Association()
    {

        SceneManager.LoadScene("game7l1Multiplayer");
    }





}
