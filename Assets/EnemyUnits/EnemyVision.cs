using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    /// <summary>
    ///  Скрипт отвечает за нахождение в поле зрения союзных юнитов для врагов
    /// </summary>
    public LayerMask enemyMask; //маска врагов. Глобальное использование
    public Transform attackPos; //позиция начала
    public float range; //радиус круга
    public GameObject hero; //объект который мы инициализируем
    public bool active; //метка для включени действий
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D heroes = Physics2D.OverlapCircle(attackPos.position, range, enemyMask); // проверяем, входит ли в зону противник
        if (heroes) // если в зоне появился противник
        {
            hero = heroes.transform.gameObject; //добавляем его в объект слежения
            active = true; //активируем активато
        }
        else //если же в поле видимости его нет, все выключаем
        {
            active = false;
            hero = null;
        }

        if (active) //проверка
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("hahahaha");
            //GetComponent<UnitAttack>().SimpleAttack();
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    
    private void OnDrawGizmosSelected() //отрисовка в редакторе зоны слежения.
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPos.position, range);
    }
}
