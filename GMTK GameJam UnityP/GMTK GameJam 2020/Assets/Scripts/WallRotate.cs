using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotate : MonoBehaviour
{
    public float ySpeed = 10.0f;

    public GameObject go;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(go.transform.position, Vector3.up, ySpeed * Time.deltaTime);

    }
}
