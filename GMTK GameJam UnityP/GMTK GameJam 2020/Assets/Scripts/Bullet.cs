using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{/*
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance;
    public Vector3 newDirection;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);

        maxDistance += 1 + Time.deltaTime;

        if (maxDistance >= 200)
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Ha colisiona");

       if (other.gameObject.CompareTag("Wall"))
       {
           Debug.Log("Es una pared");
           Vector3 wallNormal = other.contacts[0].normal;
           newDirection = Vector3.Reflect(this.gameObject.transform.position,wallNormal);
       } 
   } */

   public GameObject bulletmesh;
   private GameObject clone;
   public int spawnTime;
   private Rigidbody DishRigidbody;
   [SerializeField] float bulletSpeed;
   public Transform Bulletspawn;


    void Start()
    {
        //Bulletspawn = GetComponent<PlayerController>().bulletSpawned;
        //spawnTime = GetComponent<PlayerController>().timeToFire;

        //InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }
    
    /*void Spawn()
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        clone = Instantiate (bulletmesh, Bulletspawn.position, Bulletspawn.rotation);
        clone.GetComponent<Rigidbody>().AddForce(Vector3.forward * Time.deltaTime * bulletSpeed, ForceMode.Impulse);
    }*/

}
