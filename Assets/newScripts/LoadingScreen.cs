using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void nextscene(string LevelName)
    {
        SceneManager.LoadScene("New", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
