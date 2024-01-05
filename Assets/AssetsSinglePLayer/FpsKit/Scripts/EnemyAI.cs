using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private String PlayerTag="Player";
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private Vector3 DistanceFromPlayer;
    [SerializeField]
    private float EnemySpeedToFollowPlayer;
    private float MinDistance;


    private void Start()
    {
        EnemySpeedToFollowPlayer = 1f;
        MinDistance = 1.8f;
    }


    private void Update()
    {

        if (Player == null)
        {
            Player = GameObject.Find(PlayerTag).GetComponent<Transform>();
        }

        EnemyFollowPlayer();
    }

    private void EnemyFollowPlayer()
    {

       

        if (Vector3.Distance(Player.position, transform.position) > MinDistance)
        {
            #if UNITY_EDITOR
            Debug.Log(DistanceFromPlayer);
            #endif
            GetComponent<Animator>().SetBool("isWalk", true);
            DistanceFromPlayer = Player.position - transform.position;
            transform.position += transform.forward * EnemySpeedToFollowPlayer * Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(DistanceFromPlayer);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
        }
        else if (Vector3.Distance(Player.position, transform.position) < MinDistance)
        {
            GetComponent<Animator>().SetBool("isWalk", false);

        }
    }



}
