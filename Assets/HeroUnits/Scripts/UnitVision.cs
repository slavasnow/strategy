using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour
{
    /// <summary>
    ///  Скрипт отвечает за нахождение в поле зрения врагов для союзных юнитов
    /// </summary>
    public LayerMask enemyMask; //маска врагов. Глобальное использование
    public Transform attackPos; //позиция начала
    public float range; //радиус круга
    public GameObject enemy; //объект который мы инициализируем
    public bool active; //метка для включени действий
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D enemyes = Physics2D.OverlapCircle(attackPos.position, range, enemyMask); // проверяем, входит ли в зону противник
        if (enemyes) // если в зоне появился противник
        {
            enemy = enemyes.transform.gameObject; //добавляем его в объект слежения
            active = true; //активируем активато
        }
        else //если же в поле видимости его нет, все выключаем
        {
            active = false;
            enemy = null;
        }

        if (active) //проверка
        {
            Debug.Log("I see you now");
            GetComponent<UnitAttack>().SimpleAttack();
        }
    }
    
    private void OnDrawGizmosSelected() //отрисовка в редакторе зоны слежения.
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(attackPos.position, range);
    }

}
