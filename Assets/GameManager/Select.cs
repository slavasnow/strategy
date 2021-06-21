using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/// <summary>
/// Данный скрипт используется для работы с юнитами и строениями на глобальной карте. С его помощью мы можем выбирать объект и активировать его функции
/// </summary>
public class Select : MonoBehaviour
{
    private Camera _camera; //переменная камеры
    public GameObject unit; //Переменная юнита

// Start is called before the first frame update
    void Start()
    {
        unit = null;//обнуление переменной выбора юнитов
        _camera = Camera.main; //кеширование камеры
    }

// Update is called once per frame
    void Update()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); //позиция мыши на глобальной карте
    
        if (Input.GetMouseButtonDown(0)) 
        {
            //RaycastHit2D raycastHit2D = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition), Vector2.zero);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(mousePos, Vector2.zero);

            if (raycastHit2D)
            {
                if (unit != null) //Проверка выбран ли где-то уже юнит
                {
                    unit.GetComponent<UnitManager>().@select = false;
                    unit.GetComponent<SpriteRenderer>().color = Color.white;
                }
                //выбор объекта и действий над ним.
                unit = raycastHit2D.transform.gameObject;
                unit.GetComponent<UnitManager>().@select = true;
                //unit.GetComponent<SpriteRenderer>().color = Color.green;
                Debug.Log(unit.name);
            }
            else
            {
                //снятие всех выделений юнитов
                if (unit != null)
                {
                    unit.GetComponent<UnitManager>().@select = false;
                    //unit.GetComponent<SpriteRenderer>().color = Color.white;
                    unit = null;
                }
                Debug.Log("Free Space");
            }
        }
    }
}
