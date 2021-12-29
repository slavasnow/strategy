using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Передвижение юнита
/// </summary>
public class EnemyMove : MonoBehaviour
{
    //внешнии переменные 
    private GameObject _hero; //цель
    
    //внутренние переменные
    private NavMeshAgent _agent; // агент
    public GameObject _baseBuild;
    
    
    //private Vector2 position; //gjpbwbz
    private bool _faceRight = true; //селектор повората
    //public Vector2 endPosition = new Vector2(0,0); //координата конечной точки (базы) случайно генеритья
    void Start()
    {
        //инициализация агета
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        
        _baseBuild = GameObject.Find("/MapCreator/Base"); //поиск базы и ее координат 
    }

    // Update is called once per frame
    void Update()
    {
        _hero = GetComponent<EnemyVision>().hero;
        //изменение позиции при обнаружении юнита
        if (!GetComponent<Attributes>().@select)
        {
            _agent.SetDestination(_baseBuild.transform.position);//перемещение в точку  базы
            Flip(_baseBuild.transform.position);
            //position = _baseBuild.transform.position; //присваиваем позицию базы
        }
        else
        {
            _agent.SetDestination(_hero.transform.position); //слежение за юнитом
            Flip(_hero.transform.position);
            //position = _hero.transform.position; //присваем позицию героя
        }

        //_agent.SetDestination(position); //движение к позиции
        //Flip(position); //поворот относительно объекта
    }
    
    /// <summary>
    /// Фукнция поворота спрайта
    /// </summary>
    /// <param name="position"> Позиция противника</param>
    private void Flip(Vector3 position) 
    {
        if ((position.x > transform.position.x && !_faceRight) || (position.x < transform.position.x && _faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            _faceRight = !_faceRight;
        }
    }
}
