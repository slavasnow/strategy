using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] enemys;
    
    public Vector2 spawnPosition;

    public float count;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        ArrayReload(enemy);
    }

    // Update is called once per frame
    void Update()
    {
        
        count += Time.deltaTime;
        if (count > 25)
        {
            count = 0;
            StartCoroutine(WaitSpawn());
            Array.Resize(ref enemys, enemys.Length + 1);
        }
        
        ArrayReload(enemy);

    }

    void Spawn(GameObject enemy) //спавн врага
    {
        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }

    void ArrayReload(GameObject enemy) //массив врагов
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i] = enemy;
        }
    }

    private IEnumerator WaitSpawn() //спавн юнитов через 0.5
    {
        foreach (GameObject enemy in enemys)
        {
            Spawn(enemy);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
