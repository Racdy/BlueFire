using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneBehaviour : MonoBehaviour
{
    public Animator droneAnimator;
    public NavMeshAgent agent;
    private Transform playerTransform;

    public DroneState droneState;

    public Vector2 patrolArea;
    private Vector3 targetPoint;

    private bool enableToShoot;
    public bool enableIK;

    public GameObject pointer;
    public GameObject droneBulletPrefab;

    public float bulletForce;

    public EnemyLife enemyLife;

    public ParticleSystem spawnParticles;

    public GameObject[] weaponDropped;

    public bool dropped;

    // Start is called before the first frame update

    private void OnEnable()
    {
        spawnParticles.Play();
    }
    void Start()
    {
        enemyLife = GetComponent<EnemyLife>();
        droneAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        droneState = DroneState.PATROL;

        InvokeRepeating("GeneratePatrolPosition", 0f,5f);
        InvokeRepeating("StateManager",0f, 0.5f);
        InvokeRepeating("HitState", 0f,0.5f);
        InvokeRepeating("FollowState", 0f, 0.5f);

        enableToShoot=true;

        dropped = false;
    }

    void GeneratePatrolPosition()
    {
        if (droneState != DroneState.PATROL)
            return;

        enableIK = false;
        agent.speed = 2f;
        agent.stoppingDistance = 0f;
        float patroX = Random.Range(-patrolArea.x, patrolArea.x);
        float patroY = Random.Range(-patrolArea.x, patrolArea.x);
        targetPoint = transform.position + Vector3.right * patroX + Vector3.forward * patroY;
    }

    void StateManager()
    {
       float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < 20 && distanceToPlayer > 3)
        {
            if (enableToShoot)
                StartCoroutine("ShootState");
        }

        if (distanceToPlayer < 2)
            droneState = DroneState.HIT;
        else if (distanceToPlayer < 10)
            StartCoroutine("StayState");
        else if (distanceToPlayer < 30 || distanceToPlayer < 7)
            droneState = DroneState.FOLLOW;
        else
            droneState = DroneState.PATROL;
    }

    void HitState()
    {
        if (droneState != DroneState.HIT)
            return;

        transform.LookAt(playerTransform);

        enableIK = false;
        droneAnimator.SetTrigger("Hit");
    }

    IEnumerator StayState()
    {
        agent.speed = 4f;
        float patroX = Random.Range(-patrolArea.x, patrolArea.x);
        float patroY = Random.Range(-patrolArea.x, patrolArea.x);
        targetPoint = transform.position + Vector3.right * patroX + Vector3.forward * patroY;
        transform.LookAt(playerTransform);

        yield return new WaitForSeconds(2f);
        //droneState = DroneState.PATROL;
    }

    IEnumerator ShootState()
    {
        enableIK = true;
        enableToShoot = false;
        Shot();
        yield return new WaitForSeconds(0.5f);
        droneAnimator.SetBool("Attack", false);
        enableToShoot = true;
    }

    void FollowState()
    {
        if (droneState != DroneState.FOLLOW)
            return;

        enableIK = true;
        agent.speed = 6f;
        agent.stoppingDistance = 2f;
        targetPoint = playerTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        agent.SetDestination(targetPoint);
        droneAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        if (enemyLife.isDead)
        {
            targetPoint = transform.position;
            StopAllCoroutines();
            CancelInvoke();
            if(!dropped)
                GetDrop();
        }
    }

    public enum DroneState
    {
        PATROL,
        FOLLOW,
        SHOOT,
        HIT,
        STAY,
        DEATH
    }

    void Shot()
    {
        Ray rayShoot = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(rayShoot, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = rayShoot.GetPoint(75);
        }

        droneAnimator.SetBool("Attack", true);

        //GameObject BulletTMP = Instantiate(Bullet, pointer.transform.position, Quaternion.identity);
        //GameObject BulletTMP = BulletPool.Instance.RequestBullet();
        GameObject BulletTMP = ObjectPool.Instance.GetGameObjectOfType(droneBulletPrefab.name, true);

        BulletTMP.transform.position = pointer.transform.position;
        BulletTMP.transform.rotation = Quaternion.identity;
        BulletTMP.transform.forward =  transform.forward;
        BulletTMP.SetActive(true);
        BulletTMP.GetComponent<Rigidbody>().velocity = Vector3.zero;
        BulletTMP.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        BulletTMP.GetComponent<Rigidbody>().AddForce((targetPoint - pointer.transform.position).normalized *bulletForce, ForceMode.Impulse);
    }

    public void GetDrop()
    {
        dropped = true;
        int randomWeapon = Random.Range(0, 2);
        int randomChance = Random.Range(0, 4);
        if (randomChance == 1)
        {
            GameObject item = Instantiate(weaponDropped[randomWeapon], transform.position, Quaternion.identity);
            item.transform.position = transform.position;
        }
    }

}
