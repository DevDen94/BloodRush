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
    private bool panel1, panel2, panel3,panel4, jum;
    public GameObject[] Zombie;
    public GameObject JumpP;
    public bool IS_door;
    public int Index_C;
    public GameObject NAV_Obstacle;
    // Start is called before the first frame update
    void Start()
    {
        panel1 = false;
        panel2 = false;
        panel3 = false;
        jum = false;
        Is_2nd = false;
    }
    public bool sliderEnable = false;

    private float decrementValue = 1f;
    private WaitForSeconds waitTime = new WaitForSeconds(1f);

   public void SliderE()
    {
        StartCoroutine(DecrementSlider());
    }

    public bool Is_2nd;

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
        if (Windowslider.value <= 90 && panel1==false)
        {
            if (IS_door)
            {
                WindowPanels[0].transform.rotation = Quaternion.Euler(2f, 0, 0);
            }
            WindowPanels[0].GetComponent<Rigidbody>().isKinematic = false;
            panel1 = true;
        }
        if (Windowslider.value <= 75 && panel2 == false)
        {
            if (IS_door)
            {
                WindowPanels[1].transform.rotation = Quaternion.Euler(2f, 0, 0);
            }
            WindowPanels[1].GetComponent<Rigidbody>().isKinematic = false;
            panel2 = true;
        }
        if (Windowslider.value <= 50 && panel3 == false)
        {
            if (IS_door)
            {
                WindowPanels[2].transform.rotation = Quaternion.Euler(2f, 0, 0);
            }
            WindowPanels[2].GetComponent<Rigidbody>().isKinematic = false;
            panel3 = true;
        }
        if (Windowslider.value <= 30 && panel4 == false)
        {
            if (IS_door)
            {
                WindowPanels[3].transform.rotation = Quaternion.Euler(2f, 0, 0);
            }
            WindowPanels[3].GetComponent<Rigidbody>().isKinematic = false;
            panel4 = true;
        }

        
            if (Windowslider.value <= 0 && jum == false)
            {
            Is_2nd = true;
            jum = true;
              for (int i = 0; i < Zombie.Length; i++)
                {
                     if (Zombie[i] == null)
                    {
                      print("ABC");
                   }
                   else
                {
                    Zombie[i].GetComponent<AI>().Jump = true;
                }
                  
                }
               
                if (IS_door)
                {
                GameManager.instance.AttackModeOn = true;
            }
            else
            {
                NAV_Obstacle.SetActive(false);
            }
        }
        
    }
    public void CheckSlider()
    {
        jum = false;
        if (Windowslider.value <= 0 && jum == false)
        {
            Debug.LogError("CheckSlider");
            for (int i = 0; i < Zombie.Length; i++)
            {
                if (Zombie[i] == null)
                {
                    print("Abc");
                }
                else
                {
                    Zombie[i].GetComponent<AI>().Jump = true;
                }
            }

            jum = true;

        }
    }
}
