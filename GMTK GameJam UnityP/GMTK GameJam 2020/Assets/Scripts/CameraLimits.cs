using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLimits : MonoBehaviour
{
    public enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right, Corner}
    public static HitDirection hitDirection = HitDirection.None;
    public static Vector3 playerPositionOnEnter;

    private void Start()
    {
        playerPositionOnEnter = this.gameObject.transform.position;

    }
    private HitDirection ReturnDirection(GameObject Object, GameObject ObjectHit)
    {

        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {

            if (MyRayHit.collider != null)
            {
                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (hitDirection == HitDirection.Back || hitDirection == HitDirection.Forward || hitDirection == HitDirection.Right || hitDirection == HitDirection.Left)
                {
                    hitDirection = HitDirection.Corner;

                    if (MyNormal == MyRayHit.transform.forward)
                        playerPositionOnEnter.z = this.gameObject.transform.position.z;

                    if (MyNormal == -MyRayHit.transform.forward)
                        playerPositionOnEnter.z = this.gameObject.transform.position.z;

                    if (MyNormal == MyRayHit.transform.right)
                        playerPositionOnEnter.x = this.gameObject.transform.position.x;

                    if (MyNormal == -MyRayHit.transform.right)
                        playerPositionOnEnter.x = this.gameObject.transform.position.x;
                }
                else
                {
                    if (MyNormal == MyRayHit.transform.forward)
                    {
                        hitDirection = HitDirection.Forward;
                        playerPositionOnEnter.z = this.gameObject.transform.position.z;
                    }
                    if (MyNormal == -MyRayHit.transform.forward)
                    {
                        hitDirection = HitDirection.Back;
                        playerPositionOnEnter.z = this.gameObject.transform.position.z;
                    }
                    if (MyNormal == MyRayHit.transform.right)
                    {
                        hitDirection = HitDirection.Right;
                        playerPositionOnEnter.x = this.gameObject.transform.position.x;
                    }
                    if (MyNormal == -MyRayHit.transform.right)
                    {
                        hitDirection = HitDirection.Left;
                        playerPositionOnEnter.x = this.gameObject.transform.position.x;
                    }
                }
               
            }
        }
        return hitDirection;
    }

    private HitDirection ReturnDirectionOnly(GameObject Object, GameObject ObjectHit)
    {

        RaycastHit MyRayHit;
        Vector3 direction = (Object.transform.position - ObjectHit.transform.position).normalized;
        Ray MyRay = new Ray(ObjectHit.transform.position, direction);

        if (Physics.Raycast(MyRay, out MyRayHit))
        {
            if (MyRayHit.collider != null)
            {
                Vector3 MyNormal = MyRayHit.normal;
                MyNormal = MyRayHit.transform.TransformDirection(MyNormal);

                if (MyNormal == MyRayHit.transform.forward)hitDirection = HitDirection.Right;
                if (MyNormal == -MyRayHit.transform.forward)hitDirection = HitDirection.Left;
                if (MyNormal == MyRayHit.transform.right)hitDirection = HitDirection.Forward;
                if (MyNormal == -MyRayHit.transform.right) hitDirection = HitDirection.Back;
                             
            }
        }
        return hitDirection;
    }



    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(ReturnDirection(collision.gameObject, this.gameObject));
    }
    void OnCollisionExit(Collision collision)
    {
        if (hitDirection == HitDirection.Corner)
        {
            ReturnDirectionOnly(collision.gameObject, this.gameObject);
        }
        else
            hitDirection = HitDirection.None;
    }
}
