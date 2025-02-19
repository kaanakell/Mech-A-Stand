using UnityEngine;

public class CoinsText : MonoBehaviour
{
    [SerializeField]DataContainer dataContainer;
    TMPro.TextMeshProUGUI coinText;

    void Start()
    {
        coinText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        coinText.text = "Coins:" + dataContainer.coins.ToString();
    }
}
