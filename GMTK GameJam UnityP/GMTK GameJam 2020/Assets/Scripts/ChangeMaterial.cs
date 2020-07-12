using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    public Material[] material;
    Renderer renderer;
    TrailRenderer trailRenderer;
    Light light;
    Color myColor;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        renderer.sharedMaterial = material[0];

        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.material = material[0];

        light = GetComponentInChildren<Light>();
        myColor = light.color;
        light.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy.change_state)
        {
            renderer.sharedMaterial = material[0];
            trailRenderer.material = material[0];
            light.color = myColor;
        }
        else
        {
            renderer.sharedMaterial = material[1];
            trailRenderer.material = material[1];
            light.color = Color.yellow;
        }
    }
}
