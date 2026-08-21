using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MousterManager : MonoBehaviour
{
    [Header("怪物列表")]
    public List<GameObject> mousterPreList;
    private ExpManager Exp;
    //实时怪物数量
    [HideInInspector] public float aliveCount = 0;
    [Header("环形刷怪范围")]
    [HideInInspector]public Transform playerPosition;
    public float innerRadius;
    public float outerRadius;
    [Header("避障层级")]
    public LayerMask obsLayer;
    private float checkRadius = 0.3f;
    [Header("尝试最大次数")]
    public int maxTryCount = 30;
    //怪物刷新协程
    private Coroutine spawnCoroutine;
    [Header("全局刷怪配置")]
    public int spawnCount = 3;
    public float spawnSpace = 10f;
    public float maxAlive = 30;

    private void Awake()
    {
        GameObject expObj = GameObject.FindGameObjectWithTag("ExpController");
        if (expObj != null)
        { 
            Exp = expObj.GetComponent<ExpManager>();
        }
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerPosition = playerObj.transform;
        }

        StartSpawn();
    }

    //怪物名字转序号
    public int GetIndex(string name)
    {
        switch (name)
        {
            case "Zombie(Clone)":
                return 0;
            case "Skeleton(Clone)":
                return 1;
            case "Bat(Clone)":
                return 2;
            case "dayan(Clone)":
                return 3;
            default:
                return -1;
        }
    }

    //动态怪物池
    private List<GameObject> MousterPool()
    { 
        List<GameObject> pool = new List<GameObject>();
        foreach (GameObject mouster in mousterPreList)
        {
            bool canSpawn = CheckMouster(mouster);
            if (canSpawn)
            { 
                pool.Add(mouster);
            }
        }
        return pool;
    }
    //等级怪物匹配
    private bool CheckMouster(GameObject mous)
    {
        switch (mous.name)
        {
            case "Bat":
                return Exp.currentLevel >= 15;
            case "dayan":
                return Exp.currentLevel >= 10;
            case "Skeleton":
                return Exp.currentLevel >= 5;
            case "Zombie":
                return true;
            default:
                return true;
        }
    }

    //随机取出怪物
    private GameObject GetRandomMouster( List<GameObject> Pool)
    {
        if (Pool.Count == 0)
        {
            return null;
        }
        int random = Random.Range(0, Pool.Count);
        return Pool[random];
    }
    //怪物初始化
    private void MousterInstance(GameObject mouster, Vector2 position)
    {
        GameObject mousterobj = Instantiate(mouster, position, Quaternion.identity, transform);
        EnemyMain enemy = mousterobj.GetComponent<EnemyMain>();
        //生命修正
        enemy.maxHp += (Exp.currentLevel / 10f) * enemy.pmaxhp;
        enemy.hp = enemy.maxHp;
        //攻击修正
        enemy.atk += (Exp.currentLevel / 10f) * enemy.patk;
        //速度修正
        enemy.speed += (Exp.currentLevel / 50f) * enemy.pspeed;

        //怪物计数
        aliveCount++;
    }

    //随机位置
    private Vector2 GetRandomPosition()
    {
        if (playerPosition == null)
        {
            return new Vector2(0, 0);
        }
        Vector2 center = playerPosition.position;

        for (int i = 0; i < maxTryCount; i++)
        {
            float randomAngle = Random.Range(0, Mathf.PI * 2);
            float randomDist = Random.Range(innerRadius, outerRadius);

            float x = center.x + Mathf.Cos(randomAngle)* randomDist;
            float y = center.y + Mathf.Sin(randomAngle)* randomDist;
            Vector2 testPos = new Vector2(x, y);

            Collider2D hit = Physics2D.OverlapCircle(testPos, checkRadius, obsLayer);
            if (hit == null)
            {
                return testPos;
            }
        }
        return new Vector2(0, 0);
    }

    //启动刷怪
    public void StartSpawn()
    {
        if (spawnCoroutine != null)
        {
            return;
        }
        spawnCoroutine = StartCoroutine(LoopSpawn());
    }
    //停止刷怪
    public void StopSpawn()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    //刷怪协程
    private IEnumerator LoopSpawn()
    {
        while (true)
        {
            if (aliveCount <= maxAlive)
            {
                for (int i = 0; i < spawnCount; i++)
                { 
                    Vector2 pos = GetRandomPosition();
                    if (pos.x == 0 && pos.y == 0)
                    {
                        continue;
                    }
                    MousterInstance(GetRandomMouster(MousterPool()), pos);
                }
            }
            yield return new WaitForSeconds(spawnSpace);
        }
    }

    //导出所有怪物数据
    public List<MousterSaveDate> CollectData()
    { 
        List<MousterSaveDate> dataList = new List<MousterSaveDate>();
        foreach (Transform child in transform)
        { 
            EnemyMain enemy = child.gameObject.GetComponent<EnemyMain>();
            if (enemy == null)
            {
                continue;
            }
            dataList.Add(enemy.ExportData());
        }
        return dataList;
    }

    //读入怪物数据
    public void ImportData(List<MousterSaveDate> dataList)
    {
        foreach (Transform child in transform)
        { 
            Destroy(child.gameObject);
        }
        foreach (MousterSaveDate mous in dataList)
        {
            int idx = GetIndex(mous.preName);
            if (idx != -1)
            { 
                GameObject Target = mousterPreList[idx];

                GameObject obj = Instantiate(Target, mous.position, mous.rotation);
                EnemyMain enemy = obj.GetComponent<EnemyMain>();
                if (enemy != null)
                {
                    enemy.ImportData(mous);
                    Rigidbody2D rb2 = obj.GetComponent<Rigidbody2D>();
                    rb2.velocity = Vector2.zero;
                    rb2.angularVelocity = 0;
                    rb2.position = mous.position;
                }
            }
        }
    }

    //存档刷怪配置
    public MousSaveData SaveMous()
    {
        return new MousSaveData()
        {
            alive = this.aliveCount,
            maxAlive = this.maxAlive,
            spawnCount = this.spawnCount,
            spawnSpace = this.spawnSpace,
        };
    }
    //读入刷怪配置
    public void LoadMous(MousSaveData mous)
    {
        this.aliveCount = mous.alive;
        this.maxAlive = mous.maxAlive;
        this.spawnCount = mous.spawnCount;
        this.spawnSpace = mous.spawnSpace;
    }
}