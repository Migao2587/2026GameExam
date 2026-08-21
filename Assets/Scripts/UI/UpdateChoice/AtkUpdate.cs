using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AtkUpdate : BaseUpdate
{
    //构造函数
    public AtkUpdate(bool isPos, CharacterBase player) : base(isPos, player)
    {
    }

    public override void ui(TMP_Text text)
    {
        if (control)
        {
            text.text = "攻击力增加" + change + "倍";
        }
        else
        {
            text.text = "攻击力减少" + change + "倍";
        }
    }

    protected override void ChangeValue()
    {
        if (control)
        {
            player.atk += player.atk * change;
        }
        else
        {
            player.atk -= player.atk * change;
            if (player.atk <= 1)
            {
                player.atk = 1;
            }
        }
    }
}
