using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;

    // roaminghoz
    public float roamRadius = 5f;
    public float roamWaitTime = 2f;
    private float roamWaitTimer;

    private bool roaming = false;

    public override void Enter()
    {
        waitTimer = 0;
        roamWaitTimer = 0;
    }

    public override void Perform()
    {
        PatrolCycle();

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

            if (!roaming || enemy.agent.remainingDistance < 0.2f)
            {
                roamWaitTimer -= Time.deltaTime;

                if (roamWaitTimer <= 0f)
                {
                    Vector2 randomCircle = Random.insideUnitCircle * roamRadius;
                    Vector3 roamTargetPosition = new Vector3(
                        enemy.transform.position.x + randomCircle.x,
                        enemy.transform.position.y,
                        enemy.transform.position.z + randomCircle.y
                    );

                    enemy.agent.SetDestination(roamTargetPosition);
                    roaming = true;
                    roamWaitTimer = roamWaitTime;
                }
            }
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
                    enemy.agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                    waitTimer = 0;
                }
            }
        }
    }
}
