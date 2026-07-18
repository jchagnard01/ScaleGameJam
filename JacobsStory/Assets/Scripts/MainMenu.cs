using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public async void StartGame()
    {
        await FadeToBlack.Instance.FadeOut();
        SceneManager.LoadScene("OpenSceneTransition");
    }

    public void QuitToDesktop()
    {
        Debug.Log("Quit button has been hit!");
        Application.Quit();
    }
}
