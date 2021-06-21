using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Основные действия юнита, функции здоровья, активары, стрельба лечение.
/// </summary>
public class UnitManager : MonoBehaviour
{
    [Header("Selectel")] //переменнные выбора
    public bool @select; // активатор действий

    [Header(" Attributes")] // Атрибуты персонажа
    public float health = 100; //Здоровье
    //public float damage; //Имеющийся урон
    void Start()
    {
        @select = false; //объект деактивирован по умолчанию
    }
    /// <summary>
    /// Постоянное отслеживаение состояния юнита, на данный момент происходит его выделение и снятие выделения
    /// </summary>

    void Update()
    {
        if (@select)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    
    //Получение урона от врагов и действия которые выплняются исходя из получнея урона
    public void TakeDamage(float damage)
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
