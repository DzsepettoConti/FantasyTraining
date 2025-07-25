using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    AudioSource audioSource;


    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;


    public Animator animator;
    [SerializeField] private float damage;

    private Enemy enemy;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemy = GetComponent<Enemy>();
    }
    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        animator.CrossFadeInFixedTime("Attack 1", 0.2f);

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }
}
