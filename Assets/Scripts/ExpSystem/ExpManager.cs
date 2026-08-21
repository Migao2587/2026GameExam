using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExpManager : MonoBehaviour
{
    //玩家
    public CharacterBase player;
    //最大等级限制
    public int MaxLevel = 100;
    //升级需求
    public float expGap = 5;
    public float currentGap = 0;
    public int currentLevel;
    //怪物控制
    public MousterManager mouster;
    public UnityEvent Upgrade;
    


    private void Awake()
    {
        currentLevel = 1;
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<CharacterBase>();
        }
        GameObject mousterObj = GameObject.FindGameObjectWithTag("MousterManager");
        if (mousterObj != null)
        {
            mouster = mousterObj.GetComponent<MousterManager>();
        }
    }

    //获取怪物经验
    public void mousterExp(float maxHp, float atk, float speed)
    {
        float addExp = (HpValue(maxHp) + AtkValue(atk)) * (1 + SpValue(speed));
        currentGap += addExp;
        if (currentLevel <= MaxLevel)
        {
            if (currentGap >= expGap)
            {
                currentLevel++;
                currentGap = 0;
                //Debug.Log("升级啦");
                changeMousterCount();
                changeMousterMax();
                changeMousterSpace();
                if (currentLevel % 3 == 0 && player.money >=2)
                { 
                    Upgrade?.Invoke();
                }
            }
        }
        else
        {
            changeMousterCount();
            changeMousterMax();
            changeMousterSpace();
            return;
        }
    }
    //权重-生命
    private float HpValue(float hp)
    {
        float value = (hp / player.maxHp) * 1;
        if (value < 1)
        {
            return 1;
        }
        else
        {
            return value;
        } 
    }
    //权重-攻击
    private float AtkValue(float atk)
    {
        float value = (atk / player.atk) * 0.5f;
        if (value < 0.5f)
        {
            return 0.5f;
        }
        else
        {
            return value;
        }
    }
    //权重-速度
    private float SpValue(float sp)
    {
        float value = sp / 10;
        if (value >= 1)
        {
            return 1;
        }
        else
        {
            return value;
        }
    }

    //更新怪物刷新间隔
    private void changeMousterSpace()
    {
        mouster.spawnSpace = 10 - (currentLevel / 100f) * 10;
        if (mouster.spawnSpace <= 1)
        { 
            mouster.spawnSpace = 1;
        }
    }
    //更新刷怪数量
    private void changeMousterCount()
    {
        mouster.spawnCount += (int)(currentLevel / 5f);
    }
    //更新怪物上限
    private void changeMousterMax()
    {
        mouster.maxAlive += (int)(currentLevel/10f);
    }

    //存入经验配置
    public ExpSaveData SaveExp()
    {
        return new ExpSaveData()
        {
            gap = this.expGap,
            level = this.currentLevel,
        };
    }
    //导入经验配置
    public void LoadExp(ExpSaveData exp)
    { 
        this.expGap = exp.gap;
        this.currentLevel = exp.level;
    }
}
