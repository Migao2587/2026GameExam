using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShotSpUpdate : BaseUpdate
{
    //构造函数
    public ShotSpUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "射击速度增加" + change + "倍";
        }
        else
        {
            text.text = "射击速度减少" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.shoot.coolTime -= player.shoot.coolTime * change;
            if (player.shoot.coolTime <= 0)
            {
                player.shoot.coolTime = 0.05f;
            }
        }
        else
        {
            player.shoot.coolTime += player.shoot.coolTime * change;
            
        }
    }
}
