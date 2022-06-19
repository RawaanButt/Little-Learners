using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOption : MonoBehaviour
{


    public void Matching()
    {

        SceneManager.LoadScene("New");
    }

    public void Sorting()
    {

        SceneManager.LoadScene("game2l1");
    }

    public void Classification()
    {

        SceneManager.LoadScene("game3l1");
    }


    public void Emotions()
    {

        SceneManager.LoadScene("game6l1");
    }


    public void Adjective()
    {

        SceneManager.LoadScene("game5l1");
    }

    public void Selection()
    {

        SceneManager.LoadScene("game4l1");
    }
    public void Association()
    {

        SceneManager.LoadScene("game7l1");
    }





}
