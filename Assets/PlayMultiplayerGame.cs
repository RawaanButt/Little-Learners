using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMultiplayerGame : MonoBehaviour
{
    public void PlayMultiplayer()
    {
        SceneManager.LoadScene("MultiplayerRooms");
    }
}
