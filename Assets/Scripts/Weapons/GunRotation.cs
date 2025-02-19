using UnityEngine;

public class GunRotation : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (playerTransform.localScale.x < 0)
        {
            angle = angle - 180f;
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);

    }
}
