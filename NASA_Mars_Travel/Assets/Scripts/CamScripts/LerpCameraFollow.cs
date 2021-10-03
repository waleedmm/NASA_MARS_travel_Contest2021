using UnityEngine;
//ref:https://forum.unity.com/threads/third-person-camera.897767/
public class LerpCameraFollow : MonoBehaviour

{
    public Transform target;

    public float smoothSpeed = 1f;
    public Vector3 offset;
    public bool m_ForceLookAt = false;

    void Start()
    {
        offset =  this.transform.position - target.transform.position ;    
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        if (m_ForceLookAt)
        {
            transform.LookAt(target);
        }
    }   
}
