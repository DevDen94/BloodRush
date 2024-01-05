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
        transform.LookAt(transform.position + Player.transform.rotation * Vector3.forward, Player.transform.rotation * Vector3.up);
    }
}

