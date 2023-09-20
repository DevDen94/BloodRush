using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestScript : MonoBehaviour
{
    public GameObject prefab; // The prefab you want to instantiate
    public int numberOfObjects = 30; // Total number of objects to instantiate
    public GameObject[] array1;
    public GameObject[] array2;
    public GameObject[] array3;
    public GameObject[] allObjects;
    void Start()
    {
        InstantiateGameObjects();
        DistributeObjectsEqually();
    }
    public List<GameObject> instantiatedObjectsList = new List<GameObject>();
    
    void InstantiateGameObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 position = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            GameObject newObject = Instantiate(prefab, position, Quaternion.identity);
            instantiatedObjectsList.Add(newObject);
            System.Array.Resize(ref allObjects, allObjects.Length + 1);
            allObjects[allObjects.Length - 1] = newObject;
            // Random value between 20 and 35
            int value = Random.Range(20, 36);
         
        }
     
    }

    void DistributeObjectsEqually()
    {

        int objectsPerArray = allObjects.Length / 3;
        int remainder = allObjects.Length % 3;
        int currentIndex = 0;

        for (int i = 0; i < 3; i++)
        {
            int count = objectsPerArray + (i < remainder ? 1 : 0);

            if (i == 0)
                array1 = new GameObject[count];
            else if (i == 1)
                array2 = new GameObject[count];
            else
                array3 = new GameObject[count];

            for (int j = 0; j < count; j++)
            {
                if (i == 0)
                    array1[j] = allObjects[currentIndex];
                else if (i == 1)
                    array2[j] = allObjects[currentIndex];
                else
                    array3[j] = allObjects[currentIndex];

                currentIndex++;
            }
        }
    }
}
