using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    //Animations
    public bool isWalking = false;
    public bool isChasing = false;
    public Animator animator;

    private StateMachine stateMachine;
    private NavMeshAgent agent;

    public Transform player;
    public NavMeshAgent Agent { get => agent; }
    [SerializeField]private string currentState;
    public enemyPath path;

    public FieldOfView fieldOfView;


    //Enemy Audio
    public AudioSource audiosource;
    public AudioClip footStep;
    public AudioClip chaseAudio;

    //Used so the NPC doesn't instantly stop chasing.
    private float waitTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        fieldOfView = GetComponent<FieldOfView>();
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();

        stateMachine.Initialise();
        InvokeRepeating("playFootsteps", 0, 0.6f);
        InvokeRepeating("playRunningFootsteps", 0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = stateMachine.activeState.ToString();
        if (fieldOfView.canSee && currentState != "ChaseState")
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isChasing", true);
            stateMachine.ChangeState(stateMachine.chaseState);
            isWalking = false;
            isChasing = true;
        }
        else if (!fieldOfView.canSee && currentState != "PatrolState")
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > 6)
            {
                animator.SetBool("isChasing", false);
                stateMachine.ChangeState(stateMachine.patrolState);
                isChasing = false;
                isWalking = true;
                waitTimer = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("door"))
        {
            collision.transform.gameObject.GetComponent<Door>().ActionDoor();
        }

        if (collision.transform.CompareTag("Player"))
        {
            gameManager.endGame();
        }
    }    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("door"))
        {
            collision.transform.gameObject.GetComponent<Door>().ActionDoor();
        }
    }

    void playFootsteps()
    {
        if (isWalking)
        {
            audiosource.pitch = Random.Range(0.7f, 1f);
            audiosource.PlayOneShot(footStep);
        }
    }

    void playRunningFootsteps()
    {
        if (isChasing)
        {
            audiosource.pitch = Random.Range(0.9f, 1.4f);
            audiosource.PlayOneShot(footStep);
        }
    }
}
