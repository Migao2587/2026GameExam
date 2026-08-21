using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBase : MonoBehaviour
{
    [Header("基础属性")]
    public float moveSpeed;
    public float lifeTime = 30f;
    public float damage;
    [HideInInspector] public Vector2 dir;


    protected Vector2 direction;
    protected Rigidbody2D rb;
    protected Coroutine autoKiller;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        autoKiller = StartCoroutine(AutoKill(lifeTime));
    }

    
    //初始化
    public virtual void Initialize(Vector2 direction, float speed, float atk)
    {
        if (rb != null)
        {
            //Debug.Log("方向" + direction + ",速度" + speed + ",攻击力" + atk);
            damage = atk;
            dir = direction;
            rb.velocity = direction * speed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }
    //自动销毁
    private IEnumerator AutoKill(float time)
    { 
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        //Debug.Log("投掷物消失了！");
    }

    //手动销毁
    public void DestorySelf()
    {
        if (autoKiller != null)
        { 
            StopCoroutine(autoKiller);
        }
        Destroy(gameObject);
    }

    //碰撞检测
    protected virtual void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (obj.TryGetComponent(out CharacterBase hp))
            {
                hp.TakeDamage(damage);
            }
            DestorySelf();
        }
    }
}
