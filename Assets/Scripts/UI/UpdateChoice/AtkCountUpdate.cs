using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AtkCountUpdate : BaseUpdate
{
    //构造函数
    public AtkCountUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
        if (change <= 0.5f)
        {
            change = 1;
        }
        else if (change >= 0.8f)
        {
            change = 3;
        }
        else
        {
            change = 2;
        }
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "攻击个数增加" + (int)change + "个";
        }
        else
        {
            text.text = "攻击个数减少" + (int)change + "个";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.shoot.targetCount += (int)change;
        }
        else
        {
            player.shoot.targetCount -= (int)change;
            if (player.shoot.targetCount <= 1)
            {
                player.shoot.targetCount = 1;
            }
        }
    }
}
