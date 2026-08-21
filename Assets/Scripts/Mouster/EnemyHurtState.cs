using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtState : Isate
{
    private EnemyMain Enemy;
    private float switchTime;

    public EnemyHurtState(EnemyMain Enemy)
    {
        this.Enemy = Enemy;
    }
    public void OnEnter()
    {
        Enemy.rb.velocity = Vector2.zero;

        Enemy.chaseDistance += 5f;
        if (Enemy.chaseDistance >= 40)
        {
            Enemy.chaseDistance = 40;
        }
        switchTime = 0;
    }

    public void OnExit()
    {
        return;
    }

    public void OnFixedUpdate()
    {
        return;
    }

    public void OnUpdate()
    {
        AnimatorStateInfo state = Enemy.anim.GetCurrentAnimatorStateInfo(0);
        if (!state.IsName("HurtLeft") && !state.IsName("HurtRight"))
        {
            return;
        }
        if (state.normalizedTime > 0.9f )
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
        else
        {
            return;
        }
        
    }
}
