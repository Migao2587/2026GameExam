using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum EnemyState
{ 
    Idel,Chase,Attack,Hurt,Dead,PatrolChase,
}

public class EnemyMain : MonoBehaviour
{
    //状态机
    private Dictionary<EnemyState,Isate> states = new Dictionary<EnemyState,Isate>();
    private Isate currentState;
    //经验系统
    private ExpManager exp;
    protected MousterManager mous;
    //动画
    [HideInInspector]public Animator anim;
    //玩家层级
    public LayerMask playerLayer;
    //空气墙层级
    public LayerMask Wall;
    //玩家位置
    [HideInInspector]public Transform player;
    //玩家方向
    [HideInInspector]public Vector2 direction;
    //距离玩家距离
    [HideInInspector]public float distance;
    //游走限制
    public int wanderKill = 0;
    //各组件
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public SpriteRenderer sr;
    //战利品表
    public List<GameObject> loopList;
    //击杀数
    //public UnityEvent totalKill;

    [Header("基础属性")]
    public float chaseDistance;
    public float atkDistance;
    public float speed;
    public float atk;
    public float maxHp;
    public float hp;
    [HideInInspector] public float pmaxhp;
    [HideInInspector] public float pspeed;
    [HideInInspector] public float patk;



    protected virtual void Awake()
    {
        //获取动画
        anim = GetComponent<Animator>();
        //获取组件
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //实例化怪物状态
        states.Add(EnemyState.Idel, new EnemyIdleState(this));
        states.Add(EnemyState.Chase, new EnemyChaseState(this));
        states.Add(EnemyState.Attack, new EnemyAttackState(this));
        states.Add(EnemyState.Hurt, new EnemyHurtState(this));
        states.Add(EnemyState.Dead, new EnemyDeadState(this));
        states.Add(EnemyState.PatrolChase, new EnemyPatrolChase(this));
        //默认状态为待机
        TranState(EnemyState.Idel);
        //获取经验管理脚本
        GameObject expObj = GameObject.FindGameObjectWithTag("ExpController");
        if (expObj != null)
        {
            exp = expObj.GetComponent<ExpManager>();
        }
        GameObject mousObj = GameObject.FindGameObjectWithTag("MousterManager");
        if (mousObj != null)
        {
            mous = mousObj.GetComponent<MousterManager>();
        }
        //hp
        hp = maxHp;
        pmaxhp = maxHp;
        pspeed = speed;
        patk = atk;

    }
    //状态切换
    public void TranState(EnemyState type)
    {
        if (currentState != null)
        { 
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    protected virtual void Update()
    {
        currentState.OnUpdate();
        findPlayer();
    }

    private void FixedUpdate()
    {
        currentState.OnFixedUpdate();
    }

    //检测追击范围内的玩家
    public void findPlayer()
    {
        Collider2D[] chaseCollisders = Physics2D.OverlapCircleAll(transform.position, chaseDistance+1, playerLayer);
        foreach (var col in chaseCollisders)
        {
            if (!col.CompareTag("Player"))
                continue;

            player = col.transform;
            distance = Vector2.Distance(transform.position, player.position);
            direction = player.position - transform.position;
            break;
        }
    }
    //距离判定
    public bool IsInChase()
    {
        if (player != null)
        {
            if (distance > chaseDistance)
            {
                return false;
            }
            else if (distance > atkDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public bool IsInAtk()
    {
        if (player != null)
        {
            if (distance <= atkDistance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    //攻击事件
    public virtual void AtkPlayer()
    {
        Collider2D[] atkCollisders = Physics2D.OverlapCircleAll(transform.position, atkDistance, playerLayer);
        if (atkCollisders.Length > 0)
        {
            foreach (var col in atkCollisders)
            {
                if (!col.CompareTag("Player"))
                {
                    continue;
                }
                CharacterBase hp = col.GetComponent<CharacterBase>();
                hp.TakeDamage(atk);
            }
        }
    }

    //受伤事件
    public virtual void Hurt(float akt, Vector2 dir)
    {
        hp -= akt;
        if (hp <= 0)
        {
            Die(dir);
        }
        else
        {
            TranState(EnemyState.Hurt);
            if (dir.x >= 0)
            {
                anim.Play("HurtLeft");
            }
            else
            {
                anim.Play("HurtRight");
            }
        }
        
    }

    //死亡事件
    public virtual void Die(Vector2 dir)
    {
        if (dir.x > 0)
        {
            anim.Play("DieLeft");
            GetComponent<Collider2D>().enabled = false;
            enabled = false;
        }
        else
        {
            anim.Play("DieRight");
            GetComponent<Collider2D>().enabled = false;
            enabled = false;
        }
        exp.mousterExp(maxHp, atk, speed);
        spawnLoop();
        GameEvent.killMous();
    }

    //动画销毁
    public virtual void Kill()
    { 
        Destroy(gameObject);
        mous.aliveCount--;
    }

    //掉落物函数
    protected virtual void spawnLoop()
    {
        if (loopList.Count == 0)
        {
            return;
        }
        int randomTotal = Random.Range(0, 101);
        if (randomTotal > 80)
        {
            return;
        }
        int index = Random.Range(0, loopList.Count);
        Instantiate(loopList[index], transform.position, Quaternion.identity);
    }

    //怪物数据导出
    public MousterSaveDate ExportData()
    {
        return new MousterSaveDate()
        {
            preName = gameObject.name,
            position = gameObject.transform.position,
            rotation = gameObject.transform.rotation,
            hp = this.hp,
            maxHp = this.maxHp,
            atk = this.atk,
            speed = this.speed,
        };
    }

    //怪物数据导入
    public void ImportData(MousterSaveDate data)
    { 
        this.transform.position = data.position;
        this.transform.rotation = data.rotation;
        this.speed = data.speed;
        this.atk = data.atk;
        this.maxHp = data.maxHp;
        this.hp = data.hp;
    }
}
