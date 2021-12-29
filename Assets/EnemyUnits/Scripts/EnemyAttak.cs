using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttak : MonoBehaviour
{
    /// <summary>
    /// Предполагается управление видами атак дружественных юнитов
    /// </summary>
    //Время на атаку
    private float _curTimeout;
    //перезарядка
    private float fireRate;
    
    // Атака противника оснавная атака юнитов 
    public void  ShortRangeAttack(GameObject hero, float damage, LayerMask heroMask)
    {
        fireRate = GetComponent<Attributes>().fireRate;
        _curTimeout += Time.deltaTime;
        
        if (_curTimeout > fireRate)
        {
            _curTimeout = 0;
            RaycastHit2D raycastHit2D = Physics2D.Linecast(transform.position, hero.transform.position, heroMask);
            if (raycastHit2D)
            {
                hero.GetComponent<UnitManager>().TakeDamage(damage);
            }
        }
    }
}
