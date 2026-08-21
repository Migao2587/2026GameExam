using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : ThrowBase
{
    public float boomRadius;
    public LayerMask mousterLayer;
    private bool onTr = false;
    private Animator anim;
    private bool isBoom = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (onTr)
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            float pro = state.normalizedTime;
            if (pro > 0.95f)
            {
                DestorySelf();
                onTr = false;
            }
        }
        else
        {
            return;
        }
    }

    public void Initialize(Vector2 direction, float speed, float atk,float radius)
    { 
        base.Initialize(direction, speed, atk);
        if (rb != null)
        {
            //Debug.Log("方向" + direction + ",速度" + speed + ",攻击力" + atk);
            damage = atk;
            dir = direction;
            rb.velocity = direction * speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        boomRadius = radius;
    }

    protected override void OnTriggerEnter2D(Collider2D obj)
    {
        if (!isBoom)
        {
            if (obj.CompareTag("Mouster"))
            {
                Explode();
                rb.velocity = Vector2.zero;
                if (rb == null)
                {
                    Debug.Log("错误rb");
                }    
                if (dir.x >= 0)
                {
                    anim.Play("BoomRight");
                }
                else
                {
                    anim.Play("BoomLeft");
                }
                onTr = true;
            }
            else
            {
                return;
            }
            isBoom = true;
        }
       
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, boomRadius, mousterLayer);
        foreach (var col in hits)
        {
            if (!col.CompareTag("Mouster"))
            {
                continue;
            }
            if (!col.TryGetComponent(out EnemyMain hp))
            {
                continue;
            }
            float dist = Vector2.Distance(transform.position, col.transform.position);
            Vector2 dir2 = col.transform.position - transform.position;
            float k = dist / boomRadius;
            if (k <= 0.8)
            {
                hp.Hurt(damage, dir2);
            }
            else
            { 
                float dmg = (1 - k) * damage;
                //Debug.Log("伤害" + dmg + "原伤害" + damage);
                hp.Hurt(dmg, dir2);
            }
            
        }
    }
}
