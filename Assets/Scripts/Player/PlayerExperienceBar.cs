using UnityEngine;
using UnityEngine.UI;

public class PlayerExperienceBar : MonoBehaviour
{
    [SerializeField]Slider slider;
    [SerializeField]TMPro.TextMeshProUGUI levelText;

    public void UpdateExperienceSlider(int currentExperience, int targetExperience)
    {
        slider.maxValue = targetExperience;
        slider.value = currentExperience;
    }

    public void SetLevelText(int level)
    {
        levelText.text = "LEVEL: " + level.ToString();
    }
}
