using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Атрибуты для каждого юнита настраиваются в инспекторе и забираются отсюда на другие скрипты
/// </summary>
public class Attributes : MonoBehaviour
{
    [Header("Атрибуты заначений")]
    public float health; //Здоровье использовать тут
    public float damage; //урон
    public float armor; //броня
    public float cartridge; //кол-во патронов использовать тут
    public float fireRate; //скорострельнось
    public float speed; //скорость передвижения
    public float rangeVision; //радиус видимости
    public float rangeAttack; //радиус атаки
    public int level; //начальный уровень использовать тут
    public float experience; //экспириенс использовать тут

    [Header("Атрибуты селекторов")] 
    public bool @select; //простой селектор использовать тут
    public bool @attack; //селектор атаки использовать тут
    public GameObject selectSprite; //изменение подцветки юнита
    public GameObject popUpSprite; //показатель малого здоровья

    [Header("Атрибыты другие")] 
    public LayerMask layerMaskСharacter; //маска персонажа
    public LayerMask layerMaskBuild; //маска объекта
    public Transform visionPosition; //объект вижина
    public Transform attackPosition; //объект атаки

    [Header("Атрибуты эфектов")] 
    public Material matBlink; //матриал мигания
    public GameObject deadEffect; // парклы после смерти
}
