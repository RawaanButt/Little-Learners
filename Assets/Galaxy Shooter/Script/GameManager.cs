using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public bool gameOver = true;
    public GameObject player;

    private UIManager _uIManager;
    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, new Vector3(0, 4, 0), Quaternion.identity);
                gameOver = false;
                _uIManager.hideTitleScreen();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene("Mian Menu");
        }
    }
}
