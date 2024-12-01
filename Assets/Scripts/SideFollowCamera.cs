using UnityEngine;

public class SideFollowCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(5, 2, 0);
    public float smoothSpeed = 0.125f;

    public float maxHeight = 10f;  
    public float minHeight = 1f; 

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minHeight, maxHeight);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
