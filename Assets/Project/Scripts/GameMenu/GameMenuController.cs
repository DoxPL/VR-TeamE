using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OnMultiplayerSelected()
    {
        SceneManager.LoadScene("MultiplayerScene");
    }
}
