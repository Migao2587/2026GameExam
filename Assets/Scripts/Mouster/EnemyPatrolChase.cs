using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolChase : Isate
{
    private float wanderTime = 4f;
    private EnemyMain Enemy;
    private Vector2 currentTarget;
    private float tryMaxTime = 1f;
    private float during;
    
    public EnemyPatrolChase(EnemyMain Enemy)
    {
        this.Enemy = Enemy;
    }

    public void OnEnter()
    {
        during = 0;
        //Debug.Log("开始巡逻啦！");
        Wander();
        Enemy.wanderKill++;
        //Debug.Log(Enemy.wanderKill);
        if (Enemy.wanderKill >= 6)
        {
            Enemy.Kill();
        }
        
    }

    public void OnExit()
    {
        
    }

    public void OnFixedUpdate()
    {
        
    }

    public void OnUpdate()
    {
        during += Time.deltaTime;
        if (during >= wanderTime)
        {
            Enemy.TranState(EnemyState.Idel);
        }

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
            return;
        }
    }

    //巡逻函数
    public void Wander()
    {
        float tryTime = 0;
        do
        {
            Vector2 RandomDir = Random.insideUnitCircle;
            Vector2 offset = RandomDir * Enemy.chaseDistance;
            currentTarget = (Vector2)Enemy.transform.position + offset;
            tryTime += Time.deltaTime;
        }
        while (Physics2D.OverlapCircle(currentTarget, 0.3f, Enemy.Wall) && tryTime <= tryMaxTime);
        if (tryTime > tryMaxTime)
        {
            Enemy.TranState(EnemyState.Idel);
            return;
        }

        Vector2 dir = currentTarget - (Vector2)Enemy.transform.position;
        if (dir.x >= 0)
        {
            Enemy.anim.Play("WalkRight");
        }
        else
        {
            Enemy.anim.Play("WalkLeft");
        }
        Enemy.rb.velocity = dir.normalized * Enemy.speed;
        
    }
}
