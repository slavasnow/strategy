using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Основные действия юнита, функции здоровья, активары, стрельба лечение.
/// </summary>
public class UnitManager : MonoBehaviour
{
    //внутренняя переменная
    public bool @select; //выбора юнита зависит от скрипта в Select.cs
    private GameObject _selectSprite; //объект спрайта подключается из Attributes.cs
    private GameObject _popUpSprite; //показатель малого здоровья
    public GameObject weapon2; //второе оружее героя
    private bool _faceRight = true; //селектор повората
    
    void Start()
    {
    }
    /// <summary>
    /// Постоянное отслеживаение состояния юнита, на данный момент происходит его выделение и снятие выделения
    /// </summary>
    void Update()
    {


        //блок выделения юнита
        @select = GetComponent<Attributes>().@select;
        _selectSprite = GetComponent<Attributes>().selectSprite;

        if (@select)
        {
            //GetComponent<SpriteRenderer>().color = Color.cyan;
            _selectSprite.SetActive(true); //включеие подцветки у выбраново юнита
        }
        else
        {
            //GetComponent<SpriteRenderer>().color = Color.white;
            _selectSprite.SetActive(false);  //выключение подцветки у выбраново юнита
        }

        //блок индикатора здоровья
        _popUpSprite = GetComponent<Attributes>().popUpSprite;
        if (GetComponent<Attributes>().health <= 20)
        {
            _popUpSprite.SetActive(true);
        }

        //Изменение пораметров героя взависимости от повышения уровня
        switch (GetComponent<Attributes>().level)
        {
            case 2:
                break;
            case 3:
                weapon2.SetActive(true); //добавляем второе оружее
                break;
        }
    }
    
    //Получение урона от врагов и действия которые выплняются исходя из получнея урона
    public void TakeDamage(float damage)
    {
        GetComponent<Attributes>().health -= damage;
        if (GetComponent<Attributes>().health <= 0)
        {
            Death();
        }

    }
    //Уничтожение юнита и действия происходящие исходя из этого
    void Death()
    {
        Destroy(gameObject); // уничтожение объекта
    }
    
    public void Flip(Vector3 position) 
    {
        if ((position.x < transform.position.x && !_faceRight) || (position.x > transform.position.x && _faceRight))
        {
            //transform.localScale *= new Vector2(-1, 1);
            transform.rotation *= new Quaternion(0f,-90f,0f, 0f);
            _faceRight = !_faceRight;
        }
    }
}
