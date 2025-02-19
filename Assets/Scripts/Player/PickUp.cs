using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            GetComponent<IPickUpObject>().OnPickUp(playerHealth);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Coin_Get");
            Destroy(gameObject);
        }

        PlayerLevel playerLevel = collision.GetComponent<PlayerLevel>();
        if (playerLevel != null)
        {
            GetComponent<IPickUpObject>().OnPickUp(playerLevel);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Coin_Get");
            Destroy(gameObject);
        }

        Coins coinPickUp = collision.GetComponent<Coins>();
        if (coinPickUp != null)
        {
            GetComponent<IPickUpObject>().OnPickUp(coinPickUp);
            Destroy(gameObject);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Coin_Get");
        }
    }
}

