using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Attack_On : MonoBehaviour
{
    public GameObject LookAtt;
    public GameObject a;
    BoxCollider box;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flesh")
        {
            
                other.gameObject.GetComponent<AI_Parent>().Parent.counter += 1;
            
            other.gameObject.GetComponent<AI_Parent>().Parent.Jumpp_ForAttack = false;
                other.gameObject.GetComponent<AI_Parent>().Parent.AnimatorComponent.SetInteger("AnimState", 3);
            other.gameObject.GetComponent<AI_Parent>().Parent.AnimatorComponent.SetTrigger("Jump");
            other.gameObject.GetComponent<AI_Parent>().Parent.gameObject.transform.LookAt(LookAtt.transform);
            other.gameObject.GetComponent<AI_Parent>().Parent.agent.SetDestination(a.transform.position);
           
        }
    }


}
