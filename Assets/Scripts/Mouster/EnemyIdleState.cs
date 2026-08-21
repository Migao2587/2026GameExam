using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : Isate
{
    private EnemyMain Enemy;
    //切换巡逻时间
    private float switchTime;

    public EnemyIdleState(EnemyMain Enemy)
    { 
        this.Enemy = Enemy;
    }
    public void OnEnter()
    {
        //播放待机动画
        Enemy.rb.velocity = Vector3.zero;
        Enemy.anim.Play("Idle");
        switchTime = 0;
    }

    public void OnExit()
    {
        
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnUpdate()
    {

        //状态联动切换
        if (Enemy.IsInChase())
        {
            Enemy.TranState(EnemyState.Chase);
        }
        else if (Enemy.IsInAtk())
        {
            Enemy.TranState(EnemyState.Attack);
        }
        else
        {
            switchTime += Time.deltaTime;
            //切换巡逻
            if (switchTime >= 5f)
            {
                Enemy.TranState(EnemyState.PatrolChase);
            }
            return;
        }
    }
}
