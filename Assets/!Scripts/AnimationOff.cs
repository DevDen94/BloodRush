using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOff : MonoBehaviour
{
    public GameObject Player;


    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        Vector3 directionToPlayer = Player.transform.position - transform.position;

        transform.rotation = Quaternion.LookRotation(directionToPlayer, Vector3.down);
    }
}

