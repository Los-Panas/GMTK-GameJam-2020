using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);

        maxDistance += 1 + Time.deltaTime;

        if (maxDistance >= 200)
        {
            Destroy(this.gameObject);
        }
    }
}
