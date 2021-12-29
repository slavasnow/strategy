using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{

    private bool @select;
    private float _range;//радиус круга
    private float _damage; //урон
    private LayerMask _enemyMask; //маска врагов. Глобальное использование
    private Transform _attackPosition; //позиция начала

    public GameObject enemy; //объекта врага
    public GameObject selectSprite; //спрайт выделения

    // Start is called before the first frame update
    void Start()
    {
        _range = GetComponent<Attributes>().rangeAttack; //инициализируем радиус атаки
        _damage = GetComponent<Attributes>().damage; //инициализация урона
        _enemyMask = GetComponent<Attributes>().layerMaskСharacter; //инициализируем маску врага
        _attackPosition = GetComponent<Attributes>().attackPosition; //инициализируем начало позиции атаки
    }

    // Update is called once per frame
    void Update()
    {
        //при выделение и открытие магазина
        @select = GetComponent<Attributes>().@select;
        if (@select == true)
        {
            selectSprite.SetActive(true);
        }
        else
        {
            selectSprite.SetActive(false);
        }

        Collider2D enemyes = Physics2D.OverlapCircle(_attackPosition.position, _range, _enemyMask); // проверяем, входит ли в зону противник
        if (enemyes) // если в зоне появился противник
        {
            enemy = enemyes.transform.gameObject; //добавляем его в объект слежения
            //_health -= _damage;
            GetComponent<Attributes>().health -= _damage;
            if (GetComponent<Attributes>().health <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    private void OnDrawGizmosSelected() //зона атаки 
    {
        _attackPosition = GetComponent<Attributes>().attackPosition;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_attackPosition.position, _range);
    }
}
