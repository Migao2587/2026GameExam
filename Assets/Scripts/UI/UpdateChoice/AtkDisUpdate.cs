using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AtkDisUpdate : BaseUpdate
{
    //构造函数
    public AtkDisUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "攻击半径增加" + change + "倍";
        }
        else
        {
            text.text = "攻击半径减少" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.shoot.searchRadius += player.shoot.searchRadius * change;
        }
        else
        {
            player.shoot.searchRadius -= player.shoot.searchRadius * change;
            if (player.shoot.searchRadius <= 3)
            {
                player.shoot.searchRadius = 3;
            }
        }
    }
}
