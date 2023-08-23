using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrossHairAimDectector : MonoBehaviour
{
    public Image crosshairImage;
    public LayerMask enemyLayer;
    public LayerMask wallLayer;
    public Color enemyColor;
    public Color defaultColor;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit enemyHitInfo;
        RaycastHit wallHitInfo;
        bool hitEnemy = Physics.Raycast(ray, out enemyHitInfo, Mathf.Infinity, enemyLayer);
        bool hitWall = Physics.Raycast(ray, out wallHitInfo, Mathf.Infinity, wallLayer);
        if (hitEnemy && (!hitWall || enemyHitInfo.distance < wallHitInfo.distance))
        {
            // Change crosshair color to enemyColor when pointing at an enemy
            crosshairImage.color = Color.red;
        }
        else
        {
            // Change crosshair color back to defaultColor when not pointing at an enemy
            crosshairImage.color = Color.white;
        }
    }
}
