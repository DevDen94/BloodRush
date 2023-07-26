using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckWindow : MonoBehaviour
{
    public WaypointGroup New_WayPointGroup;
    public GameObject window;
    public GameObject[] WindowPanels;
    public Slider Windowslider;
    private bool panel1, panel2, panel3, jum;
    public AI Zombie;
    // Start is called before the first frame update
    void Start()
    {
        panel1 = false;
        panel2 = false;
        panel3 = false;
        jum = false;
    }
    public bool sliderEnable = false;

    private float decrementValue = 1f;
    private WaitForSeconds waitTime = new WaitForSeconds(1f);

   public void SliderE()
    {
        StartCoroutine(DecrementSlider());
    }
        
    

    private IEnumerator DecrementSlider()
    {
        while (true)
        {

            Windowslider.value -= decrementValue;
           

            yield return waitTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Windowslider.value <= 70 && panel1==false)
        {
            WindowPanels[0].GetComponent<Rigidbody>().isKinematic = false;
            panel1 = true;
        }
        if (Windowslider.value <= 40 && panel2 == false)
        {
            WindowPanels[1].GetComponent<Rigidbody>().isKinematic = false;
            panel2 = true;
        }
        if (Windowslider.value <= 10 && panel3 == false)
        {
            WindowPanels[2].GetComponent<Rigidbody>().isKinematic = false;
            panel3 = true;
        }
        if (Windowslider.value <= 0 && jum == false)
        {
            Zombie.Jump = true;
            jum = true;

        }
    }
}
