using UnityEngine;

public class PlayerWinManager : MonoBehaviour
{
    [SerializeField] GameObject winMessagePanel;
    PauseManager pauseManager;
    [SerializeField] DataContainer dataContainer;
    [SerializeField] FlagsTable flagsTable;

    void Start()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    public void Win(string stageID)
    {
        FMODUnity.RuntimeManager.PlayOneShot("Command_FootstepEND");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Command_STOP_AllMusic");
        FMODUnity.RuntimeManager.PlayOneShot("event:/Music_WIN");
        winMessagePanel.SetActive(true);
        pauseManager.PauseGame();
        Flag flag= flagsTable.GetFlag(stageID);
        if(flag != null)
        {
            flag.state = true;
        }
    }
}
