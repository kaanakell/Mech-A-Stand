using UnityEngine;

public class HealPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField] int healAmount;
    public void OnPickUp(PlayerHealth playerHealth)
    {
        playerHealth.Heal(healAmount);
    }

    public void OnPickUp(PlayerLevel playerlevel)
    {
        
    }

    public void OnPickUp(Coins coinPickUp)
    {
       
    }
}
