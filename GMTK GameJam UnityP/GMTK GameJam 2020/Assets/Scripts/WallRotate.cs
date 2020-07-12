using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotate : MonoBehaviour
{
    public float ySpeed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, ySpeed * Time.deltaTime);

    }
}
