using UnityEngine;

public class RoomCompletion : MonoBehaviour
{
    [SerializeField] float timeToCompleteRoom;

    StageTimer stageTimer;
    PauseManager pauseManager;
    //RoomManager roomManager;

    [SerializeField] RoomClearedPanel roomCompletePanel;

    private void Start()
    {
        stageTimer = GetComponent<StageTimer>();
        pauseManager = FindAnyObjectByType<PauseManager>();
        roomCompletePanel = FindAnyObjectByType<RoomClearedPanel>(FindObjectsInactive.Include);
        //roomManager = GetComponent<RoomManager>();
    }

    public void Update()
    {
        if(stageTimer.time > timeToCompleteRoom)
        {
            pauseManager.PauseGame();
            roomCompletePanel.gameObject.SetActive(true);
            //roomManager.OpenNextRoomDoor();
        }
    }
}
