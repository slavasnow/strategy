using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    
    public bool select; // активатор
    
    void Start()
    {
        select = false; //объект диактивирован по умолчанию
        GetComponent<UnitMove>().Start();
    }

    void Update()
    {
        //если объект выбран, то начинаем движение
        if (select == true) 
        {
            GetComponent<UnitMove>().Update(); //вызос синхролнизации позиции
        }
    }

    // Действия при навидении
    private void OnMouseEnter() //наведенено
    {
        if (select == false)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    private void OnMouseExit() 
    {
        if (select == false)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    //выбор объекста и активирование функций
    private void OnMouseDown()
    {
        if (select == false)
        { 
            select = true; //активатор 
            GetComponent<SpriteRenderer>().color = Color.green; //подсвечивание
            GetComponent<UnitMove>().enabled = true; // активация движения
        }
        else
        {
            select = false; // активатор
            GetComponent<SpriteRenderer>().color = Color.white; // снятие подсвечивания
            GetComponent<UnitMove>().enabled = false; //деактивация ддижения
        }

    }
}
