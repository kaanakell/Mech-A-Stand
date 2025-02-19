/* using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private FMOD.Studio.EventInstance musicInstance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/mainThemeFINAL");
            musicInstance.start();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }
    }
} */
