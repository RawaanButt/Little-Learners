using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] life;
    public Image imgDisplay;
    public Text scoreText;
    public Text powerText;
    public int score;
    public GameObject titleScreen;
    public void updateLives(int currentLives)
    {
        imgDisplay.sprite = life[currentLives];
    }

    public void updateScore() 
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void PowerGain(string power)
    {
        powerText.enabled = true;
        powerText.text = power + " Power Up";

    }
    public void powerEmpty()
    {
        powerText.enabled = false;

    }
    public void showTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void hideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: ";

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
