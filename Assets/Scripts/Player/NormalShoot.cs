using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class NormalShoot : MonoBehaviour
{
    [Header("基础属性")]
    public float selfAtk;
    public float flySpeed = 1f;
    public float searchRadius;
    public float coolTime;
    private float countTime = 0;
    public int targetCount;
    public GameObject bullet;
    public GameObject boom;
    private CharacterBase player;
    public float BoomRadius = 0;


    private void Awake()
    {
        player = GetComponent<CharacterBase>();
    }

    private void FixedUpdate()
    {
        countTime += Time.deltaTime;
        if (countTime >= coolTime)
        { 
            countTime = 0;
            Vector2[] targets = DetectMouster();
            if (targets.Length > 0)
            {
                AtkMouster(targets);
            }
            else
            {
                return;
            }
            return;
        }
        else
        {
            return;
        }
    }


    //索敌函数
    public Vector2[] DetectMouster()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);

        List<(Transform mousterPosition, float distance)> tempList = new List<(Transform, float)>();
        Vector2 playerPosition = transform.position;
        foreach (Collider2D col in hitColliders)
        {
            if (col.CompareTag("Mouster"))
            {
                float dis = Vector2.Distance(playerPosition, col.transform.position);
                tempList.Add((col.transform, dis));
                tempList.Sort((a, b) => a.distance.CompareTo(b.distance));
            }
        }

        List<Vector2> finalList = new List<Vector2>();
        int count = Mathf.Min(tempList.Count, targetCount);
        for (int i = 0; i < count; i++)
        {
            finalList.Add(tempList[i].mousterPosition.position);
        }
        return finalList.ToArray();
            
    }

    //攻击函数
    public void AtkMouster(Vector2[] target)
    {
        if (target == null || target.Length == 0)
        {
            return;
        }
        Vector2 playerPos = transform.position;

        for (int i = 0; i < target.Length; i++)
        {
            Vector2 fireDir = (target[i] - playerPos).normalized;
            if (BoomRadius == 0)
            {
                GameObject bulletObj = Instantiate(bullet, playerPos, Quaternion.identity);
                if (bulletObj.TryGetComponent<ThrowBase>(out ThrowBase proj))
                {
                    proj.Initialize(fireDir, flySpeed, (selfAtk + player.atk));
                }
            }
            else
            {
                GameObject bulletObj = Instantiate(boom, playerPos, Quaternion.identity);
                if (bulletObj.TryGetComponent<FireBall>(out FireBall proj))
                {
                    proj.Initialize(fireDir, flySpeed * 0.5f, (selfAtk + player.atk),BoomRadius);
                }
            }
        }
    }

    //存档
    public ShootSaveData SaveShoot()
    {
        return new ShootSaveData()
        {
            coolTime = this.coolTime,
            targetCount = this.targetCount,
            boomRadius = this.BoomRadius,
        };
    }
    //存档
    public void LoadShoot(ShootSaveData shot)
    { 
        this.coolTime = shot.coolTime;
        this.targetCount = shot.targetCount;
        this.BoomRadius = shot.boomRadius;
    }

}
