using System.Collections.Generic;
using UnityEngine;

public class DropOnDestroy : MonoBehaviour
{
    [SerializeField] List<GameObject> dropItemPrefab;
    [SerializeField] [Range(0f, 1f)] float chance = 1f;

    bool isQuitting = false;

    private void OnApplicationQuit()
    {
        isQuitting = true;
    }

    public void CheckDrop()
    {

        if(isQuitting || dropItemPrefab.Count <= 0)
        {
            return;
        }
        
        if(Random.value < chance)
        {
            GameObject toDrop = dropItemPrefab[Random.Range(0, dropItemPrefab.Count)];

            if(toDrop == null)
            {
                Debug.LogWarning("DropOnDestroy, reference to dropped item is null");
            }

            SpawnManager.instance.SpawnObject(transform.position, toDrop);
        }
    }
}
