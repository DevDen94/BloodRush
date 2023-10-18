using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamManager : MonoBehaviour
{
    public LineRenderer _laserBeam;
    public CameraControl _cameraForRaycasting;
    public GameObject _reloadingSign;

    // Update is called once per frame
    void Update()
    {
        if(_laserBeam.gameObject.activeInHierarchy)
        {
            _laserBeam.SetPosition(0, _laserBeam.transform.position);

            RaycastHit hit;

            if (Physics.Raycast(_cameraForRaycasting.transform.position, _cameraForRaycasting.transform.forward, out hit))
            {
                Vector3 hitPoint = hit.point;

                if (!_reloadingSign.activeInHierarchy)
                {
                    _laserBeam.SetPosition(1, hitPoint);
                }
                else
                {
                    if(Physics.Raycast(transform.position, transform.forward, out hit))
                    {
                        Vector3 hitPointR = hit.point;

                        _laserBeam.SetPosition(1, hitPointR);
                    }
                }
            }
        }
    }
}
