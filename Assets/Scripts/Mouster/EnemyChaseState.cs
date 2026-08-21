using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EnemyChaseState : Isate
{
    private EnemyMain Enemy;

    public EnemyChaseState(EnemyMain Enemy)
    {
        this.Enemy = Enemy;
    }
    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        
    }

    public void OnFixedUpdate()
    {
        if (Enemy.direction.normalized.x >= 0)
        {
            Enemy.anim.Play("WalkRight");
        }
        else
        {
            Enemy.anim.Play("WalkLeft");
        }
        //Debug.Log(Enemy.direction.x);
        Enemy.rb.velocity = Enemy.speed * Enemy.direction.normalized;
    }

    public void OnUpdate()
    {
        //状态联动切换
        if (Enemy.IsInAtk())
        {
            Enemy.TranState(EnemyState.Attack);
        }
        else if (!Enemy.IsInChase())
        {
            Enemy.TranState(EnemyState.Idel);
        }
        else
        {
            return;
        }
    }
}
