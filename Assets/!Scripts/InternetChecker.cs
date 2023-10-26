using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetChecker : MonoBehaviour
{

    public GameObject CanvasInternetError;


    private void Awake()
    {
        DontDestroyOnLoad(this);
    }



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckForInternet());
    }

    IEnumerator CheckForInternet()
    {
        yield return new WaitForSeconds(0.5f);
        
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            //Time.timeScale = 0f;
            
            CanvasInternetError.SetActive(true);
        }
        else
        {
            Debug.Log("----working---");
            //Time.timeScale = 1f;
            CanvasInternetError.SetActive(false);
        }
        StartCoroutine(CheckForInternet());
    }
}
