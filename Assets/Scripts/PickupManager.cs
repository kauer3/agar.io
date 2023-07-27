using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public GameObject pickupPrefab;
    public int spawnAmount = 1;
    public Vector2 spawnRange;

    // Start is called before the first frame update
    void Start()
    {
       for  (int i = 0; i < spawnAmount; i++)
        {
            GameObject spawnedObject = (GameObject)Instantiate(pickupPrefab);
            spawnedObject.transform.Translate(Random.Range(-spawnRange.x, spawnRange.x), Random.Range(-spawnRange.y, spawnRange.y), 0);
        }
    }
}
