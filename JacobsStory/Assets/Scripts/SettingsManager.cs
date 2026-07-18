using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void ToggleFullScreen(bool isFullscreen) => Screen.fullScreen = isFullscreen;
    public void AdjustGraphicQuality(int qualityLevel) => QualitySettings.SetQualityLevel(qualityLevel);
}
