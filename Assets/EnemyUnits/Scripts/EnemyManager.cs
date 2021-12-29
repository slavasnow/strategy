using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Управление состояниями
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public void TakeDamage(float damage) //получение урона
    {
        GetComponent<Attributes>().health -= damage;

        StartCoroutine(Blink());
        
        if (GetComponent<Attributes>().health <= 0)
        {
            Death();
            GameObject.Find("GameManager").GetComponent<Manager>().moneyInt += 50; //Цена смерти
        }


    }
    //Уничтожение юнита и действия происходящие исходя из этого
    void Death()
    {
        Instantiate(GetComponent<Attributes>().deadEffect, transform.position, Quaternion.identity); //появление эфекта взрыва
        Destroy(gameObject); //удаление объекта
    }

    private IEnumerator Blink() //Визуализакотор попаданий
    {
        Material matDefault = GetComponent<SpriteRenderer>().material;
        
        GetComponent<SpriteRenderer>().material = GetComponent<Attributes>().matBlink;
        
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().material = matDefault;
    }
}
