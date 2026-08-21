using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class BaseUpdate
{
    //变化基础值
    protected bool control;
    protected float change;
    protected CharacterBase player;


    //构造方法
    public BaseUpdate(bool isPos, CharacterBase player)
    {
        change = Random.Range(0f, 1f);
        change = Mathf.Round(change * 100f) / 100f;
        control = isPos;
        this.player = player;
    }
    //渲染文本
    public abstract void ui(TMP_Text text);
    //按钮绑定
    public void Btn(Button btn)
    {
        if (control)
        {
            btn.onClick.RemoveAllListeners();
            btn.GetComponent<ButtonCount>().countAdd();
        }
        else
        {
            btn.GetComponent<ButtonCount>().countAdd();
        }
        btn.onClick.AddListener(() => shop(btn));
    }

    //购买校检
    public void shop(Button btn)
    {
        player.money -= 2;
        if (player.money < 0)
        {
            player.money += 2;
            return;
        }
        btn.GetComponent<ButtonCount>().missChange();
        ChangeValue();
        player.updateUI?.Invoke();

    }
    //属性变化
    protected abstract void ChangeValue();

    //重置坏文本
    public void badRefrash(TMP_Text text)
    {
        text.text = "无";
    }
}
