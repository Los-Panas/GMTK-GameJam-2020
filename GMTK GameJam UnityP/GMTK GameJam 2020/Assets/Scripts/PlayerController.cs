using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    public GameObject bulletSpawnPoint;
    [SerializeField] float waitTime;
    [SerializeField] GameObject bulletmesh;
    public int timeToFire;
    private int toFireTrack = 0;
    [SerializeField] bool ableToFire;
    [SerializeField] float bulletSpeed;
    private GameObject clone;
    public float health = 100f;
    public int spawnTime;

    public Transform Bulletspawn;

    void Update()
    {
        RotationInput();
        Shoot();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);
        transform.Translate(movement.normalized * movementSpeed * Time.deltaTime, Space.World);
    }

    void RotationInput()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    void Shoot()
    {
        toFireTrack++;

        if (toFireTrack == timeToFire && ableToFire)
        {
            toFireTrack = 0;
            InvokeRepeating ("Spawn", spawnTime, spawnTime);
            //bulletSpawned = Instantiate(bullet.transform, bulletSpawnPoint.transform.position, Quaternion.identity);
            //bulletSpawned.rotation = bulletSpawnPoint.transform.rotation;
        } else if (!ableToFire)
        {
            toFireTrack = 0;
        }
    }

    void Spawn()
    {
        clone = Instantiate (bulletmesh, Bulletspawn.position, Bulletspawn.rotation);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            health -= 10f; //SHOULD HAVE A BULLET DAMAGE FOR NOW IS HARDCODED
            Destroy(collision.gameObject);
        }
        
        if(health <= 0) 
        {
            Scene curr_scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curr_scene.name); // I dont know if we need to save some values or not but if we needed to we should store them somewhere before the reload.
        }
    }
}
