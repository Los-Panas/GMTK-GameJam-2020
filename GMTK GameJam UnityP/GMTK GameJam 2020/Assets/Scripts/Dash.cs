using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    enum DashDirection
    {
        Left,
        Right,
        Up,
        Down,
        NoDirection
    }

    Rigidbody body;
    public float speed;
    public float dashSpeed;
    private DashDirection dashDirection;
    public float dashDuration;
    public float dashTimer;
    public float dashCooldown;
    public float currentCooldown;


    void Start()
    {
        body = GetComponent<Rigidbody>();
        dashDirection = DashDirection.NoDirection;
        currentCooldown = dashCooldown;
    }

    private void Update()
    {
        DashFunc();
    }

    void DashFunc()
    {
        //body.velocity = Vector3.zero;
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDirection = DashDirection.Down;
        }
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDirection = DashDirection.Up;
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDirection = DashDirection.Left;
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashDirection = DashDirection.Right;
        }

        if (dashDirection != DashDirection.NoDirection)
        {
            if (dashTimer >= dashDuration)
            {
                dashDirection = DashDirection.NoDirection;
                dashTimer = 0;
                body.velocity = Vector3.zero;
            }
            else
            {
                dashTimer += Time.deltaTime;
                if (dashDirection == DashDirection.Left)
                {
                    body.velocity = Vector3.left * dashSpeed;
                }

                if (dashDirection == DashDirection.Right)
                {
                    body.velocity = Vector3.right * dashSpeed;
                }

                if (dashDirection == DashDirection.Up)
                {
                    body.velocity = Vector3.forward * dashSpeed;
                }
                if (dashDirection == DashDirection.Down)
                {
                    body.velocity = -Vector3.forward * dashSpeed;
                }
            }
        }
    }
}
