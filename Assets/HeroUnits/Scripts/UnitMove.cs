using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
/// <summary>
/// Логика движения юнита, на данный момент примитивное, в последующем будет умное.
/// </summary>
public class UnitMove: MonoBehaviour
{
    //внешние переменные
    private bool @select; //переменная активации юнита
    
    //внутренние переменные
    private Vector2 _mousePosition; //позиция мыши/
    private Camera _camera; //переменная камеры
    private NavMeshAgent _agent; //переменная агента
    private Animator _animator; //анимации

// Start is called before the first frame update 
    void Start()
    {
        //@select = GetComponent<UnitManager>().@select;
        _camera = Camera.main; //инициализация камеры
        _agent = GetComponent<NavMeshAgent>();//инициализация объекта
        _animator = GetComponent<Animator>();
        
        _agent.updateRotation = false; 
        _agent.updateUpAxis = false;

        _mousePosition = _agent.nextPosition;// чтобы персонаж не смещался на 0.0
    }

// Update is called once per frame
    void Update()
    {
        @select = GetComponent<Attributes>().@select; //синхронизируем переменную через атрибуты
        if (@select)
        {
            if (Input.GetMouseButton(1))
            {
                _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);

            }

            _agent.SetDestination(_mousePosition);
            GetComponent<UnitManager>().Flip(_mousePosition);
        }

        if (!_agent.hasPath)
        {
            _animator.Play("IDLE");
        }
        else
        {
            _animator.Play("MOVE");
        }
    }
}
