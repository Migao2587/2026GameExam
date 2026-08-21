using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家数据类
[System.Serializable]
public class PlayerSaveData
{
    public Vector2 position;
    public Quaternion rotation;
    public float hp;
    public float maxhp;
    public float speed;
    public float atk;
    public float spaceTime;
    public float money;
}

//怪物数据类
[System.Serializable]
public class MousterSaveDate
{
    public string preName;
    public float speed;
    public float atk;
    public float maxHp;
    public float hp;
    public Vector2 position;
    public Quaternion rotation;
}

//经验数据类
[System.Serializable]
public class ExpSaveData
{
    public float gap;
    public int level;
}
//怪物管理类
[System.Serializable]
public class MousSaveData
{
    public int spawnCount;
    public float spawnSpace;
    public float maxAlive;
    public float alive;
}

//射击数据类
[System.Serializable]
public class ShootSaveData
{
    public float coolTime;
    public int targetCount;
    public float boomRadius;
}

//存档数据类
[System.Serializable]
public class GameSaveData
{
    public PlayerSaveData playerData;
    public List<MousterSaveDate> MousterData;
    public ExpSaveData ExpSaveData;
    public MousSaveData MousM;
    public ShootSaveData ShootData;
}
