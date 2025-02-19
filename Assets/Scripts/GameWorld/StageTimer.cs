using UnityEngine;

public class StageTimer : MonoBehaviour
{
    public float time;
    TimerUI timerUI;
    public float maxuUiTime = 120f;
    public float uiTime;
    private void Awake()
    {
        timerUI = FindAnyObjectByType<TimerUI>();
        uiTime = maxuUiTime;
    }

    void Update()
    {
        time += Time.deltaTime;
        //timerUI.UpdateTime(time);
        uiTime -= Time.deltaTime;
        if (uiTime <= 0)
        {
            uiTime = maxuUiTime;
        }
        timerUI.UpdateTime(uiTime);

    }
}
