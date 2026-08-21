using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MousSpUpdate : BaseUpdate
{
    //构造函数
    public MousSpUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "怪物生成速度减少" + change + "倍";
        }
        else
        {
            text.text = "怪物生成速度增加" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.MouM.spawnSpace += player.MouM.spawnSpace * change;
            if (player.MouM.spawnSpace >= 15)
            {
                player.MouM.spawnSpace = 15;
                player.MouM.maxAlive -= (int)(player.MouM.maxAlive * (1 + change));
                if (player.MouM.maxAlive <= 10)
                { 
                    player.MouM.maxAlive = 10;
                }
            }
        }
        else
        {
            player.MouM.spawnSpace -= player.MouM.spawnSpace * change;
            
            if (player.MouM.spawnSpace <= 2)
            {
                player.MouM.spawnSpace = 2;
                player.MouM.maxAlive += (int)(player.MouM.maxAlive * (1 + change));
            }
        }
    }
}
