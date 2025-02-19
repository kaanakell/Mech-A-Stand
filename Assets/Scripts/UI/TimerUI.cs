using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
