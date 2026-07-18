using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartSender : MonoBehaviour
{
    public void SendToGameStart() => SceneManager.LoadScene("SampleScene");
}
