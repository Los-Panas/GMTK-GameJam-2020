using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public void StartCorutineShake(float duration, float force)
    {
        StartCoroutine(Shake(duration, force));
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0.0f;
        while(elapsed < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
    }
    //public GameObject Cam;
    //private float shake_amount;
    //Vector3 camPos;


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Shake(0.1f, 0.4f);
    //    }

    //    camPos = Cam.transform.position;


    //}
    //public void Shake(float amount, float length)
    //{
    //    shake_amount = amount;
    //    camPos = new Vector3(Cam.transform.position.x, Cam.transform.position.y, Cam.transform.position.z);
    //    InvokeRepeating("BeginShake", 0, 0.01f);
    //    Invoke("StopShake", length);

    //}

    //private void BeginShake()
    //{
    //    float offset_y = Random.value * shake_amount * 2 - shake_amount;
    //    camPos.z = offset_y;

    //    Cam.transform.position = camPos;
    //}

    //private void StopShake()
    //{
    //    CancelInvoke("BeginShake");
    //}

}