using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardianBehaviour : MonoBehaviour
{

    public Animator guardianAnimator;
    public NavMeshAgent agent;

    private GameObject player;
    private Transform playerTransform;

    public GuardianState guardianState;

    public Vector2 patrolArea;
    private Vector3 targetPoint;

    private string[] hitType = { "HAttack", "VAttack", "SAttack"};
    private string hit;

    private bool playerDetected;
    public bool enableIK;
    public bool isHitting;
    public bool isAtraction;

    public int reciveBulletCount;

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
        reciveBulletCount = 0;
        guardianAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("PlayerPrincipal");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        guardianState = GuardianState.PATROL;

        InvokeRepeating("GeneratePatrolPosition", 0f, 5f);
        InvokeRepeating("StateManager", 0f, 0.5f);

        InvokeRepeating("FollowState", 0f, 0.5f);
        InvokeRepeating("CoverState", 0f, 0.2f);

        InvokeRepeating("AttractionState",0f,0.1f);

        isHitting = false;
        
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targetPoint);
        guardianAnimator.SetFloat("Speed", agent.velocity.sqrMagnitude);

        if(enemyLife.isDead) {
            targetPoint = transform.position;
            StopAllCoroutines();
            CancelInvoke();
        }
    }

    void GeneratePatrolPosition()
    {
        if (guardianState != GuardianState.PATROL || isHitting)
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

        if (enemyLife.isCover)
        {
            guardianState = GuardianState.COVER;
        }
        else if (distanceToPlayer < 4)
        {
            guardianState = GuardianState.HIT;
            StartCoroutine("HitState");
        }
        else if (distanceToPlayer < 10)
            agent.speed = 10f;
        else if (distanceToPlayer < 20)
        {
            guardianAnimator.SetBool("Atraction", false);
            guardianState = GuardianState.FOLLOW;
        }
        else if (playerDetected && distanceToPlayer > 20)
        {
            guardianState = GuardianState.ATTRACTION;

        }
        else
            guardianState = GuardianState.PATROL;
    }

    void FollowState()
    {
        if (guardianState != GuardianState.FOLLOW || isHitting)
            return;

        playerDetected = true;
        agent.speed = 5f;
        agent.stoppingDistance = 2f;
        targetPoint = playerTransform.position;

    }

    void AttractionState()
    {
        if(guardianState != GuardianState.ATTRACTION) 
            return;

        guardianAnimator.SetBool("Atraction", true);

        targetPoint = transform.position;

        Vector3 dir = transform.position - player.transform.position;

        player.GetComponent<Rigidbody>().AddForce(dir * 10f,ForceMode.Impulse);
    }

    void CoverState()
    {
        if (guardianState != GuardianState.COVER)
        {
            guardianAnimator.SetBool("Cover", false);
            return;
        }

        targetPoint = transform.position;
        transform.LookAt(playerTransform);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerTransform.position), 50 * Time.deltaTime);
        guardianAnimator.SetBool("Cover", true);
    }

    IEnumerator HitState()
    {
        if (guardianState != GuardianState.HIT || isHitting)
            yield return new WaitForSeconds(0.5f);

        targetPoint = transform.position;

        isHitting = true;
        int hitIndex = Random.Range(0, 3);
        hit = hitType[hitIndex];

        guardianAnimator.SetTrigger(hit);
        yield return new WaitForSeconds(2f);

        isHitting = false;
    }


    public enum GuardianState
    {
        PATROL,
        FOLLOW,
        HIT,
        COVER,
        ATTRACTION,
        DEATH
    }

}
