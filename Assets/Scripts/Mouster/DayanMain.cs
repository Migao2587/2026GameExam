using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DayanMain : EnemyMain
{
    public GameObject Small;
    [HideInInspector] public bool rebuild;


    protected override void Awake()
    {
        base.Awake();
        rebuild = true;
    }
    public override void Kill()
    {
        if (rebuild)
        {
            for (int i = 0; i < 2; i++)
            {
                SpawnSmall(maxHp, atk, speed, GetRandomPos());
            }
        }
        Destroy(gameObject);
        mous.aliveCount--;
    }

    //分裂方法
    private void SpawnSmall(float hp, float atk, float speed,Vector2 pos)
    {
        GameObject small = Instantiate(Small , pos , transform.rotation);

        DayanMain smallControl = small.GetComponent<DayanMain>();
        if (smallControl != null)
        {
            smallControl.SetSmall(hp, atk, speed);
            small.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        }
    }

    //小怪数值初始化
    public void SetSmall(float nhp, float natk, float nspeed)
    {
        maxHp = nhp / 2;
        hp = maxHp;
        atk = natk / 2;
        speed = nspeed / 2;
        rebuild = false;
        this.enabled = true;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = true;
        }
    }

    //随机点位选择
    private Vector2 GetRandomPos()
    {
        for (int i = 0; i < 5; i++)
        {
            float randAngle = Random.Range(0f, Mathf.PI * 2f);
            float randDist = Random.Range(0f, 1f);

            float offX = Mathf.Cos(randAngle) * randDist;
            float offY = Mathf.Sin(randAngle) * randDist;

            Vector2 cheackPos = (Vector2)transform.position + new Vector2(offX, offY);

            Collider2D hit = Physics2D.OverlapCircle(cheackPos, 0.3f,-1);
            if (hit == null)
            {
                return cheackPos;
            }
        }
        return transform.position;
    }

    protected override void spawnLoop()
    {
        if (rebuild)
        {
            base.spawnLoop();
        }
        else
        {
            return;
        }
    }
}
