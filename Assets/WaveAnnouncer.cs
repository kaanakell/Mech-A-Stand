using UnityEngine;
using TMPro;
public class WaveAnnouncer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI waveText;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void DisplayText(string text)
    {
        waveText.text = text;
        animator.SetTrigger("ShowText");
    }
}
