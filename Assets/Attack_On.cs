using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Attack_On : MonoBehaviour
{
    public GameObject LookAtt;
    public GameObject a;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flesh")
        {
          /*  if (other.gameObject.GetComponent<AI>().isTrigger_GameObj == false)
            {*/
                other.gameObject.GetComponent<AI>().Jumpp_ForAttack = false;
                other.gameObject.GetComponent<AI>().AnimatorComponent.SetInteger("AnimState", 3);
                other.gameObject.GetComponent<AI>().AnimatorComponent.SetTrigger("Jump");
                other.gameObject.GetComponent<AI>().gameObject.transform.LookAt(LookAtt.transform);
                other.gameObject.GetComponent<AI>().agent.SetDestination(a.transform.position);
               // other.gameObject.GetComponent<AI>().isTrigger_GameObj = true;
            //}
        }
    }
}
