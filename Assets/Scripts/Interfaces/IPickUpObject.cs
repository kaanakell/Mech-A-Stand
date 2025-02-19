using UnityEngine;

public interface IPickUpObject
{
    public void OnPickUp(PlayerHealth playerHealth);
    public void OnPickUp(PlayerLevel playerlevel);
    public void OnPickUp(Coins coinPickUp);
}
