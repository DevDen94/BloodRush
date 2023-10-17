using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamManager : MonoBehaviour
{
    public LineRenderer _laserBeam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_laserBeam.gameObject.activeInHierarchy)
        {
            _laserBeam.SetPosition(0, _laserBeam.transform.position);

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Vector3 hitPoint = hit.point;

                _laserBeam.SetPosition(1, hitPoint);
            }
        }
    }
}
