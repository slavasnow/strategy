using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /// <summary>
    ///  Скрипт отвечает за нахождение в поле зрения врагов для союзных юнитов
    /// </summary>
public class UnitVision : MonoBehaviour
{
    //переменный внешнии
    private float _range;//радиус круга
    private float _damage; //урон
    private LayerMask _enemyMask; //маска врагов. Глобальное использование
    //private LayerMask _spawnMask; //маска спавна врагов. Глобальное использование
    private Transform _attackPosition; //позиция начала
    private UnitAttack _unitAttack; //переменная фукнкции атаки

    //переменные внутреннии
    public GameObject enemy; //объект который мы инициализируем
    private bool _hadSeen; //метка для включени действий
    public ParticleSystem fire; //эффект стрельбы 1 оружия
    public ParticleSystem fire2; //эффект стрельбы 2 оружия

    // Инициализация функций и переменных
    void Start()
    {
        _range = GetComponent<Attributes>().rangeAttack; //инициализируем радиус атаки
        _damage = GetComponent<Attributes>().damage; //инициализация урона
        _enemyMask = GetComponent<Attributes>().layerMaskСharacter; //инициализируем маску врага
        //_spawnMask = GetComponent<Attributes>().layerMaskBuild; //инициализируем маску здания
        _attackPosition = GetComponent<Attributes>().attackPosition; //инициализируем начало позиции атаки
        
        //_unitAttack = GetComponent<UnitAttack>(); //инициализация функции атаки
    }

    // Update is called once per frame
    void Update()
    {

        Collider2D enemyes = Physics2D.OverlapCircle(_attackPosition.position, _range, _enemyMask); // проверяем, входит ли в зону противник
        //Collider2D spawns = Physics2D.OverlapCircle(_attackPosition.position, _range, _spawnMask); //проверяем, входи ли в зону база

        //if(enemyes || spawns)
        if (enemyes) // если в зоне появился противник
        {
            enemy = enemyes.transform.gameObject; //добавляем его в объект слежения
            //enemy = spawns.transform.gameObject; //добавляем постройку в объект слежения
            _hadSeen = true; //активируем селектор
            GetComponent<UnitManager>().Flip(enemy.transform.position);
            fire.Play();
            fire2.Play();
        }
        else //если же в поле видимости его нет, все выключаем
        {
            _hadSeen = false;
            enemy = null;
            fire.Pause();
            fire2.Pause();
        }

        if (_hadSeen) //проверка на вхождение дух радусов
        {
            //Debug.Log("I see you now");
            GetComponent<UnitAttack>().SimpleAttack(_damage, enemy.transform.position, enemy);
        }
    }
    
    //отрисовка в редакторе зоны слежения.
    private void OnDrawGizmosSelected()
    {
        _attackPosition = GetComponent<Attributes>().attackPosition;
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_attackPosition.position, _range);
    }

}
