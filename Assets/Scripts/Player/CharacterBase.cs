using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterBase : MonoBehaviour
{
    [Header("基础属性")]
    public float hp;
    public float maxHp;
    public float speed;
    public float atk;
    public float spaceTime;
    public float money;
    private bool wudi = false;
    private string tempHpChange;
    public UnityEvent<float, float> refreshHpUI;
    public UnityEvent DIE;
    public UnityEvent updateUI;
    [Header("全局管理联动")]
    public ExpManager EXP;
    public MousterManager MouM;
    

    [HideInInspector]public Rigidbody2D rb;
    [HideInInspector]public Animator anim;
    [HideInInspector] public NormalShoot shoot;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        hp = maxHp;
        money = 0;
        tempHpChange = "" + hp + maxHp;
        refreshHpUI?.Invoke(hp, maxHp);
        shoot = GetComponent<NormalShoot>();
    }
    //检测血量变化
    private void LateUpdate()
    {
        if (tempHpChange != ""+hp+maxHp)
        {
            refreshHpUI?.Invoke(maxHp, hp);
            tempHpChange = "" + hp + maxHp;
        }
    }
    //受伤
    public virtual void TakeDamage(float damage)
    {
        if (wudi)
        {
            return;
        }
        else
        {
            if (hp - damage > 0)
            {
                hp -= damage;
                anim.SetTrigger("isHurt");
                StartCoroutine(wudiCount());
            }
            else
            {
                Die();
            }
        }

    }
   
    IEnumerator wudiCount()
    {
        wudi = true;
        yield return new WaitForSeconds(spaceTime);
        wudi = false;
    }

    //死亡
    public virtual void Die()
    {
        hp = 0;
        refreshHpUI?.Invoke(maxHp, 0);
        Destroy(gameObject);
        DIE?.Invoke();
    }

    //玩家数据导出
    public PlayerSaveData ExportData()
    {
        return new PlayerSaveData()
        {
            position = gameObject.transform.position,
            rotation = gameObject.transform.rotation,
            hp = this.hp,
            maxhp = this.maxHp,
            atk = this.atk,
            spaceTime = this.spaceTime,
            speed = this.speed,
            money = this.money,
        };
    }

    //玩家数据导入
    public void ImportData(PlayerSaveData Data)
    { 
        gameObject.transform.position = Data.position;
        gameObject.transform.rotation = Data.rotation;
        this.atk = Data.atk;
        this.hp = Data.hp;
        this.maxHp = Data.maxhp;
        this.speed = Data.speed;
        this.money = Data.money;
        this.spaceTime = Data.spaceTime;
    }
}
