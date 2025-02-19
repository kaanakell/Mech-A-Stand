using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    [SerializeField] GameObject weaponParentObject;

    public void GameOver()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Command_FootstepEND");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Command_STOP_AllMusic");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Music_FAIL");
        Debug.Log("Game Over");
        GetComponent<PlayerMovement>().enabled = false;
        Destroy(gameObject);
        gameOverPanel.SetActive(true);
        weaponParentObject.SetActive(false);
    }
}
