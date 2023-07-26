using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieJumpFromWindow : MonoBehaviour
{
    
        public AnimationClip jumpAnimation;
        public float jumpForce = 5f;
        

        private bool canJump = true;
        
        private Rigidbody rb;
        public Animator animator;    

        private void Start()
        {
           // animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

    public static ZombieJumpFromWindow Instance;


    private void Awake()
    {
        Instance = this;
    }


    public void JumpFromWindow(Transform jumpTarget)
        {


            animator.SetBool("Jump", true);

                // Disable collisions during the jump
                GetComponent<Collider>().enabled = false;
                Collider[] colliders = GetComponentsInChildren<Collider>();
                foreach (Collider collider in colliders)
                {
                    collider.enabled = false;
                }
                // Enable collisions after a delay
                Invoke("EnableCollisions", 0.5f);
            
        }

        private void EnableCollisions()
        {
        animator.SetBool("Jump", false);
        canJump = true;
        //GetComponent<Animator>().SetInteger("AnimState", 3);
        Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        Invoke("EnableCollisions", 1f);
    }

        private void SetDisabler()
        {
        NavigationBaker.Instance.Disable();

        }

}
