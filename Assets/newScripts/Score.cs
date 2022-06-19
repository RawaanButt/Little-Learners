using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    // Start is called before the first frame update

    public int scorevar = 0;
    // public GameObject scoreText;
    public Text newText;
    void Start()
    {
        scorevar = 0;
        Debug.Log(scorevar);

        // newText = scoreText.AddComponent<Text>();
    }
    public void increment()
    {
        scorevar = scorevar + 2;
        Debug.Log(scorevar);


    }

    // Update is called once per frame
    void Update()
    {

        newText.text = "" + scorevar;

        // newText.text = scorevar.ToString();

    }

}
