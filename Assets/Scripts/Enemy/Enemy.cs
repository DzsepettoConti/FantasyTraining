using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    public NavMeshAgent agent;
    private GameObject player;
    public bool isRanger;
    public PlayerHealth playerHealt;

    //just for debug


    public GameObject Player { get => player; }
    public Path path;
    [Header("Sight Values")]
    public float sightDistance = 10f;
    public float fieldOfView = 85f;
    public float eyeHeight;
    [Header("Weapon Values")]
    public Transform gunBarell;
    [Range(0.1f, 10f)]
    public float fireRate;
    public float bulletVelocity;

    [SerializeField]
    private string currentState;


    [SerializeField] private float health;
    [SerializeField] private bool isDead;
    [SerializeField] private UnityEngine.UI.Slider slider;
    [SerializeField] private Camera targetCamera;
     public Animator animator;

    [SerializeField] testExperienceManager playerStats;
    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        slider.value = health;
        CheckHealth(health);
    }

    void Start()
    {
        GetReferences();
        isDead = false;
        slider.maxValue = health;
        slider.minValue = 0;
    }
    void Update()
    {
        UpdateHealthBar();
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }
    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // is the player close enough?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                // emeljük meg a célpontot (pl. fejmagasság)
                Vector3 playerHeadPos = player.transform.position + Vector3.up * 1.5f;

                Vector3 targetDirection = playerHeadPos - (transform.position + Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.green);
                            return true;
                        }
                    }
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance, Color.red);
                }
            }
        }
        return false;
    }
    private void CheckHealth(float health)
    {
        if (health <= 0)
        {
            isDead = true;
            enemyDead();
        }
    }
    private void enemyDead()
    {
        Debug.Log("Meghalt az enemy");
        Destroy(gameObject);
        playerStats.HandleKill("Skeleton");
    }
    private void GetReferences()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");

    }
    private void UpdateHealthBar()
    {
        slider.value = health;
        slider.transform.LookAt(targetCamera.transform.position);

    }


}
