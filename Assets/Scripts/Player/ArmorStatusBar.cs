using UnityEngine;

public class ArmorStatusBar : MonoBehaviour
{
    [SerializeField] Transform bar;

    public void SetState(float current, float max)
    {
        float state = (float)current;
        if(max == 0)
        {
            state = 0f;
        }
        else
        {
            state /= max;
        }

        
        if (state < 0f)
        {
            state = 0f;
        }
        bar.transform.localScale = new Vector3(state, 1f, 1f);
    }
}
