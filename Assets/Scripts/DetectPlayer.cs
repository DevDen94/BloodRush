using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;
using MarsFPSKit;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class DetectPlayer : MonoBehaviourPunCallbacks
{
    public static DetectPlayer Instance;
    private List<GameObject> players = new List<GameObject>();
    public NavMeshAgent navMeshAgent;
    private Animator AnimatorComponent;
    public float attackRange = 2f;
    public int MaxHealth = 100;
    public int DamageMultiplier = 1;
    private bool Wait;
    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public Kit_IngameMain main;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        main = Kit_IngameMain.Instance.main;
        
    }
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Photon is not connected. Make sure you are connected to the network.");
            return;
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        AnimatorComponent = GetComponent<Animator>();
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the object.");
            return;
        }
        //Invoke(nameof(Delay), 20f);
        Wait = true;
        foreach (Rigidbody rb in this.gameObject.GetComponentsInChildren<Rigidbody>())
        {
            rigidbodies.Add(rb);
           // rb.isKinematic = true;
            AnimatorComponent.enabled = true;
        }
        PlayerPrefs.SetInt("ForStopZombie", 0);
        Delay();
    }
    void Delay()
    {
        Wait = true;
        PlayerPrefs.SetInt("ForStopZombie", 0);
    }
    void Update()
    {
        if (Wait)
        {
            if (PlayerPrefs.GetInt("ForStopZombie")== 0)
            {
                if (!PhotonNetwork.IsConnected)
                    return;

                // Update the list of players
                UpdatePlayersList();

                // Find the nearest player
                GameObject nearestPlayer = GetNearestPlayer();

                // Do something with the nearest player (e.g., follow or target)
                if (nearestPlayer != null)
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, nearestPlayer.transform.position);

                    if (distanceToPlayer <= attackRange)
                    {
                        // Player is within attack range, perform attack
                        AttackPlayer(nearestPlayer);
                    }
                    else
                    {
                        MoveTowardsPlayer(nearestPlayer.transform.position);
                    }
                    // Your logic here
                  
                }
               
            }
            else
            {
                navMeshAgent.isStopped = true;
                Wait = false;
                AnimatorComponent.SetInteger("AnimState",0);
            }
        }
    }

    void UpdatePlayersList()
    {
        players.Clear();

        // Assuming all players are tagged as "Player"
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (var playerObject in playerObjects)
        {
            if (playerObject != null && playerObject != gameObject)
            {
                players.Add(playerObject);
            }
        }
    }

   public GameObject GetNearestPlayer()
    {
        GameObject nearestPlayer = null;
        float minDistance = float.MaxValue;

        foreach (var player in players)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPlayer = player;
                }
            }
        }

        return nearestPlayer;
    }
    void MoveTowardsPlayer(Vector3 targetPosition)
    {
        navMeshAgent.isStopped = false;
        // Set the destination for the NavMeshAgent
        AnimatorComponent.SetInteger("AnimState", 2);
        navMeshAgent.speed = 1.5f;
        navMeshAgent.SetDestination(targetPosition);
        
    }
    bool AnimationDelayBool =false;
    
    void ForAnimation()
    {
        AnimationDelayBool = false;
    }
    void AttackPlayer(GameObject player)
    {
        if (AnimationDelayBool == true)
        {
            AnimationDelayBool = false;
            Invoke(nameof(ForAnimation), 1f);
        }
        else if (AnimationDelayBool == false)
        {
            navMeshAgent.isStopped = true;

            // Trigger attack animation
            AnimatorComponent.SetTrigger("Attack");
            AnimationDelayBool = true;// Assuming 3 is the attack animation state
        }
        
       
    }
    bool ForEvent =false;
    public void TakeDamage(float damage,int GunId)
    {
        // Apply damage to the zombie
        MaxHealth -= (int)damage*GunId;

        // Check if the zombie's health is zero or below
        if (MaxHealth <= 0)
        {
            foreach (Rigidbody rb in rigidbodies)
            {

                rb.isKinematic = false;
                AnimatorComponent.enabled = false;
                navMeshAgent.enabled = false;
               
            }
            Invoke(nameof(DestroyZombie), 2f);
           
          
        }
       
    }
    public void DestroyZombie()
    {
       
        if (ForEvent == false)
        {
            Debug.LogError("destroy");
            byte evCode = Kit_EventIDs.killEvent;
            PhotonNetwork.RaiseEvent(evCode, 0, new RaiseEventOptions { Receivers = ReceiverGroup.All }, SendOptions.SendReliable);
            ForEvent = true;
        }
        
        PhotonNetwork.Destroy(gameObject);
    }
    void CutMesh(Vector3 cutPosition, Vector3 cutDirection)
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;
            Plane cutPlane = new Plane(transform.InverseTransformDirection(cutDirection), cutPosition);
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            for (int i = 0; i < triangles.Length; i += 3)
            {
                Vector3 vertex1 = vertices[triangles[i]];
                Vector3 vertex2 = vertices[triangles[i + 1]];
                Vector3 vertex3 = vertices[triangles[i + 2]];

                if (cutPlane.GetSide(transform.TransformPoint(vertex1)) &&
                    cutPlane.GetSide(transform.TransformPoint(vertex2)) &&
                    cutPlane.GetSide(transform.TransformPoint(vertex3)))
                {
                    // Remove the triangle
                    triangles[i] = triangles[i + 1] = triangles[i + 2] = -1;
                }
            }

            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }
    }
}
