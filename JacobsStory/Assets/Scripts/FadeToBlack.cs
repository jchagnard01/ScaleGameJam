using System.Threading.Tasks;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    public static FadeToBlack Instance;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeDuration = 0.5f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public async Task FadeIn() => await Fade(0);
    public async Task FadeOut() => await Fade(1);

    private async Task Fade(float targetTransparency)
    {
        float start = canvasGroup.alpha;
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, targetTransparency, time / fadeDuration);
            await Task.Yield();
        }
        canvasGroup.alpha = targetTransparency;
    }
}
