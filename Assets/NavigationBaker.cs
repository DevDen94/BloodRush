using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationBaker : MonoBehaviour
{

    public static NavigationBaker Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(Enabler), 1f);
    }
    public void Enabler()
    {
        GetComponent<AI>().enabled = false;
        GetComponent<CharacterDamage>().enabled = false;
        GetComponent<NPCAttack>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<RemoveBody>().enabled = false;
    }

    public void WindowBreakerAnimation()
    {
        
        transform.GetChild(0).GetComponent<Animator>().SetInteger("AnimState", 3);
    }

    public void Disable()
    {
        GetComponent<AI>().enabled = true;
        GetComponent<CharacterDamage>().enabled = true;
        GetComponent<NPCAttack>().enabled = true;
        GetComponent<NavMeshAgent>().enabled = true;
        //GetComponent<RemoveBody>().enabled = true;
        GetComponent<SWS.splineMove>().enabled = false;
    }
}