using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject Player;

    public float smoothSpeed;
    public Vector3 offset;
    Vector3 playerPosition;

    void FixedUpdate()
    {
        switch (CameraLimits.hitDirection)
        {
            case CameraLimits.HitDirection.None:
                playerPosition = Player.transform.position + offset;
                break;
            case CameraLimits.HitDirection.Top:
                break;
            case CameraLimits.HitDirection.Bottom:
                break;
            case CameraLimits.HitDirection.Forward:
                playerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, CameraLimits.playerPositionOnEnter.z) + offset;
                break;
            case CameraLimits.HitDirection.Back:
                playerPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, CameraLimits.playerPositionOnEnter.z) + offset;
                break;
            case CameraLimits.HitDirection.Left:
                playerPosition = new Vector3(CameraLimits.playerPositionOnEnter.x, Player.transform.position.y, Player.transform.position.z) + offset;
                break;
            case CameraLimits.HitDirection.Right:
                playerPosition = new Vector3(CameraLimits.playerPositionOnEnter.x, Player.transform.position.y, Player.transform.position.z) + offset;
                break;
            case CameraLimits.HitDirection.Corner:
                playerPosition = new Vector3(CameraLimits.playerPositionOnEnter.x, Player.transform.position.y, CameraLimits.playerPositionOnEnter.z) + offset;
                break;
            default:
                break;
        }

        Vector3 smoothPosition = Vector3.Lerp(transform.position, playerPosition, smoothSpeed);
        transform.position = smoothPosition;
    }
}
