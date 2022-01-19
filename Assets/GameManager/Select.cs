using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Данный скрипт используется для работы с юнитами и строениями на глобальной карте. С его помощью мы можем выбирать объект и активировать его функции
/// </summary>
public class Select : MonoBehaviour
{
    //Внутреннии переменные
    private Camera _camera; //переменная камеры
    public GameObject unit; //Переменная юнита

    public Text textHealth; //переменная здорья выбранного юнита
    public Text textLevel; //переменная уровня
    public Text textDamage; //переменная урона

    public GameObject panel;


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
            
            if (raycastHit2D.collider.gameObject)
            {
                if (unit != null) //Проверка выбран ли где-то уже юнит
                {
                    unit.GetComponent<Attributes>().@select = false;
                    //unit.GetComponent<SpriteRenderer>().color = Color.white;
                }
                //выбор объекта и действий над ним.
                unit = raycastHit2D.transform.gameObject;
                unit.GetComponent<Attributes>().@select = true;
                Debug.Log(unit.name);

                switch (unit.name)
                {
                    case "Base":
                        panel.SetActive(true);
                        break;
                    default:
                        panel.SetActive(false);
                        break;
                }
            }
            else
            {
                //снятие всех выделений юнитов
                if (unit != null)
                {
                    unit.GetComponent<Attributes>().@select = false;
                    unit = null;
                }
                Debug.Log("Free Space");
            }
        }

        //показ здоровья для для выбранного объекта
        if (unit != null)
        {
            textHealth.GetComponent<Text>().text = $"Health {unit.GetComponent<Attributes>().health}"; //здоровье юнита когда он выбран
            textLevel.GetComponent<Text>().text = $"Level {unit.GetComponent<Attributes>().level}"; //уровень объекта
            textDamage.GetComponent<Text>().text = $"Damage {unit.GetComponent<Attributes>().damage}"; //уровень объекта
        }
        else
        {
            textHealth.GetComponent<Text>().text = "Health"; //если не выбраны юниты
            textLevel.GetComponent<Text>().text = "Level"; //уровень объекта
            textDamage.GetComponent<Text>().text =  "Damage"; //уровень объекта
        }

    }
}
