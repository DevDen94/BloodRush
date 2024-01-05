using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] private Transform m_MiniMapCam;
    [SerializeField] private float moveSpeed = 5f; // Adjust this to control camera movement speed

    private void Start()
    {
       // gameObject.SetActive(false);
    }



    void Update()
    {
        //if (Input.GetMouseButton(0)) // Check for left mouse click
        //{
        //    // Get the mouse position in screen coordinates
        //    Vector3 mousePosition = Input.mousePosition;
        //    if (Camera.main == null)
        //    {
        //        return;
        //    }
        //    // Convert the screen coordinates to world coordinates
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, m_MiniMapCam.position.z));
        //    // Move the camera to the clicked position
        //    m_MiniMapCam.position = Vector3.MoveTowards(m_MiniMapCam.position, worldPosition, moveSpeed * Time.deltaTime);
        //}
    }



    //public void ToggleMiniMap()
    //{
    //    if (gameObject.activeInHierarchy)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        gameObject.SetActive(true);
    //    }
    //}
}
