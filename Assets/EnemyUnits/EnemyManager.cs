using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public float health = 100;
    public float damage = 2; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void TakeDamage() //получение урона
    {
        health -= damage;
        if (damage == 0)
        {
            Death();
        }

    }
    //Уничтожение юнита и действия происходящие исходя из этого
    void Death()
    {
        Destroy(gameObject); // уничтожение объекта
    }
}
