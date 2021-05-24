using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UIElements;

public class UnitMove: MonoBehaviour
{
    public Vector2 mousePositionStep, startPointPosition; // 1. Точка назначения; 2. Точка начала
    public float speed;
    
    // Start is called before the first frame update
    public void Start()
    {
        startPointPosition = transform.position; //синхронизация позиции
    }

    // Update is called once per frame
    public void Update()
    {
        //движение героя по клику мыши
        if (Input.GetMouseButtonDown(1))
        {
            // берется позиция мыши с мировыми координатами
            mousePositionStep = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // перемещение объекта в точку
        transform.position = Vector3.MoveTowards(startPointPosition, mousePositionStep, speed);
    }
}
