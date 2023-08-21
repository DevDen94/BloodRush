using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrossHairAimDectector : MonoBehaviour
{
    public Image crosshairImage;
    public LayerMask enemyLayerMask;
    public Color originalCrosshairColor;
    public bool state;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 50, enemyLayerMask))
        {
            crosshairImage.color = Color.red; // Change to the aimed color
            state = true;
        }
        else if(state)
        {
            crosshairImage.color = originalCrosshairColor;
            state = false;// Reset to the default color
        }
    }
}
