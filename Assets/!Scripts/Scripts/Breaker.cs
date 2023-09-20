using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{

    public UnityEngine.UI.Slider HealthBar;
    public float reductionSpeed = 0.000000000000000000000000000000001f; // Speed at which the slider reduces
    public float minValue = 0.0f;       // Minimum value the slider can reach
    public float maxValue = 100.0f;    // Maximum value the slider can have

    private float currentValue;
    public Color minColor = Color.red;
    public Color maxColor = Color.green;

    bool isTrigger;
    public GameObject ObjectMeshParent;

    public Transform[] childObjects;
    Collider ZombieObject;
    public Transform jumpObject;

    bool isJump;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Flesh")
    //    {
    //        StartCoroutine(checkDoor_windowHealth());
    //        ZombieObject = other;
    //    }
    //}

    private void Start()
    {
        isJump = false;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Flesh")
        {
            StartCoroutine(checkDoor_windowHealth());
            ZombieObject = other;
        }
    }



    IEnumerator checkDoor_windowHealth()
    {
        yield return new WaitForSeconds(0.5f);
        DecreaseHealth();
        StartCoroutine(checkDoor_windowHealth());
    }




   public void DecreaseHealth()
    {
        
        HealthBar.value = HealthBar.value - 1;
        // Calculate the normalized value of the slider (0 to 1)
       float normalizedValue = (HealthBar.value - HealthBar.minValue) / (HealthBar.maxValue - HealthBar.minValue);
       // Interpolate the color between minColor and maxColor based on the slider's value
       Color lerpedColor = Color.Lerp(minColor, maxColor, normalizedValue);
       HealthBar.fillRect.gameObject.GetComponent<UnityEngine.UI.Image>().color = lerpedColor;
        childObjectHealth();
    }

    public void childObjectHealth()
    {
        if (HealthBar.value <= 0 && !isJump)
        {
            Debug.Log(ZombieObject);
            ZombieObject.gameObject.GetComponent<ZombieJumpFromWindow>().JumpFromWindow(jumpObject);
            isJump=true;
        }
    }


}
