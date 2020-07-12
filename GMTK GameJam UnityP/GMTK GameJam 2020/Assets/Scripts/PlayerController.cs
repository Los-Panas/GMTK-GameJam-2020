using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    Vector3 movement;
    float horizontal;
    float vertical;

    [SerializeField] float movementSpeed;
    public GameObject bulletSpawnPoint;
    [SerializeField] float waitTime;
    [SerializeField] GameObject bulletmesh;
    public int timeToFire;
    private int toFireTrack = 0;
    public bool ableToFire = true;
    [SerializeField] float bulletSpeed;
    [SerializeField] int minNumOfShots;
    [SerializeField] int maxNumfShots;
    private GameObject clone;
    public int health = 100;
    public int spawnTime;
    public bool invulnerability = false;
    public float invulnerability_time = 2f;
    public Transform Bulletspawn;

    public float dashCD;
    public float currentDashCD;
    public float dashDuration;
    public float dashSpeed;
    public float dashTimer;
    private bool dashDirection;

    Rigidbody body;

    private RoomManager rmAddPoints;

    private RandomPointOnMesh star002;
    private RandomPointOnMesh rectangle002;

    private RandomPointOnMesh star0021;
    private RandomPointOnMesh rectangle0021;

    public HealthBarHandler healthBarHandler;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        currentDashCD = dashCD;
        dashDirection = false;

        GameObject Walls = GameObject.Find("Walls");
        rmAddPoints = Walls.GetComponent<RoomManager>();

        //GameObject Star002 = GameObject.Find("Star002");
        //star002 = Star002.GetComponent<RandomPointOnMesh>();

        //GameObject Rectangle002 = Walls.transform.Find("Rectangle001").gameObject.transform.Find("Rectangle002").gameObject;
        //rectangle002 = Rectangle002.GetComponent<RandomPointOnMesh>();

        //GameObject Rectangle0021 = Walls.transform.Find("Rectangle001 (1)").gameObject.transform.Find("Rectangle002").gameObject;
        //rectangle0021 = Rectangle0021.GetComponent<RandomPointOnMesh>();

        //GameObject Star0021 = Walls.transform.Find("Star001 (1)").gameObject.transform.Find("Star002").gameObject;
        //star0021 = Star0021.GetComponent<RandomPointOnMesh>();
        healthBarHandler.SetMaxHealth(health);
    }

    void Update()
    {
        GetMoveInput();
        RotationInput();
        Shoot();
    }

    void GetMoveInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement = new Vector3(horizontal, 0, vertical);
    }

    void FixedUpdate()
    {
        transform.Translate(movement.normalized * movementSpeed * Time.deltaTime, Space.World);
        ImprovedDashFunc();
    }

    void ImprovedDashFunc()
    {
        currentDashCD -= Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftShift) && currentDashCD <= 0)
        {
            dashDirection = true;
        }

        if (dashDirection != false)
        {
            if (dashTimer >= dashDuration)
            {
                dashDirection = false;
                dashTimer = 0;
                body.velocity = Vector3.zero;
            }
            else
            {
                StartCoroutine(TrailToggle());
                dashTimer += Time.deltaTime;
                currentDashCD = dashCD;
                body.velocity = movement.normalized * dashSpeed;
            }
        }
    }

    IEnumerator TrailToggle()
    {
        GetComponent<TrailRenderer>().enabled = true;
        yield return new WaitForSeconds(dashDuration);
        GetComponent<TrailRenderer>().enabled = false;
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
        for (int i = 0; i <= Random.Range(minNumOfShots, maxNumfShots); i++)
        {
            clone = Instantiate(bulletmesh, Bulletspawn.position, Bulletspawn.rotation);
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        }       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            Destroy(other.gameObject);

            rmAddPoints.AddPoints();

            if(star002.gameObject.activeInHierarchy == true)
            {
                star002.bangGetPoint = true;
                Debug.Log("sphere1 true");
            }
            if (rectangle002.gameObject.activeInHierarchy == true)
            {
                rectangle002.bangGetPoint = true;
                Debug.Log("sphere2 true");
            }
            //if (star0021.gameObject.activeInHierarchy == true)
            //{
            //    star0021.bangGetPoint = true;
            //    Debug.Log("sphere1 true");
            //}
            //if (rectangle0021.gameObject.activeInHierarchy == true)
            //{
            //    rectangle0021.bangGetPoint = true;
            //    Debug.Log("sphere2 true");
            //}
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            if (!invulnerability)
            {
                health -= 10; //SHOULD HAVE A BULLET DAMAGE FOR NOW IS HARDCODED
                healthBarHandler.SetHealth(health);
                invulnerability = true;
                StartCoroutine(ImmuneTime(Time.realtimeSinceStartup));
            }
            Destroy(collision.gameObject);
        }
        
        if(health <= 0) 
        {
            Scene curr_scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curr_scene.name); // I dont know if we need to save some values or not but if we needed to we should store them somewhere before the reload.
        }
    }
    IEnumerator ImmuneTime(float time)
    {
        while ((Time.realtimeSinceStartup - time) < invulnerability_time)
        {
            yield return new WaitForEndOfFrame();
        }
        invulnerability = false;
    }
}
