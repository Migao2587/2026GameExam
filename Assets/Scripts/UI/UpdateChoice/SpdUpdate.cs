using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpdUpdate : BaseUpdate
{
    //构造函数
    public SpdUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "移动速度增加" + change + "倍";
        }
        else
        { 
            text.text = "移动速度减少" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.speed += player.speed * change;
        }
        else
        { 
            player.speed -= player.speed * change;
            if (player.speed <= 1)
            {
                player.speed = 1;
            }
        }
    }
}
