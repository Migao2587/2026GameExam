using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpUpdate : BaseUpdate
{
    //构造函数
    public ExpUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "经验获取增加" + change + "倍";
        }
        else
        {
            text.text = "经验获取减少" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.EXP.expGap -= player.EXP.expGap * change;
            if (player.EXP.expGap <= 1)
            {
                player.EXP.expGap = 1;
            }
        }
        else
        {
            player.EXP.expGap += player.EXP.expGap * change;
        }
    }
}
