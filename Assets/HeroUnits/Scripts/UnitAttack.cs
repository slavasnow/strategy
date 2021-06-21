using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    /// <summary>
    /// Предполагается управление видами атак дружественных юнитов
    /// </summary>
    //Время на атаку
    private float _curTimeout;
    private float fireRate = 1;
    
    // Атака противника оснавная атака юнитов 
    public void SimpleAttack()
    {
        _curTimeout += Time.deltaTime;
        if (_curTimeout > fireRate)
        {
            _curTimeout = 0;
            RaycastHit2D raycastHit2D = Physics2D.Linecast(transform.position, GetComponent<UnitVision>().enemy.transform.position);
            if (raycastHit2D)
            {
                Debug.Log("Shoot");
            }
        }
    }
}
