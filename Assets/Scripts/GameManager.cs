using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public Transform[] spawnPositions; // Array of transform positions where enemies will be spawned
    public int StartEnemiesSpawner;
    public SWS.PathManager[] Waypoints;

    public static GameManager Instance;



    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        for(int i=0;i< spawnPositions.Length; i++)
        {
            for (int j = 0; j < StartEnemiesSpawner; i++)
            {
                if (i <= StartEnemiesSpawner - 1) { 
                   GameObject Enemy=  Instantiate(enemyPrefab, spawnPositions[i].position, Quaternion.identity);
                    Enemy.GetComponent<SWS.splineMove>().pathContainer = Waypoints[i];
                    Enemy.transform.GetChild(0).GetComponent<Animator>().SetInteger("AnimState", 1);
                }
                else
                    break;
            }
        }
    }


}
