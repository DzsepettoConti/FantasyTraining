using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float attackTimer;



    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            if (attackTimer > enemy.fireRate)
            {
               
                if (enemy.isRanger) {
                    Shoot();
                } else
                {
                   meleeAttack();
                }
            }
            if (moveTimer > Random.Range(3, 7))
            {
                enemy.agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //change to the search state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
        Transform gunBarell = enemy.gunBarell;
        Vector3 playerPos = enemy.Player.transform.position;
        float distance = Vector3.Distance(gunBarell.position, playerPos);
        Debug.Log($"Ennyi m távol vagyok: {distance}");
        float shootHeight;
        if (distance < 5)
        {
            shootHeight = 1.5f;
            Debug.Log($"A distance kissebb mint 5, ezért ennyi a shootheight: {shootHeight}");
        }
        else if (distance < 10 && distance > 5)
        {
            shootHeight = distance / 2.4f;
        }
        else if (distance > 10)
        {
            shootHeight = distance / 2.5f;
        }
        else
        {
            shootHeight = 1.5f;
        }

        Debug.Log($"Shoot height{shootHeight}");
        Vector3 playerHeadPos = playerPos + Vector3.up * shootHeight;

        Vector3 shootDirection = (playerHeadPos - gunBarell.position).normalized;

        Vector3 randomizedDirection = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection;

        GameObject bullet = GameObject.Instantiate(
            Resources.Load("Prefabs/slimeSpit") as GameObject,
            gunBarell.position,
            Quaternion.LookRotation(randomizedDirection, Vector3.up)
        );

        bullet.GetComponent<Rigidbody>().velocity = randomizedDirection * enemy.bulletVelocity;

        Debug.Log("shoot");
        attackTimer = 0;

    }
    public void meleeAttack()
    {
// Debug.Log("Megyek megütni!");
        enemy.agent.SetDestination(enemy.Player.transform.position);
        enemy.StartCoroutine(FollowAndMeleeAttack());
    }

    private IEnumerator FollowAndMeleeAttack()
    {
        while (true)
        {
            float distance = Vector3.Distance(enemy.transform.position, enemy.Player.transform.position);

            if (distance <= 1.5f) // közelharc-távolság
            {
                //Debug.Log("Melee ütés!");

                PlayerHealth playerHealth = enemy.Player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10); // ugyanúgy, mint a bullet
                }
                enemy.agent.isStopped = true;
                yield break;
            }
            enemy.agent.SetDestination(enemy.Player.transform.position);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
