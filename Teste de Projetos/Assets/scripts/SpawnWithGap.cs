using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWithGap : MonoBehaviour
{
    [SerializeField] private GameObject spawn;   // Prefab a ser spawnado
    [SerializeField] private float spawnLength;  // Distância entre cada spawn
    [SerializeField] private int spawnCount;     // Quantos spawns fazer

    void Start()
    {
        SpawnEntities();
    }

     void SpawnEntities()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // Só no eixo Z
            Vector3 pos = transform.position + new Vector3(0, 0, i * spawnLength);
            Instantiate(spawn, pos, Quaternion.identity);
        }
    }
}

