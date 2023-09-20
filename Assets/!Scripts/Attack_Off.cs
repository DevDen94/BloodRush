using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Off : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Flesh")
        {
          /*  if (other.gameObject.GetComponent<AI>().isTrigger_GameObj == false)
            {
                other.gameObject.GetComponent<AI>().followPlayer = false;
                other.gameObject.GetComponent<AI>().isTrigger_GameObj = true;
                other.gameObject.GetComponent<AI>().AttackMode = false;
            }*/
           
        }
    }



}
