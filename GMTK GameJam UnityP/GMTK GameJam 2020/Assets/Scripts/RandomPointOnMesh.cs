using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomPointOnMesh : MonoBehaviour
{
    private MeshCollider lookupCollider;

    public bool bangGetPoint = false;
    private Vector3 randomPoint;

    public List<Vector3> debugPoints;

    GameObject sphere;

    RoomManager roomManager;

    public bool callOnce = false;
    Scene scene;
    private void Start()
    {
        lookupCollider = GetComponent<MeshCollider>();
        GameObject Walls = GameObject.Find("Walls");
        roomManager = Walls.GetComponent<RoomManager>();
        bangGetPoint = true;

        scene = SceneManager.GetActiveScene();
    }

    
    void Update()
    {
        if (callOnce == false)
        {
            bangGetPoint = true;
            callOnce = true;
        }

        if (bangGetPoint)
        {
            Vector3 randomPoint = GetRandomPointOnMesh(lookupCollider.sharedMesh) * 0.3f;
            randomPoint += lookupCollider.transform.position;

            //UGLIEST CODE ALIVE -- PROTECT YOUR EYES
            sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = new Vector3(randomPoint.x, 1f, randomPoint.z);
            //sphere.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
            sphere.tag = "PickUp";
            WallRotate wrScript = sphere.AddComponent<WallRotate>();
            wrScript.ySpeed = -10.0f;
            sphere.GetComponent<Collider>().isTrigger = true;

            if(roomManager.points == roomManager.maxPoints - roomManager.spherePoints)
            {
                Material newMat = Resources.Load("Collectable", typeof(Material)) as Material;
                sphere.GetComponent<Renderer>().material = newMat;
                Color color = new Color(0, 0, 1, 1);
                sphere.GetComponent<Renderer>().material.SetColor("_EmissionColor", color);

            }
            else
            {
                Material newMat = Resources.Load("Collectable", typeof(Material)) as Material;
                sphere.GetComponent<Renderer>().material = newMat;
            }
            

            bangGetPoint = false;

        }
    }

    Vector3 GetRandomPointOnMesh(Mesh mesh)
    {
        //if you're repeatedly doing this on a single mesh, you'll likely want to cache cumulativeSizes and total
        float[] sizes = GetTriSizes(mesh.triangles, mesh.vertices);
        float[] cumulativeSizes = new float[sizes.Length];
        float total = 0;

        for (int i = 0; i < sizes.Length; i++)
        {
            total += sizes[i];
            cumulativeSizes[i] = total;
        }

        //so everything above this point wants to be factored out

        float randomsample = Random.value * total;

        int triIndex = -1;

        for (int i = 0; i < sizes.Length; i++)
        {
            if (randomsample <= cumulativeSizes[i])
            {
                triIndex = i;
                break;
            }
        }

        if (triIndex == -1) Debug.LogError("triIndex should never be -1");

        Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
        Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
        Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

        //generate random barycentric coordinates

        float r = Random.value;
        float s = Random.value;

        if (r + s >= 1)
        {
            r = 1 - r;
            s = 1 - s;
        }
        //and then turn them back to a Vector3
        Vector3 pointOnMesh = a + r * (b - a) + s * (c - a);

        float x = pointOnMesh.x;
        float y = pointOnMesh.y;
        float z = pointOnMesh.z;
        pointOnMesh.x = x;
        pointOnMesh.y = 4.0f;
        pointOnMesh.z = y;

        return pointOnMesh;

    }

    float[] GetTriSizes(int[] tris, Vector3[] verts)
    {
        int triCount = tris.Length / 3;
        float[] sizes = new float[triCount];
        for (int i = 0; i < triCount; i++)
        {
            sizes[i] = .5f * Vector3.Cross(verts[tris[i * 3 + 1]] - verts[tris[i * 3]], verts[tris[i * 3 + 2]] - verts[tris[i * 3]]).magnitude;
        }
        return sizes;
    }
}
