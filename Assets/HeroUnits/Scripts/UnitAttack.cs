using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
    /// <summary>
    /// Предполагается управление видами атак дружественных юнитов
    /// </summary>
public class UnitAttack : MonoBehaviour
{
    //внешние переменные
    private float _fireRate; //скорострельность
    //внутренние переменные
    private float _curTimeout; //счетчик тайм - аута всегда 0

    // Атака противника оснавная атака юнитов 
    public void SimpleAttack(float damage , Vector2 enemyPosition, GameObject enemy) //дамаг, прзиция атаки, сам объект
    {
        _fireRate = GetComponent<Attributes>().fireRate; //инициализируем скоростельность
        //_enemyPosition = GetComponent<UnitVision>().enemy.transform.position; //инициализация позиции противника для стрельбы
        
        _curTimeout += Time.deltaTime;
        
        if (_curTimeout > _fireRate)
        {
            _curTimeout = 0;
            RaycastHit2D raycastHit2D = Physics2D.Linecast(transform.position, enemyPosition);
            if (raycastHit2D)
            {
                enemy.GetComponent<EnemyManager>().TakeDamage(damage);
            }
        }
    }
}
