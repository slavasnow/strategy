using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    /// <summary>
    ///  Скрипт отвечает за нахождение в поле зрения союзных юнитов для врагов
    /// </summary>
    /// 
    public class EnemyVision : MonoBehaviour
{
    //внешние переменные
    private LayerMask _heroMask; //маска врагов. Глобальное использование
    private Transform _visionPos; // объект вижена 
    private Transform _attackPos; //объект атаки
    private float _rangeVision; //радиус круга
    private float _rangeAttaсk; //радиус атаки
    private float _damage;
    //внутренняя
    public GameObject hero; //объект который мы инициализируем
   

    // Start is called before the first frame update
    void Start()
    {
        _heroMask = GetComponent<Attributes>().layerMaskСharacter;
        _visionPos = GetComponent<Attributes>().visionPosition;
        _attackPos = GetComponent<Attributes>().attackPosition;
        _rangeVision = GetComponent<Attributes>().rangeVision;
        _rangeAttaсk = GetComponent<Attributes>().rangeAttack;
        _damage = GetComponent<Attributes>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D vision = Physics2D.OverlapCircle(_visionPos.position, _rangeVision, _heroMask); // проверяем, входит ли в зону видимости противник
        if (vision) // если в зоне появился противник
        {
            hero = vision.transform.gameObject; //добавляем его в объект слежения
            GetComponent<Attributes>().@select = true;//активируем активато
            //GetComponent<SpriteRenderer>().color = Color.red;
            
        }
        else //если же в поле видимости его нет, все выключаем
        {
            GetComponent<Attributes>().@select = false;
            hero = null;
            //GetComponent<SpriteRenderer>().color = Color.white;
        }
        
        Collider2D attack = Physics2D.OverlapCircle(_attackPos.position, _rangeAttaсk, _heroMask);
        if (attack) //проверка
        {
            GetComponent<EnemyAttak>().ShortRangeAttack(hero, _damage ,_heroMask);
        }
    }
    
    private void OnDrawGizmosSelected() //отрисовка в редакторе зоны слежения.
    {
        _visionPos = GetComponent<Attributes>().visionPosition;
        _attackPos = GetComponent<Attributes>().attackPosition;
        _rangeVision = GetComponent<Attributes>().rangeVision;
        _rangeAttaсk = GetComponent<Attributes>().rangeAttack;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_attackPos.position, _rangeAttaсk);
        Gizmos.DrawWireSphere(_visionPos.position, _rangeVision);
        
    }
}
