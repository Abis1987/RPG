using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;    
    public float spawnInterval = 1f;
    public int spawnCount = 3;
    public Vector3 spawnRange = Vector3.one * 5f;
    private bool canSpawn = true;

                       
    [SerializeField] private List<GameObject> spawnedPrefabs = new List<GameObject>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            StartCoroutine(SpawnObjects());
        }
    }

    private IEnumerator SpawnObjects()
    {
        if (canSpawn)
        {
            if(spawnedPrefabs.Count == 0)
            {
                for (int i = 0; i < spawnCount; i++)
                {
                    Vector3 randomPosition = transform.position + new Vector3(Random.Range(-spawnRange.x, spawnRange.x),
                                                                                  Random.Range(-spawnRange.y, spawnRange.y),
                                                                                  Random.Range(-spawnRange.z, spawnRange.z));

                    GameObject spawnedPrefab = Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
                    spawnedPrefabs.Add(spawnedPrefab);


                }
                canSpawn = false;

                yield return new WaitForSeconds(spawnInterval);

                canSpawn = true;
            }
            
        }
        
    }

    private void Update()
    {
        
        for (int i = spawnedPrefabs.Count - 1; i >= 0; i--)
        {
            if (spawnedPrefabs[i] == null)
            {
                spawnedPrefabs.RemoveAt(i);
            }
        }

    }
}