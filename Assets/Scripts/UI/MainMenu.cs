using Unity.VisualScripting;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    PauseManager pauseManager;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!panel.activeInHierarchy)
            {
                OpenMenu();
            }
            else
            {
                CloseMenu();
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI_NO");
            }
        }
    }

    public void CloseMenu()
    {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_NO");        
    }

    public void OpenMenu()
    {
        pauseManager.PauseGame();
        panel.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI_YES");
    }
}
