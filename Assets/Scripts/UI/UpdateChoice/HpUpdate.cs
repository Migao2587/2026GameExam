using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpUpdate : BaseUpdate
{
    //构造
    public HpUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "血量上限增加" + change + "倍";
        }
        else
        {
            text.text = "血量上限减少" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            float temphp = player.maxHp * change;
            player.maxHp += player.maxHp * change;
            player.hp += temphp;
        }
        else
        {
            player.maxHp -= player.maxHp * change;
            if (player.maxHp <= 0)
            {
                player.maxHp = 1;
            }
            if (player.hp > player.maxHp)
            {
                player.hp -= player.maxHp;
            }
            
        }
    }
}
