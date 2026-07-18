using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSender : MonoBehaviour
{
    public void SendToMainMenu() => SceneManager.LoadScene("MainMenuScene");
}
