using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    public GameObject[] Maps;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject m in Maps)
        {
            m.SetActive(false);
            Maps[PlayerPrefs.GetInt("Mode")].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
