using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SentinelBehaviour : MonoBehaviour
{
    public Animator sentinelAnimator;
    public NavMeshAgent agent;

    private Transform playerTransform;

    public SentinelState sentinelState;

    public Vector2 patrolArea;
    private Vector3 targetPoint;

    public GameObject pointer;
    public GameObject sentinelBulletPrefab;

    public float bulletForce;
    private bool enableToShoot;

    public EnemyLife enemyLife;

    public ParticleSystem spawnParticles;

    private void OnEnable()
    {
        spawnParticles.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyLife = GetComponent<EnemyLife>();
        sentinelAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        sentinelState = SentinelState.PATROL;

        InvokeRepeating("GeneratePatrolPosition", 0f, 5f);
        InvokeRepeating("StateManager", 0f, 0.5f);
        InvokeRepeating("FollowState", 0f, 0.5f);

        enableToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetPoint);
        sentinelAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        if (enemyLife.isDead)
        {
            targetPoint= transform.position;
            StopAllCoroutines();
            CancelInvoke();
        }
    }

    public enum SentinelState
    {
        PATROL,
        FOLLOW,
        SHOOT,
        DEATH
    }

    void GeneratePatrolPosition()
    {
        if (sentinelState != SentinelState.PATROL)
            return;

        agent.speed = 2f;
        agent.stoppingDistance = 0f;
        float patroX = Random.Range(-patrolArea.x, patrolArea.x);
        float patroY = Random.Range(-patrolArea.x, patrolArea.x);
        targetPoint = transform.position + Vector3.right * patroX + Vector3.forward * patroY;
    }

    void StateManager()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < 70 && distanceToPlayer > 8)
        {
            if (enableToShoot)
                StartCoroutine("ShootState");
        }
        
        if (distanceToPlayer > 10 || distanceToPlayer < 20)
            sentinelState = SentinelState.FOLLOW;
        else
            sentinelState = SentinelState.PATROL;
    }

    void FollowState()
    {
        if (sentinelState != SentinelState.FOLLOW)
            return;

        agent.speed = 8f;
        agent.stoppingDistance = 2f;
        targetPoint = transform.position - playerTransform.position;
    }

    IEnumerator ShootState()
    {
        transform.LookAt(playerTransform);
        enableToShoot = false;
        Shot();
        yield return new WaitForSeconds(2f);
        sentinelAnimator.SetBool("Attack", false);
        enableToShoot = true;
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

        sentinelAnimator.SetBool("Attack", true);

        GameObject BulletTMP = ObjectPool.Instance.GetGameObjectOfType(sentinelBulletPrefab.name, true);

        BulletTMP.transform.position = pointer.transform.position;
        BulletTMP.transform.rotation = Quaternion.identity;
        BulletTMP.transform.forward = transform.forward;
        BulletTMP.SetActive(true);
        BulletTMP.GetComponent<Rigidbody>().velocity = Vector3.zero;
        BulletTMP.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        BulletTMP.GetComponent<Rigidbody>().AddForce((targetPoint - pointer.transform.position).normalized * bulletForce, ForceMode.Impulse);
    }
}
