using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoomUpdate : BaseUpdate
{
    //构造函数
    public BoomUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "子弹范围增加" + change*2 + "倍";
        }
        else
        {
            text.text = "子弹范围减少" + change*2 + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.shoot.BoomRadius += change*2;
        }
        else
        {
            player.shoot.BoomRadius -=change*2;
            if (player.shoot.BoomRadius <= 0)
            {
                player.shoot.BoomRadius = 0;
            }
        }
    }
}
