using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private AudioManager audioManager;
    public int waypointIndex;
    public float waitTimer;
    private bool patrolOn = false;

    public override void Enter()
    {
        audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        if (patrolOn == false && audioManager.audioSource.clip.name == "chaseMusic")
        {
            audioManager.swapAudio();
            patrolOn = true;
        }
        enemy.Agent.speed = 1.5f;
        Perform();
    }

    public override void Perform()
    {
        PatrolCycle();
    }  
    
    public override void Exit()
    {
        patrolOn = false;
    }

    public void PatrolCycle()
    {
        enemy.animator.SetBool("isWalking", true);
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 10)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                {
                    waypointIndex = Random.Range(0, enemy.path.waypoints.Count - 1);
                }

                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                enemy.isWalking = true;
                waitTimer = 0;
            }
            else
            {
                enemy.isWalking = false;
                enemy.animator.SetBool("isWalking", false);
            }
        }
    }
}
