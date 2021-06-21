using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    public Vector2 endPosition = new Vector2(0,0); //координата конечной точки (базы) случайно генеритья
    public bool select;
    public GameObject hero;
    private NavMeshAgent _agent; // агент
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //select = GetComponent<EnemyVision>().active;
        //hero = GetComponent<EnemyVision>().hero;
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        select = GetComponent<EnemyVision>().active;
        hero = GetComponent<EnemyVision>().hero;
        
        if (@select)
        {
            _agent.SetDestination(hero.transform.position);
        }
        else
        {
            _agent.SetDestination(endPosition); //перемещение в точку  базы
        }

    }
}
