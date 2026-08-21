using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : Isate
{
    private EnemyMain Enemy;

    public EnemyAttackState(EnemyMain Enemy)
    {
        this.Enemy = Enemy;
    }
    public void OnEnter()
    {
        //动画播放翻转
        Enemy.rb.velocity = Vector2.zero;
        //Debug.Log(Enemy.direction.normalized.x);
        if (Enemy.direction.normalized.x >= 0)
        {
            Enemy.anim.Play("AttackRight");
        }
        else
        {
            Enemy.anim.Play("AttackLeft");
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
        //攻击动画结束
        AnimatorStateInfo info = Enemy.anim.GetCurrentAnimatorStateInfo(0);
        bool isAtk = info.IsName("AttackRight") || info.IsName("AttackLeft");
        if (isAtk)
        {
            if (info.normalizedTime >= 0.95f)
            {
                Enemy.TranState(EnemyState.Idel);
            }
        }

        //特殊Bat怪物处理
        //BatMain bat = Enemy as BatMain;
        //if (bat != null)
        //{
        //    bat.EnableCrash = false;
        //}
    }
}
