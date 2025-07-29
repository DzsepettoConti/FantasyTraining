using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;

    // Roaminghoz

    private float idleBlendTimer = 0f;

    public override void Enter()
    {
        waitTimer = 0;
    }

    public override void Perform()
    {
        PatrolCycle();
        SetMovementAnimation();

        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {
    }

    public void PatrolCycle()
    {
        if (enemy.path == null)
        {
            Debug.Log("nincs path ezért roamol");

           
        }
        else
        {
            if (enemy.agent.remainingDistance < 0.2f)
            {
                waitTimer += Time.deltaTime;

                if (waitTimer > 3)
                {
                    if (waypointIndex < enemy.path.waypoints.Count - 1)
                    {
                        waypointIndex++;
                    }
                    else
                    {
                        waypointIndex = 0;
                    }

                    enemy.agent.isStopped = false;
                    enemy.agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                    waitTimer = 0;
                }
            }
        }
    }

    private void SetMovementAnimation()
    {
        float currentSpeed = enemy.agent.velocity.magnitude;

        if (currentSpeed >= 0.1f)
        {
            enemy.animator.SetFloat("moveSpeed", currentSpeed);
            idleBlendTimer = 0f;

            if (enemy.agent.isStopped)
                enemy.agent.isStopped = false;
        }
        else
        {
            enemy.animator.SetFloat("moveSpeed", 0f);
            idleBlendTimer += Time.deltaTime;

            if (!enemy.agent.isStopped)
                enemy.agent.isStopped = true;

            if (idleBlendTimer >= 3f)
            {
                float idleVariation = Random.Range(0f, 1f);
                enemy.animator.SetFloat("idleBlend", idleVariation);
                idleBlendTimer = 0f;
            }
        }
    }
}
