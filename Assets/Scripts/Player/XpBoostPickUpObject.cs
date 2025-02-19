using UnityEngine;

public class XpBoostPickUpObject : MonoBehaviour, IPickUpObject
{
    [SerializeField]int amount;
    public void OnPickUp(PlayerLevel playerLevel)
    {
        playerLevel.AddExperience(amount);
    }

    public void OnPickUp(PlayerHealth playerHealth)
    {
        
    }

    public void OnPickUp(Coins coinPickUp)
    {
        
    }
}
