using UnityEngine;

public class CoinPickUp : MonoBehaviour, IPickUpObject
{
    [SerializeField]int count;
    public void OnPickUp(Coins coinPickUp)
    {
        coinPickUp.Add(count);
    }
    public void OnPickUp(PlayerHealth playerHealth)
    {
        
    }

    public void OnPickUp(PlayerLevel playerlevel)
    {
        
    }

    
}
