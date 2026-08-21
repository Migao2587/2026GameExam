using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WudiUpdate : BaseUpdate
{
    //构造函数
    public WudiUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "无敌时间增加" + change + "倍";
        }
        else
        {
            text.text = "无敌时间减少" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.spaceTime += player.spaceTime * change;
        }
        else
        {
            player.spaceTime -= player.spaceTime * change;
            if (player.spaceTime <= 0)
            {
                player.spaceTime = 0.1f;
            }
        }
    }
}
