using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    private async void Awake()
    {
        await HandleTransition();
    }

    private async Task HandleTransition()
    {
        await FadeToBlack.Instance.FadeIn();
        await Task.Delay(5000);
        await FadeToBlack.Instance.FadeOut();
        SceneManager.LoadScene("SampleScene");
    }
}
