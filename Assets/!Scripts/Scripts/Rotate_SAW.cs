using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_SAW : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f; // Adjust this value to change the rotation speed.
    public bool IsRotate;
    private void Start()
    {
        IsRotate = true;
    }
    // Update is called once per frame
    public void Update()
    {


        if (IsRotate)
        {
            // Get the current rotation of the GameObject
            Vector3 currentRotation = transform.rotation.eulerAngles;

            // Calculate the new rotation based on the X-axis rotation
            float newRotationX = currentRotation.x + rotationSpeed * Time.deltaTime;

            // Apply the new rotation to the GameObject
            transform.rotation = Quaternion.Euler(currentRotation.x, newRotationX, currentRotation.z);
        }
    }
}
