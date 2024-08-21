using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using System.IO;


public class ZombieControler : MonoBehaviourPunCallbacks
{
    public GameObject[] Zombie;
    public Transform[] ZombieTransform;
    PhotonView PV;
    public float spawnInterval = 15f; 
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            InvokeRepeating("SpawnZombie", 0f, spawnInterval);
        }
        //Admob.Instance.ShowSmallBanner();

       
    }
    void SpawnZombie()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            // Randomly choose a zombie prefab from the array
            int randomIndex = Random.Range(0, Zombie.Length);
            GameObject zombiePrefab = Zombie[randomIndex];
            int randomIndexSpawnPoint = Random.Range(0, ZombieTransform.Length);
            // Spawn the selected zombie at a random position
          
            PhotonNetwork.Instantiate(Path.Combine("Zombie",zombiePrefab.name), ZombieTransform[randomIndexSpawnPoint].position, ZombieTransform[randomIndexSpawnPoint].rotation, 0, new object[] { PV.ViewID });

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
