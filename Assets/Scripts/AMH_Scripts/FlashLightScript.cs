using UnityEngine;


    public class FlashLightScript : MonoBehaviour
    {
        public static FlashLightScript Instance;
        public Transform target;
        public bool isGrabbed = false;
        public float speed = 2.5f;
        public Transform positionTarget;
        public Light Light;
        public float BlueBattery = 100;
        public float DamageRate = 0.25f;
        public float BatterySpendNumber = 1;
       
        public AudioSource audioSource;
        public Transform aimPoint;

        void Awake()
        {
            Instance = this;
        }
        public void FlashLight_Decision(bool decision)
        {
            Light.enabled = decision;
        
        }

        private void Start()
        {
            Light = GetComponent<Light>();
        }

        public void Grabbed()
        {
            
            isGrabbed = true;
        }
        private void Update()
        {
            if (!isGrabbed) return;

            
        }

        public void PlayAudioBlueLight()
        {
            audioSource.Play();
        }

        public void StopAudioBlueLight()
        {
            audioSource.Stop();
        }
    
        void LateUpdate()
        {
          
            
            if (!isGrabbed) return;
            Vector3 dir = target.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, speed * Time.deltaTime);
            transform.position = positionTarget.position;
          
           

            //if (GameCanvas.Instance.isFlashBlueNow && BlueBattery > 0)
            //{
            //    BlueBattery = BlueBattery - Time.deltaTime * BatterySpendNumber * 2;
            //    if (!audioSource.isPlaying)
            //    {
            //        PlayAudioBlueLight();
            //    }
            //    var directionLeft2 = Quaternion.AngleAxis(20, aimPoint.transform.right * -1) * Vector3.forward;
            //    var directionLeft = Quaternion.AngleAxis(10, aimPoint.transform.right * -1) * Vector3.forward;
            //    var directionForward = aimPoint.TransformDirection(Vector3.forward);
            //    var directionRight = Quaternion.AngleAxis(10, aimPoint.transform.right) * Vector3.forward;
            //    var directionRight2 = Quaternion.AngleAxis(20, aimPoint.transform.right) * Vector3.forward;
            //    if (Physics.Raycast(aimPoint.position, directionLeft2, out hit, 5, layerMask))
            //    {
            //        hit.transform.GetComponent<DemonScript>().GetDamage(DamageRate);
            //    }
            //    else if (Physics.Raycast(aimPoint.position, directionLeft, out hit, 5, layerMask))
            //    {
            //        hit.transform.GetComponent<DemonScript>().GetDamage(DamageRate);
            //    }
            //    else if (Physics.Raycast(aimPoint.position, directionForward, out hit, 5, layerMask))
            //    {
            //        hit.transform.GetComponent<DemonScript>().GetDamage(DamageRate);
            //    }
            //    else if (Physics.Raycast(aimPoint.position, directionRight, out hit, 5, layerMask))
            //    {
            //        hit.transform.GetComponent<DemonScript>().GetDamage(DamageRate);
            //    }
            //    else if (Physics.Raycast(aimPoint.position, directionRight2, out hit, 5, layerMask))
            //    {
            //        hit.transform.GetComponent<DemonScript>().GetDamage(DamageRate);
            //    }
            //}
            //else if (BlueBattery < 100)
            //{
            //    BlueBattery = BlueBattery + Time.deltaTime * BatterySpendNumber;
            //    if (BlueBattery > 100)
            //    {
            //        BlueBattery = 100;
            //    }
            //}
            //if (BlueBattery <= 0)
            //{
            //    GameCanvas.Instance.FlashLight_BlueEffect_Up();
            //    StopAudioBlueLight();
            //}
            //GameCanvas.Instance.Image_BlueLight.fillAmount = (BlueBattery / 100);
        }
    }
