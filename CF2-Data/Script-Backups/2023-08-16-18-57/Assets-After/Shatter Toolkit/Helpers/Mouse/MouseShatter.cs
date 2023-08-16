// Shatter Toolkit
// Copyright 2015 Gustav Olsson
using UnityEngine;

namespace ShatterToolkit.Helpers
{
    public class MouseShatter : MonoBehaviour
    {
        public void Update()
        {
            if (ControlFreak2.CF2Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.ScreenPointToRay(ControlFreak2.CF2Input.mousePosition), out hit))
                {
                    hit.collider.SendMessage("Shatter", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}