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
    public Vector2 mousePosition; //позиция мыши/

    public bool @select; //переменная активации юнита

    private Camera _camera; //переменная камеры
    private NavMeshAgent _agent; //переменная агента
// Start is called before the first frame update 
    void Start()
    {
        //@select = GetComponent<UnitManager>().@select;
        _camera = Camera.main;
        _agent = GetComponent<NavMeshAgent>();
        
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        mousePosition = _agent.pathEndPosition;// чтобы персонаж не смещался на 0.0
    }

// Update is called once per frame
    void Update()
    {
        @select = GetComponent<UnitManager>().@select;
        if (@select)
        {
            if (Input.GetMouseButton(1))
            {
                mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            _agent.SetDestination(mousePosition);
        }
    }
}
