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
    public int maxHealth = 100;
    public int currentHealth;
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
    private RandomPointOnMesh pentagon002;
    private RandomPointOnMesh hexagon002;
    private RandomPointOnMesh heptagon002;

    private int count;

    public HealthBarHandler healthBarHandler;

    public ParticleSystem particleSys;

    public Material regularMaterial;
    public Material lightMaterial;
    public Material hurtMaterial;
    public Material lightHurtMaterial;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        currentDashCD = dashCD;
        dashDirection = false;

        count = 0;

        GameObject Walls = GameObject.Find("Walls");
        rmAddPoints = Walls.GetComponent<RoomManager>();

        GameObject Star002 = GameObject.Find("Star002");
        star002 = Star002.GetComponent<RandomPointOnMesh>();

        GameObject Rectangle002 = Walls.transform.Find("Rectangle001").gameObject.transform.Find("Rectangle002").gameObject;
        rectangle002 = Rectangle002.GetComponent<RandomPointOnMesh>();

        GameObject Pentagon002 = Walls.transform.Find("Pentagon001").gameObject.transform.Find("Pentagon002").gameObject;
        pentagon002 = Pentagon002.GetComponent<RandomPointOnMesh>();

        GameObject Hexagon002 = Walls.transform.Find("Hexagon001").gameObject.transform.Find("Hexagon002").gameObject;
        hexagon002 = Hexagon002.GetComponent<RandomPointOnMesh>();

        GameObject Heptagon002 = Walls.transform.Find("Heptagon001").gameObject.transform.Find("Heptagon002").gameObject;
        heptagon002 = Heptagon002.GetComponent<RandomPointOnMesh>();

        healthBarHandler.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
    }

    void Update()
    {
        GetMoveInput();
        RotationInput();
        Shoot();
        healthBarHandler.SetHealth(currentHealth);
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
            FindObjectOfType<AudioManager>().Play("Dash");

        }

        if (currentDashCD <= 0)
        {
            if (invulnerability)
            {
                GetComponent<MeshRenderer>().material = hurtMaterial;
            }
            else
            {
                GetComponent<MeshRenderer>().material = regularMaterial;
            }
        }
        else
        {
            if (invulnerability)
            {
                GetComponent<MeshRenderer>().material = lightHurtMaterial;
            }
            else
            {
                GetComponent<MeshRenderer>().material = lightMaterial;
            }
        }

        if (dashDirection)
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
        particleSys.Emit(30);
        FindObjectOfType<AudioManager>().Play("Shot");


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            FindObjectOfType<AudioManager>().Play("Coin");

            Destroy(other.gameObject);

            rmAddPoints.AddPoints();

            //TODO: UI WORLD (+ ROOMMANAGER.SPHEREPOINTS)

            if (star002.gameObject.activeInHierarchy == true)
            {
                star002.bangGetPoint = true;
                //Debug.Log("sphere1 true");
            }
            if (rectangle002.gameObject.activeInHierarchy == true)
            {
                rectangle002.bangGetPoint = true;
                if(rectangle002.bangGetPoint == true)
                {
                    count = count + 1;
                }
                if(count >= 2)
                {
                    pentagon002.bangGetPoint = true;
                    count = 0;
                }
                maxNumfShots = 1;
                bulletSpeed = 500;

                //Debug.Log("sphere2 true");
            }
            if (pentagon002.gameObject.activeInHierarchy == true)
            {
                pentagon002.bangGetPoint = true;
                //Debug.Log("sphere3 true");
                if (pentagon002.bangGetPoint == true)
                {
                    count = count + 1;
                }
                if (count >= 2)
                {
                    hexagon002.bangGetPoint = true;
                    count = 0;
                }
                maxNumfShots = 2;
                minNumOfShots = 2;
                bulletSpeed = 500;
            }
            if (hexagon002.gameObject.activeInHierarchy == true)
            {
                hexagon002.bangGetPoint = true;
                if (hexagon002.bangGetPoint == true)
                {
                    count = count + 1;
                }
                if (count >= 2)
                {
                    heptagon002.bangGetPoint = true;
                    count = 0;
                }
                //Debug.Log("sphere4 true");
               
            }
            if (heptagon002.gameObject.activeInHierarchy == true)
            {
                heptagon002.bangGetPoint = true;
                //Debug.Log("sphere5 true");
                maxNumfShots = 2;
                minNumOfShots = 3;
                bulletSpeed = 600;
                

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            if (!invulnerability)
            {
                currentHealth -= 10; //SHOULD HAVE A BULLET DAMAGE FOR NOW IS HARDCODED
                FindObjectOfType<CameraShake>().StartCorutineShake();
                FindObjectOfType<AudioManager>().Play("Gethit");
                invulnerability = true;
                StartCoroutine(ImmuneTime(Time.realtimeSinceStartup));
            }
            Destroy(collision.gameObject);
        }
        
        if(currentHealth <= 0) 
        {
            FindObjectOfType<AudioManager>().Play("Restart");
            Scene curr_scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(curr_scene.name); // I dont know if we need to save some values or not but if we needed to we should store them somewhere before the reload.
        }
    }

    public void HealthToMax()
    {
        currentHealth = maxHealth;
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
