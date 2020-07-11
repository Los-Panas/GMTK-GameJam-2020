using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovedDash : MonoBehaviour
{
    Rigidbody body;
    public float speed;
    public float dashSpeed;
    public float dashDuration;
    public float dashTimer;
    public float dashCooldown;
    public float currentCooldown;

    private float vertical;
    private float horizontal;
    private Vector3 movement;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        currentCooldown = dashCooldown;
    }

    void Update()
    {
        GetInputs();
    }

    void GetInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
    }

    void ImprovedDashFunc()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            body.velocity = movement * dashSpeed;
        }
    }
}
