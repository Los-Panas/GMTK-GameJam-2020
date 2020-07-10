using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject Player;

    public float smoothSpeed;
    public Vector3 offset;


    void FixedUpdate()
    {
        if(!CameraLimits.stop_camera)
        {
            Vector3 desiredPosition = Player.transform.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothPosition;
        }
    }
}
