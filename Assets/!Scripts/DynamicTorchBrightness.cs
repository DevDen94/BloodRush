using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTorchBrightness : MonoBehaviour
{
    public Light _torchLight;
    public float _lightRange;
    public float _maxIntensity;

    public float _currentIntensity;

    void Start()
    {
        _lightRange = _torchLight.range;
        _maxIntensity = _torchLight.intensity;
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(_torchLight.transform.position, _torchLight.transform.forward, out hit, _lightRange))
        {
            float distance = hit.distance;

            distance /= _lightRange;
            distance *= _maxIntensity;
            _currentIntensity = distance / 2;
            float val = Mathf.Lerp(_maxIntensity, _currentIntensity, 2f);
            _torchLight.intensity = val;
            
        }
        else
        {
            _torchLight.intensity = _maxIntensity;
        }
    }
}
