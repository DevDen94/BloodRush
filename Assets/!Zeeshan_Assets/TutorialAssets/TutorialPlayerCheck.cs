using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerCheck : MonoBehaviour
{
    [SerializeField] string _name;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0.0f;

            if (_name == "WalkEndTrigger")
            {
                TutorialManager.s_Instance.pressToSprint.SetActive(true);
            }
            else if(_name == "RunEndTrigger")
            {
                TutorialManager.s_Instance.pressToJump.SetActive(true);

                //TutorialManager.s_Instance.sprintButton.GetComponent<ControlFreak2.TouchButton>().toggle = false;
            }
            else if(_name == "StartGunChangeTut")
            {
                TutorialManager.s_Instance.pressToChangeWeapon.SetActive(true);
            }
            
            this.gameObject.SetActive(false);
        }
    }
}
