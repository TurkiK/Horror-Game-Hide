using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    private AudioManager audioManager;
    private bool chaseOn = false;
    public override void Enter()
    {
        audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        if(chaseOn == false && audioManager.audioSource.clip.name == "ambMusic")
        {
            audioManager.audioSource.Stop();
            audioManager.audioSource.clip = audioManager.chaseMusic;
            audioManager.audioSource.Play();
            chaseOn = true;
        }
        enemy.Agent.speed = 6.05f;
        enemy.Agent.acceleration = 10f;
        enemy.Agent.angularSpeed = 360f;
        Perform();
    }

    public override void Perform()
    {
        Chase();
    }

    public override void Exit()
    {
        chaseOn = false;
    }

    public void Chase()
    {
        enemy.Agent.SetDestination(enemy.player.position);
    }
}
